using System;
using System.Collections.Generic;

namespace ETPMS.Infrastructure.Extensions
{
    public sealed class EntityComparer<T> : IComparer<T>
    {
        private readonly Func<T, T, int> _comparer;
        public EntityComparer(Func<T, T, int> comparer)
        {
            this._comparer = comparer;
        }

        public int Compare(T x, T y)
        {
            return this._comparer(x, y);
        }
    }
}