using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiApplication.Database
{
    public static class IQueryableMethodExtensions
    {
        public static IQueryable<T> ToQueryable<T>(this T instance)
        {
            return new SingleValueQueryable<T>(instance);
        }
    }

    public class SingleValueQueryable<T> : IQueryable<T>, IEnumerator<T>
    {
        private bool hasExecutedOnce = false;

        public SingleValueQueryable(T value)
        {
            this.Current = value;
        }

        public Type ElementType => typeof(T);

        public Expression Expression => throw new NotImplementedException();

        public IQueryProvider Provider => throw new NotImplementedException();

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public T Current { get; set; }

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (hasExecutedOnce)
            {
                return false;
            }

            hasExecutedOnce = true;
            return true;
        }

        public void Reset()
        {
            this.hasExecutedOnce = false;
        }
    }
}

