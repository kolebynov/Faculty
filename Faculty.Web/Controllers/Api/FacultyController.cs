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
    [Produces("application/json")]
    [Route("api/Faculties")]
    public class FacultyController : BaseApiController<EFCore.Domain.Faculty>
    {
        public FacultyController(IRepository<EFCore.Domain.Faculty> repository, 
            IEntityExpressionsBuilder entityExpressionsBuilder, IApiHelper apiHelper) 
            : base(repository, entityExpressionsBuilder, apiHelper)
        {
        }

        [HttpGet("{id?}/specialties")]
        public async Task<IActionResult> GetSpecialties([FromServices]IRepository<Specialty> specialtyRepository, Guid id, GetItemsOptions options)
        {
            if (ModelState.IsValid)
            {
                return Json(await CreateApiResultFromQueryAsync(specialtyRepository.Entities.Where(specialty => specialty.FacultyId == id),
                    Guid.Empty, options));
            }

            return Json(GetErrorResultFromModelState(ModelState));
        }

        [HttpGet("{id?}/groups")]
        public async Task<IActionResult> GetGroups([FromServices]IRepository<Group> groupRepository, Guid id, GetItemsOptions options)
        {
            if (ModelState.IsValid)
            {
                return Json(await CreateApiResultFromQueryAsync(groupRepository.Entities.Where(group => group.Specialty.FacultyId == id),
                    Guid.Empty, options));
            }

            return Json(GetErrorResultFromModelState(ModelState));
        }
    }
}