using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Faculty.EFCore.Infrastucture
{
    public interface IEntityExpressionsBuilder
    {
        Expression<Func<TEntity, TId>> GetIdSelectorExpression<TEntity, TId>();
        Expression<Func<TEntity, TEntity>> GetDefaultEntitySelectorExpression<TEntity>();
    }
}
