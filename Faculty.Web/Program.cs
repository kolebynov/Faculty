using System;
using System.Linq;
using Faculty.EFCore.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Faculty.EFCore.Domain;
using Microsoft.AspNetCore.Identity;
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
                SeedTestData(scope.ServiceProvider.GetRequiredService<FacultyContext>(),
                    scope.ServiceProvider.GetRequiredService<UserManager<User>>());
            }

            
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        private static void SeedTestData(FacultyContext context, UserManager<User> userManager)
        {
            SeedFaculty(context);
            SeedUsers(userManager);
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

        private static void SeedUsers(UserManager<User> userManager)
        {
            userManager.CreateAsync(new User
            {
                UserName = "kolebynov"
            }, "1q1q1q1q").Wait();
        }
    }
}