namespace Kubis1982.Shared.Persistance.Comparers
{
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class DictionaryStringComparer : ValueComparer<Dictionary<string, string>>
    {
        public DictionaryStringComparer() : base(
                (c1, c2) => c1!.SequenceEqual(c2!),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToDictionary())
        {
        }
    }
}
