using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Faculty.EFCore.Domain;
using Faculty.EFCore.Resources;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Faculty.EFCore.Infrastucture
{
    public class EntityExpressionsBuilder : IEntityExpressionsBuilder
    {
        private static readonly ConcurrentDictionary<Type, LambdaExpression> _defaultEntitySelectorsCache = 
            new ConcurrentDictionary<Type, LambdaExpression>();

        public Expression<Func<TEntity, TEntity>> GetDefaultEntitySelectorExpression<TEntity>()
        {
            Type entityType = typeof(TEntity);
            if (!_defaultEntitySelectorsCache.TryGetValue(entityType, out LambdaExpression result))
            {
                ParameterExpression inputEntityExpression = Expression.Parameter(entityType);

                _defaultEntitySelectorsCache[entityType] = result = Expression.Lambda<Func<TEntity, TEntity>>(
                    Expression.MemberInit(
                        Expression.New(entityType.GetConstructor(Type.EmptyTypes)),
                        entityType.GetProperties().Select<PropertyInfo, MemberBinding>(property =>
                            Expression.Bind(property, Expression.Property(inputEntityExpression, property)))
                    ),
                    inputEntityExpression
                );
            }

            return (Expression<Func<TEntity, TEntity>>)result;
        }
    }
}
