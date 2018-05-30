using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.EFCore.Data;
using Faculty.EFCore.Domain;
using Faculty.EFCore.Infrastucture;
using Faculty.Web.Services.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faculty.Web.Controllers.Api
{
    [Route("api/Specialties")]
    public class SpecialtyController : BaseApiController<Specialty>
    {
        public SpecialtyController(IRepository<Specialty> repository, IEntityExpressionsBuilder entityExpressionsBuilder, IApiHelper apiHelper) 
            : base(repository, entityExpressionsBuilder, apiHelper)
        {
        }
    }
}