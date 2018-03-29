using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Faculty.EFCore.Resources;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Faculty.EFCore.Infrastucture
{
    public class EntityExpressionsBuilder : IEntityExpressionsBuilder
    {
        private static readonly Dictionary<(Type entityType, Type idType), LambdaExpression> _idSelectorExpressionsCache =
            new Dictionary<(Type entityType, Type idType), LambdaExpression>();

        public Expression<Func<TEntity, TId>> GetIdSelectorExpression<TEntity, TId>()
        {
            Type entityType = typeof(TEntity);
            Type idType = typeof(TId);
            var cacheKey = (entityType, idType);
            if (!_idSelectorExpressionsCache.TryGetValue(cacheKey, out LambdaExpression resultExpression))
            {
                PropertyInfo keyPropertyInfo = entityType.GetProperties()
                    .FirstOrDefault(prop => prop.GetCustomAttribute<KeyAttribute>() != null && prop.PropertyType == idType);
                if (keyPropertyInfo == null)
                {
                    throw new ArgumentException(string.Format(
                            ExceptionMessages.EntityExpressionsBuilder_NotFoundIdPropertyFomat, idType.Name, entityType.Name),
                        nameof(TId));
                }
                var entityParameter = Expression.Parameter(entityType);
                _idSelectorExpressionsCache[cacheKey] = resultExpression =
                    Expression.Lambda<Func<TEntity, TId>>(Expression.Property(entityParameter, keyPropertyInfo), entityParameter);
            }

            return (Expression<Func<TEntity, TId>>)resultExpression;
        }
    }
}
