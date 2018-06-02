using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.EFCore.Data;
using Faculty.EFCore.Domain;
using Faculty.EFCore.Infrastucture;
using Faculty.Web.Model;
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

        [HttpGet("{id?}/groups")]
        public async Task<IActionResult> GetGroups([FromServices]IRepository<Group> groupRepository, Guid id, GetItemsOptions options)
        {
            if (ModelState.IsValid)
            {
                return Json(await CreateApiResultFromQueryAsync(groupRepository.Entities.Where(group => group.SpecialtyId == id),
                    Guid.Empty, options));
            }

            return Json(GetErrorResultFromModelState(ModelState));
        }
    }
}