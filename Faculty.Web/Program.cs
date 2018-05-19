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
            SeedFaculty(context);
            context.SaveChanges();
        }

        private static void SeedFaculty(FacultyContext context)
        {
            EFCore.Domain.Faculty faculty = new EFCore.Domain.Faculty
            {
                Name = "Факультет 1"
            };
            context.Faculties.Add(faculty);
            SeedSpecialities(context, faculty);
        }

        private static void SeedSpecialities(FacultyContext context, EFCore.Domain.Faculty faculty)
        {
            Specialty specialty = new Specialty
            {
                FacultyId = faculty.Id,
                Name = "Специальность 1"
            };
            context.Specialties.Add(specialty);
            SeedGroups(context, specialty);
        }

        private static void SeedGroups(FacultyContext context, Specialty specialty)
        {
            foreach (int i in Enumerable.Range(1, 10))
            {
                Group group = new Group
                {
                    Id = Guid.NewGuid(),
                    Name = "Группа " + i,
                    SpecialtyId = specialty.Id
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
                    Name = "Студент " + i + " " + group.Name,
                    FirstName = "Студент " + i,
                    GroupId = group.Id
                });
            }
        }
    }
}