using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.EFCore.Data;
using Faculty.EFCore.Domain;
using Faculty.EFCore.Infrastucture;
using Faculty.Web.Infrastructure;
using Faculty.Web.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faculty.Web.Controllers.Api
{
    [Route("api/students")]
    public class StudentsController : BaseApiController<Student>
    {
        public StudentsController(IRepository<Student> repository, IEntityExpressionsBuilder entityExpressionsBuilder) 
            : base(repository, entityExpressionsBuilder)
        {
        }
    }
}