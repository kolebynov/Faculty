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
using Faculty.Web.Services.Api;

namespace Faculty.Web.Controllers.Api
{
    [Produces("application/json")]
    public abstract class BaseApiController<TEntity> : Controller
        where TEntity : BaseEntity
    {
        protected readonly IEntityExpressionsBuilder EntityExpressionsBuilder;
        protected readonly IApiHelper ApiHelper;

        public IRepository<TEntity> EntityRepository { get; }

        [HttpGet("{id?}")]
        public virtual async Task<IActionResult> GetItems(Guid id, GetItemsOptions options)
        {
            if (ModelState.IsValid)
            {
                return Json(await CreateApiResultFromQueryAsync(EntityRepository.Entities, id, options));
            }

            return Json(GetErrorResultFromModelState(ModelState));
        }

        [HttpPost]
        public virtual async Task<IActionResult> AddItem([FromBody] TEntity item)
        {
            if (ModelState.IsValid)
            {
                await EntityRepository.AddAsync(item);
                TEntity newItem = (await GetItemsFromQueryAsync(EntityRepository.Entities, item.Id, null)).First();
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
                await EntityRepository.UpdateAsync(item);
                TEntity updatedEntity = (await GetItemsFromQueryAsync(EntityRepository.Entities, id, null)).First();
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

        protected BaseApiController(IRepository<TEntity> repository, IEntityExpressionsBuilder entityExpressionsBuilder, IApiHelper apiHelper)
        {
            EntityRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            EntityExpressionsBuilder = entityExpressionsBuilder ?? throw new ArgumentNullException(nameof(entityExpressionsBuilder));
            ApiHelper = apiHelper ?? throw new ArgumentNullException(nameof(apiHelper)); ;
        }

        protected virtual async Task<ApiResult<IEnumerable<T>>> CreateApiResultFromQueryAsync<T>(IQueryable<T> query, Guid id, GetItemsOptions options)
            where T : BaseEntity
        {
            int rowsTotal = 1;
            if (Equals(id, Guid.Empty))
            {
                rowsTotal = query.Count();
            }

            return ApiResult.SuccesGetResult(await GetItemsFromQueryAsync(query, id, options), new PaginationData
            {
                CurrentPage = options?.Page ?? 1,
                ItemsPerPage = options?.RowsCount ?? rowsTotal,
                TotalItems = rowsTotal
            });
        }

        protected virtual async Task<IEnumerable<T>> GetItemsFromQueryAsync<T>(IQueryable<T> query, Guid id, GetItemsOptions options)
            where T : BaseEntity
        {
            query = query.Select(EntityExpressionsBuilder.GetDefaultEntitySelectorExpression<T>());
            if (!Equals(id, Guid.Empty))
            {
                query = query.Where(entity => entity.Id == id);
            }
            else
            {
                if (options != null && options.RowsCount > 0)
                {
                    query = query.Skip((options.Page - 1) * options.RowsCount).Take(options.RowsCount);
                }
            }

            return await Task.FromResult((IEnumerable<T>)query.ToArray());
        }

        protected virtual ApiResult GetErrorResultFromModelState(ModelStateDictionary modelState)
        {
            return ApiHelper.GetErrorResultFromModelState(modelState);
        }
    }
}