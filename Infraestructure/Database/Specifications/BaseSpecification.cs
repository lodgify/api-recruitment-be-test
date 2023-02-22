using Domain.Specifications;
using System;
using System.Linq.Expressions;

namespace Infraestructure.Database.Specifications
{
    /// <summary>
    /// Base class that implements the ISpecification<typeparamref name="T"/> interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseSpecification<T> : ISpecification<T>
    {
        private Expression<Func<T, bool>> _expression;

        public BaseSpecification(Expression<Func<T, bool>> predicate)
        {
            _expression = predicate;
        }

        public Expression<Func<T, bool>> QueryExpression => _expression;
    }
}
