using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.EFCore.Data;
using Faculty.EFCore.Domain;
using Faculty.Web.Infrastructure;
using Faculty.Web.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faculty.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Students")]
    public class StudentsController : BaseApiController<Student, StudentDto, Guid>
    {
        public StudentsController(IRepository<Student> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Guid IdSelector(Student entity) => entity.Id;
    }
}