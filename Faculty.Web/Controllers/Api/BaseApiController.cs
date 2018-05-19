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
                return Json(await GetItemsFromRepository(EntityRepository.Entities, id, options));
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

        protected virtual Task<ApiResult<T[]>> GetItemsFromRepository<T>(IQueryable<T> query, Guid id, GetItemsOptions options)
            where T : BaseEntity
        {
            int rowsTotal;
            query = query.Select(_entityExpressionsBuilder.GetDefaultEntitySelectorExpression<T>());
            if (!Equals(id, Guid.Empty))
            {
                query = query.Where(entity => entity.Id == id);
                rowsTotal = 1;
            }
            else
            {
                rowsTotal = query.Count();

                if (options != null && options.RowsCount > 0)
                {
                    query = query.Skip((options.Page - 1) * options.RowsCount).Take(options.RowsCount);
                }
            }
            return Task.FromResult((ApiResult<T[]>)ApiResult.SuccesGetResult(query.ToArray(), new PaginationData
            {
                CurrentPage = options?.Page ?? 1,
                ItemsPerPage = options?.RowsCount ?? rowsTotal,
                TotalItems = rowsTotal
            }));
        }

        protected virtual ApiResult GetErrorResultFromModelState(ModelStateDictionary modelState)
        {
            IEnumerable<ApiError> errors = modelState.Values
                .SelectMany(s => s.Errors)
                .Select(e => new ApiError { Message = e.ErrorMessage });
            return ApiResult.ErrorResult(errors);
        }
    }
}