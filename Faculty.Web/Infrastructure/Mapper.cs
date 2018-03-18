using Faculty.EFCore.Domain;
using Faculty.Web.Model;
using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Faculty.Web.Infrastructure
{
    public class Mapper : IMapper
    {
        private static readonly string ENTITIES_NAMESPACE = "Faculty.EFCore.Domain";
        private static readonly string MODELS_NAMESPACE = "Faculty.Web.Model";

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
                InitEntityMappings(config);
            });
        }

        private static void InitEntityMappings(IMapperConfigurationExpression config)
        {
            var entityTypes = typeof(Cathedra).Assembly.GetTypes()
                .Where(t => t.Namespace == ENTITIES_NAMESPACE);
            var modelTypes = typeof(CathedraDto).Assembly.GetTypes()
                .Where(t => t.Namespace == MODELS_NAMESPACE).ToArray();
            foreach (Type entityType in entityTypes)
            {
                Type modelType = Array.Find(modelTypes, t => t.Name == entityType.Name + "Dto");
                if (modelType != null)
                {
                    config.CreateMap(entityType, modelType);
                }
            }
        }
    }
}
