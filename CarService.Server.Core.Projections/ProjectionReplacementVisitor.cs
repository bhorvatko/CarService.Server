using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Core.Projections
{
    internal class ProjectionReplacementVisitor : ExpressionVisitor
    {
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == "Project")
            {
                IProjection projection = new ProjectionFactory().GetProjection(node.Object!.Type);

                LambdaExpression visitedExpression = (LambdaExpression)projection.VisitedExpression;

                MultiParamReplaceVisitor visitor = new MultiParamReplaceVisitor(node.Arguments.ToArray(), visitedExpression);
                Expression withReplacedParameters = visitor.Visit(visitedExpression.Body);

                return Visit(withReplacedParameters);
            } else
            {
                return base.VisitMethodCall(node);
            }
        }
    }
}
