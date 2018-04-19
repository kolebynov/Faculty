using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Faculty.EFCore.Data;
using Faculty.EFCore.Infrastucture;
using Faculty.Web.ApiResults;
using Faculty.Web.Infrastructure;
using Faculty.Web.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Faculty.Web.Controllers.Api
{
    [Produces("application/json")]
    public abstract class BaseApiController<TEntity, TId> : Controller
    {
        private readonly IEntityExpressionsBuilder _entityExpressionsBuilder;
        private Func<TEntity, TId> _idSelector;

        public IRepository<TEntity> EntityRepository { get; }

        protected virtual Func<TEntity, TId> IdSelector
        {
            get => _idSelector ?? (_idSelector = CreateIdSelector());
            set => _idSelector = value;
        }

        [HttpGet("{id?}")]
        public virtual async Task<IActionResult> GetItems(TId id, GetItemsOptions options)
        {
            if (ModelState.IsValid)
            {
                return Json(await GetItemsFromRepository(id, options));
            }

            return Json(GetErrorResultFromModelState(ModelState));
        }

        [HttpPost]
        public virtual async Task<IActionResult> AddItem([FromBody] TEntity item)
        {
            if (ModelState.IsValid)
            {
                TEntity newItem = await EntityRepository.AddAsync(item);
                return CreatedAtAction("GetItems", new { id = IdSelector(newItem) },
                    ApiResult.SuccessResult(new[] { newItem }));
            }

            return Json(GetErrorResultFromModelState(ModelState));
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> UpdateItem(TId id, [FromBody] TEntity item)
        {
            if (ModelState.IsValid)
            {
                TEntity updatedEntity = await EntityRepository.UpdateAsync(item);
                return CreatedAtAction("GetItems", new { id }, ApiResult.SuccessResult(new[] { updatedEntity }));
            }

            return Json(GetErrorResultFromModelState(ModelState));
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> RemoveItem(TId id)
        {
            if (ModelState.IsValid)
            {
                await EntityRepository.DeleteAsync(id);
                return Json(new ApiResult { Success = true });
            }

            return Json(GetErrorResultFromModelState(ModelState));
        }

        protected BaseApiController(IRepository<TEntity> repository, IEntityExpressionsBuilder entityExpressionsBuilder)
        {
            EntityRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _entityExpressionsBuilder = entityExpressionsBuilder ?? throw new ArgumentNullException(nameof(entityExpressionsBuilder));
        }

        protected virtual async Task<ApiResult<TEntity[]>> GetItemsFromRepository(TId id, GetItemsOptions options)
        {
            TEntity[] entities;
            int rowsTotal;
            if (!Equals(id, default(TId)))
            {
                entities = new[] {await EntityRepository.GetByIdAsync(id)};
                rowsTotal = 1;
            }
            else
            {
                IQueryable<TEntity> query = EntityRepository.Entities;
                if (options != null && options.RowsCount > 0)
                {
                    query = query.Skip((options.Page - 1) * options.RowsCount).Take(options.RowsCount);
                }

                entities = query.ToArray();
                rowsTotal = EntityRepository.Entities.Count();
            }
            return ApiResult.SuccesGetResult(entities, new PaginationData
            {
                CurrentPage = options?.Page ?? 1,
                ItemsPerPage = options?.RowsCount ?? rowsTotal,
                TotalItems = rowsTotal
            });
        }

        protected virtual Func<TEntity, TId> CreateIdSelector() =>
            _entityExpressionsBuilder.GetIdSelectorExpression<TEntity, TId>().Compile();

        private ApiResult GetErrorResultFromModelState(ModelStateDictionary modelState)
        {
            IEnumerable<ApiError> errors = modelState.Values
                .SelectMany(s => s.Errors)
                .Select(e => new ApiError { Message = e.ErrorMessage });
            return ApiResult.ErrorResult(errors);
        }
    }
}