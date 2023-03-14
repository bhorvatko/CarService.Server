using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Core.Projections
{
    public class MultiParamReplaceVisitor : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, Expression> replacements;
        private readonly LambdaExpression expressionToVisit;
        public MultiParamReplaceVisitor(Expression[] parameterValues, LambdaExpression expressionToVisit)
        {
            if (parameterValues.Length != expressionToVisit.Parameters.Count)
                throw new ArgumentException(string.Format("The paraneter values count ({0}) does not match the expression parameter count ({1})", parameterValues.Length, expressionToVisit.Parameters.Count));
            replacements = expressionToVisit.Parameters
                .Select((p, idx) => new { Idx = idx, Parameter = p })
                .ToDictionary(x => x.Parameter, x => parameterValues[x.Idx]);
            this.expressionToVisit = expressionToVisit;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (replacements.TryGetValue(node, out Expression? replacement))
                return Visit(replacement);
            return base.VisitParameter(node);
        }

        public Expression Replace()
        {
            return Visit(expressionToVisit.Body);
        }
    }
}
