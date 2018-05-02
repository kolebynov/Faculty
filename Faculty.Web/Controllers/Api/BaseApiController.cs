using Faculty.EFCore.Data;
using Faculty.EFCore.Domain;
using Faculty.EFCore.Infrastucture;
using Faculty.Web.ApiResults;
using Faculty.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Web.Controllers.Api
{
    [Produces("application/json")]
    public abstract class BaseApiController<TEntity> : Controller
        where TEntity : BaseEntity
    {
        private readonly IEntityExpressionsBuilder _entityExpressionsBuilder;

        public IRepository<TEntity> EntityRepository { get; }

        [HttpGet("{id?}")]
        public virtual async Task<IActionResult> GetItems(Guid id, GetItemsOptions options)
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
                return CreatedAtAction("GetItems", new { id = newItem.Id },
                    ApiResult.SuccessResult(new[] { newItem }));
            }

            return Json(GetErrorResultFromModelState(ModelState));
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> UpdateItem(Guid id, [FromBody] TEntity item)
        {
            if (ModelState.IsValid)
            {
                TEntity updatedEntity = await EntityRepository.UpdateAsync(item);
                return CreatedAtAction("GetItems", new { id }, ApiResult.SuccessResult(new[] { updatedEntity }));
            }

            return Json(GetErrorResultFromModelState(ModelState));
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> RemoveItem(Guid id)
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

        protected virtual Task<ApiResult<TEntity[]>> GetItemsFromRepository(Guid id, GetItemsOptions options)
        {
            int rowsTotal;
            IQueryable<TEntity> query = EntityRepository.Entities.Select(_entityExpressionsBuilder.GetDefaultEntitySelectorExpression<TEntity>());
            if (!Equals(id, Guid.Empty))
            {
                query = query.Where(entity => entity.Id == id);
                rowsTotal = 1;
            }
            else
            {
                if (options != null && options.RowsCount > 0)
                {
                    query = query.Skip((options.Page - 1) * options.RowsCount).Take(options.RowsCount);
                }

                rowsTotal = EntityRepository.Entities.Count();
            }
            return Task.FromResult((ApiResult<TEntity[]>)ApiResult.SuccesGetResult(query.ToArray(), new PaginationData
            {
                CurrentPage = options?.Page ?? 1,
                ItemsPerPage = options?.RowsCount ?? rowsTotal,
                TotalItems = rowsTotal
            }));
        }

        private ApiResult GetErrorResultFromModelState(ModelStateDictionary modelState)
        {
            IEnumerable<ApiError> errors = modelState.Values
                .SelectMany(s => s.Errors)
                .Select(e => new ApiError { Message = e.ErrorMessage });
            return ApiResult.ErrorResult(errors);
        }
    }
}