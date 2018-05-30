using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Web.ApiResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Faculty.Web.Services.Api
{
    public interface IApiHelper
    {
        ApiResult GetErrorResultFromModelState(ModelStateDictionary modelState);
    }
}