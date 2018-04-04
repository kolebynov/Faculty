using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Faculty.EFCore.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Faculty.Web.Infrastructure;
using Faculty.Web.Model;
using Faculty.EFCore.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Faculty.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                SeedTestData(scope.ServiceProvider.GetRequiredService<FacultyContext>());
            }

            
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        private static void SeedTestData(FacultyContext context)
        {
            SeedGroups(context);
            context.SaveChanges();
        }

        private static void SeedGroups(FacultyContext context)
        {
            foreach (int i in Enumerable.Range(1, 10))
            {
                Group group = new Group
                {
                    Id = Guid.NewGuid(),
                    Name = "Group " + i
                };
                context.Groups.Add(group);
                SeedStudentsForGroup(context, group);
            }
        }

        private static void SeedStudentsForGroup(FacultyContext context, Group group)
        {
            foreach (int i in Enumerable.Range(1, 30))
            {
                context.Students.Add(new Student
                {
                    Id = Guid.NewGuid(),
                    Name = "Student " + i + " " + group.Name,
                    FirstName = "Student " + i,
                    GroupId = group.Id
                });
            }
        }
    }
}