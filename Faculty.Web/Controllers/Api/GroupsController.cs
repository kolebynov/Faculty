﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.EFCore.Data;
using Faculty.EFCore.Domain;
using Faculty.EFCore.Infrastucture;
using Faculty.Web.ApiResults;
using Faculty.Web.Model;
using Microsoft.AspNetCore.Mvc;

namespace Faculty.Web.Controllers.Api
{
    [Route("api/groups")]
    public class GroupsController : BaseApiController<Group>
    {
        public GroupsController(IRepository<Group> repository, IEntityExpressionsBuilder entityExpressionsBuilder)
            : base(repository, entityExpressionsBuilder)
        {
        }
    }
}