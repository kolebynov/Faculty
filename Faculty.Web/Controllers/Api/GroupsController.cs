using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.EFCore.Data;
using Faculty.EFCore.Domain;
using Faculty.EFCore.Infrastucture;
using Faculty.Web.ApiResults;
using Faculty.Web.Model;
using Faculty.Web.Services.Api;
using Microsoft.AspNetCore.Mvc;

namespace Faculty.Web.Controllers.Api
{
    [Route("api/groups")]
    public class GroupsController : BaseApiController<Group>
    {
        public GroupsController(IRepository<Group> repository, IEntityExpressionsBuilder entityExpressionsBuilder, IApiHelper apiHelper)
            : base(repository, entityExpressionsBuilder, apiHelper)
        {
        }

        [HttpGet("{id?}/students")]
        public async Task<IActionResult> GetStudents([FromServices]IRepository<Student> studentRepository, Guid id, GetItemsOptions options)
        {
            if (ModelState.IsValid)
            {
                return Json(await CreateApiResultFromQueryAsync(studentRepository.Entities.Where(student => student.GroupId == id), 
                    Guid.Empty, options));
            }

            return Json(GetErrorResultFromModelState(ModelState));
        }
    }
}