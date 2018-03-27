﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.EFCore.Data;
using Faculty.Web.ApiResults;
using Faculty.Web.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Faculty.Web.Controllers.Api
{
    [Produces("application/json")]
    public abstract class BaseApiController<TEntity, TId> : Controller
    {
        public IRepository<TEntity> EntityRepository { get; }

        [HttpGet("{id?}")]
        public virtual async Task<IActionResult> GetItems(TId id)
        {
            return Json(await GetItemsFromRepository(id));
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

        protected BaseApiController(IRepository<TEntity> repository)
        {
            EntityRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected virtual async Task<ApiResult<TEntity[]>> GetItemsFromRepository(TId id)
        {
            TEntity[] entities = !id.Equals(default(TId)) ? new[] { await EntityRepository.GetByIdAsync(id) } : EntityRepository.Entities.ToArray();

            return ApiResult.SuccessResult(entities);
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