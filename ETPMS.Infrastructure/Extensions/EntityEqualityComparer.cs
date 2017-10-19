using System;
using System.Collections.Generic;

namespace ETPMS.Infrastructure.Extensions
{
    public sealed class EntityEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _comparer;

        public EntityEqualityComparer(Func<T, T, bool> comparer)
        {
            this._comparer = comparer;
        }

        public bool Equals(T x, T y)
        {
            return this._comparer != null && this._comparer(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}