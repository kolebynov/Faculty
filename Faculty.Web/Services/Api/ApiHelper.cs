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
            IEnumerable<ApiError> errors = modelState.Values
                .SelectMany(s => s.Errors)
                .Select(e => new ApiError { Message = e.ErrorMessage });
            return ApiResult.ErrorResult(errors);
        }
    }
}