using System.Collections.Generic;
using System.Linq;
using Faculty.Web.ApiResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Faculty.Web.Services.Api
{
    public class ApiHelper : IApiHelper
    {
        public ApiResult GetErrorResultFromModelState(ModelStateDictionary modelState)
        {
            IEnumerable<ApiError> errors = modelState
                .Where(pair => pair.Value.Errors.Count > 0)
                .Select(pair => new ApiError
                {
                    Key = pair.Key,
                    Message = string.Join("\n", pair.Value.Errors.Select(err => err.ErrorMessage))
                });

            return ApiResult.ErrorResult(errors);
        }
    }
}