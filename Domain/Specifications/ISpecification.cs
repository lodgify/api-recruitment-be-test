using System;
using System.Linq.Expressions;

namespace Domain.Specifications
{
    /// <summary>
    /// Defines the contract for a class that defines specifications through Expressions that receive an object of type <typeparamref name="T"/> and return a bool.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// Represents the existing Expression in the spefification.
        /// </summary>
        Expression<Func<T, bool>> QueryExpression { get; }
    }
}
