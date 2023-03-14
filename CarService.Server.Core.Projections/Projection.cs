using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Core.Projections
{
    public abstract class Projection<TEntity, TDto> : IProjection
    {
        protected abstract Expression<Func<TEntity, TDto>> Expression { get; }
        public Expression VisitedExpression => GetExpression();

        public TDto Project(TEntity entity)
        {
            return Expression.Compile().Invoke(entity);
        }

        public Expression<Func<TEntity, TDto>> GetExpression()
        {
            var visitor = new ProjectionReplacementVisitor();
            var expression = visitor.Visit(Expression);

            return (Expression<Func<TEntity, TDto>>)expression;
        }
    }

    public interface IProjection
    {
        Expression VisitedExpression { get; }
    }
}
