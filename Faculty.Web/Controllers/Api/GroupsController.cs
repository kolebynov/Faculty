using System;
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

        protected override async Task<ApiResult<Group[]>> GetItemsFromRepository(Guid id, GetItemsOptions options)
        {
            Group[] entities;
            int rowsTotal;
            if (!Equals(id, Guid.Empty))
            {
                entities = new[] { await EntityRepository.GetByIdAsync(id) };
                rowsTotal = 1;
            }
            else
            {
                IQueryable<Group> query = EntityRepository.Entities.Select(g => new Group
                {
                    Id = g.Id,
                    Name = g.Name,
                    Specialty = g.Specialty,
                    SpecialtyId = g.SpecialtyId
                });
                if (options != null && options.RowsCount > 0)
                {
                    query = query.Skip((options.Page - 1) * options.RowsCount).Take(options.RowsCount);
                }

                entities = query.ToArray();
                rowsTotal = EntityRepository.Entities.Count();
            }
            return ApiResult.SuccesGetResult(entities, new PaginationData
            {
                CurrentPage = options?.Page ?? 1,
                ItemsPerPage = options?.RowsCount ?? rowsTotal,
                TotalItems = rowsTotal
            });
        }
    }
}