using Faculty.EFCore.Data;
using Faculty.EFCore.Domain;
using Faculty.EFCore.Infrastucture;
using Faculty.Web.Services.Api;
using Microsoft.AspNetCore.Mvc;

namespace Faculty.Web.Controllers.Api
{
    [Route("api/students")]
    public class StudentsController : BaseApiController<Student>
    {
        public StudentsController(IRepository<Student> repository, IEntityExpressionsBuilder entityExpressionsBuilder, IApiHelper apiHelper) 
            : base(repository, entityExpressionsBuilder, apiHelper)
        {
        }
    }
}