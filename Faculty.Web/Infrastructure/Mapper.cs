using Faculty.EFCore.Domain;
using Faculty.Web.Model;
using System;

namespace Faculty.Web.Infrastructure
{
    public class Mapper : IMapper
    {
        public V Map<T, V>(T src)
        {
            return AutoMapper.Mapper.Map<T, V>(src);
        }

        static Mapper()
        {
            InitMapper();
        }

        private static void InitMapper()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<BaseLookup, NameValuePair<Guid>>()
                    .ConvertUsing(lookup => new NameValuePair<Guid>(lookup.Name, lookup.Id));
                config.CreateMap<Cathedra, CathedraDto>();
                config.CreateMap<Course, CourseDto>();
                config.CreateMap<EFCore.Domain.Faculty, FacultyDto>();
                config.CreateMap<Group, GroupDto>();
                config.CreateMap<Specialty, SpecialtyDto>();
                config.CreateMap<Student, StudentDto>();
                config.CreateMap<Subject, SubjectDto>();
                config.CreateMap<Teacher, TeacherDto>();
            });
        }
    }
}
