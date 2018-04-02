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

        public object Map(object src, Type srcType, Type destType)
        {
            return AutoMapper.Mapper.Map(src, srcType, destType);
        }

        static Mapper()
        {
            InitMapper();
        }

        private static void InitMapper()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                /*config.CreateMap<BaseLookup, NameValuePair<Guid>>()
                    .ConvertUsing(lookup => new NameValuePair<Guid>(lookup.Name, lookup.Id));
                InitEntityMappings(config);*/
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
                    MapEntityToModel(config, entityType, modelType);
                    MapModelToEntity(config, entityType, modelType);
                }
            }
        }

        private static void MapEntityToModel(IMapperConfigurationExpression config, Type entityType, Type modelType)
        {
            config.CreateMap(entityType, modelType);
        }

        private static void MapModelToEntity(IMapperConfigurationExpression config, Type entityType, Type modelType)
        {
            var mappingExpr = config.CreateMap(modelType, entityType);
            var allModelProps = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var idsProps = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(prop => prop.Name.EndsWith("Id") && prop.PropertyType == typeof(Guid));
            foreach (var idProp in idsProps)
            {
                var propName = idProp.Name.Remove(idProp.Name.Length - 2);
                var modelNameValuePairProp = Array.Find(allModelProps, prop => prop.Name == propName);
                if (modelNameValuePairProp != null)
                {
                    mappingExpr.ForMember(idProp.Name, opt => opt.MapFrom(
                        src => ((NameValuePair<Guid>)modelNameValuePairProp.GetValue(src)).Value));
                }
            }
        }
    }
}
