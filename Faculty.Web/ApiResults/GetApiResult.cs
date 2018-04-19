using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Web.Model;

namespace Faculty.Web.ApiResults
{
    public class GetApiResult<TData> : ApiResult<TData>
    {
        public PaginationData Pagination { get; set; }
    }
}
