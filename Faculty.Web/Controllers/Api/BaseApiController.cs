using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Core.Data;
using Faculty.Web.ApiResults;
using Faculty.Web.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Faculty.Web.Controllers.Api
{
    [Produces("application/json")]
    public abstract class BaseApiController<TEntity, TModel, TId> : Controller
    {
        public IRepository<TEntity> EntityRepository { get; }
        public IMapper Mapper { get; }

        [HttpGet("{id?}")]
        public virtual async Task<IActionResult> GetItems(TId id)
        {
            return Json(await GetItemsFromQuery(id));
        }

        [HttpPost]
        public virtual async Task<IActionResult> AddItem([FromBody] TModel item)
        {
            if (ModelState.IsValid)
            {
                TEntity newItem = await EntityRepository.AddAsync(Mapper.Map<TModel, TEntity>(item));
                return CreatedAtAction("GetItems", new { id = IdSelector(newItem) },
                    ApiResult.SuccessResult(new[] { Mapper.Map<TEntity, TModel>(newItem) }));
            }

            return Json(GetErrorResultFromModelState(ModelState));
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> UpdateItem(TId id, [FromBody] TModel item)
        {
            if (ModelState.IsValid)
            {
                TEntity updatedEntity = await EntityRepository.UpdateAsync(Mapper.Map<TModel, TEntity>(item));
                return CreatedAtAction("GetItems", new { id }, ApiResult.SuccessResult(new[] { Mapper.Map<TEntity, TModel>(updatedEntity) }));
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

        protected BaseApiController(IRepository<TEntity> repository, IMapper mapper)
        {
            EntityRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected virtual async Task<ApiResult<TModel[]>> GetItemsFromQuery(TId id)
        {
            TEntity[] entities = !id.Equals(default(TId)) ? new[] { await EntityRepository.GetByIdAsync(id) } : EntityRepository.Entities.ToArray();

            return ApiResult.SuccessResult(Mapper.Map<TEntity[], TModel[]>(entities));
        }

        protected abstract TId IdSelector(TEntity entity);

        private ApiResult GetErrorResultFromModelState(ModelStateDictionary modelState)
        {
            IEnumerable<ApiError> errors = modelState.Values
                .SelectMany(s => s.Errors)
                .Select(e => new ApiError { Message = e.ErrorMessage });
            return ApiResult.ErrorResult(errors);
        }
    }
}