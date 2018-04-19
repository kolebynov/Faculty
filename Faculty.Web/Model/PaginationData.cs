using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Web.Model
{
    public class PaginationData
    {
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalPages => ItemsPerPage > 0 ? (int)Math.Ceiling((double)TotalItems / ItemsPerPage) : 1;
    }
}
