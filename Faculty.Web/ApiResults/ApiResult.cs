using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Web.ApiResults
{
    public class ApiResult<TData> : ApiResult
    {
        public TData Data { get; set; }
    }

    public class ApiResult
    {
        public bool Success { get; set; }
        public IEnumerable<ApiError> Errors { get; set; }

        public static ApiResult<TData> SuccessResult<TData>(TData data)
        {
            return new ApiResult<TData>
            {
                Success = true,
                Data = data
            };
        }

        public static ApiResult ErrorResult(IEnumerable<ApiError> errors)
        {
            return new ApiResult
            {
                Success = false,
                Errors = errors
            };
        }
    }
}
