using Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infraestructure.Database.Specifications
{
    /// <summary>
    /// Class that allows to work with multiple Expressions of <typeparamref name="T"/> and bool at the same time.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AndSpecification<T> : ISpecification<T>
    {
        private List<Expression<Func<T, bool>>> _expressions = new List<Expression<Func<T, bool>>>();

        public void AddExpression(Expression<Func<T, bool>> expression)
        {
            _expressions.Add(expression);
        }

        /// <summary>
        /// Returns an Expression which represents the concatenation of all the internal expressions. This expressions will return true if all the provided expressions match.
        /// </summary>
        /// <param name="expressions"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public Expression<Func<T, bool>> QueryExpression
        {
            get
            {
                if (_expressions.Count == 1)
                {
                    return _expressions.First();
                }

                return GenerateMultipleQueryExpression(_expressions, 0);
            }
        }

        private Expression<Func<T, bool>> GenerateMultipleQueryExpression(List<Expression<Func<T, bool>>> expressions, int position)
        {
            var leftExpression = expressions[position];
            ParameterExpression parameter = leftExpression.Parameters.First();

            if (position + 2 == expressions.Count)
            {
                var rightExpression = expressions[position + 1];
                return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(
                        leftExpression.Body,
                        Expression.Invoke(rightExpression, parameter)),
                    parameter);
            }

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(
                    leftExpression.Body,
                    Expression.Invoke(
                        GenerateMultipleQueryExpression(
                            expressions,
                            position++),
                        parameter)),
                parameter);
        }
    }
}
