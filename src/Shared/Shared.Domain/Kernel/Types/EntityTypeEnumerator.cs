namespace Kubis1982.Shared.Kernel.Types
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public abstract class EntityTypeEnumerator(string prefix, short numerator, string name) : IEquatable<EntityTypeEnumerator?>
    {
        public EntityTypeId Code { get; } = EntityTypeId.Create(prefix, numerator);
        public string Name { get; } = name;

        public override string ToString() => $"{Name} ({Code})";

        public static IEnumerable<T> GetAll<T>() where T : EntityTypeEnumerator =>
            typeof(T).GetProperties(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                        .Select(f => f.GetValue(null))
                        .Cast<T>();

        public static T FromCode<T>(EntityTypeId entityTypeId) where T : EntityTypeEnumerator
        {
            var matchingItem = Parse<T, string>(entityTypeId, "kod", item => item.Code == entityTypeId);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : EntityTypeEnumerator
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem! == null!)
            {
                throw new InvalidOperationException($"'{value}' is not found in '{typeof(T)}' ({description})");
            }

            return matchingItem;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as EntityTypeEnumerator);
        }

        public bool Equals(EntityTypeEnumerator? other)
        {
            return other is not null &&
                   EqualityComparer<EntityTypeId>.Default.Equals(Code, other.Code);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Code);
        }

        public static bool operator ==(EntityTypeEnumerator? left, EntityTypeEnumerator? right)
        {
            return EqualityComparer<EntityTypeEnumerator>.Default.Equals(left, right);
        }

        public static bool operator !=(EntityTypeEnumerator? left, EntityTypeEnumerator? right)
        {
            return !(left == right);
        }
    }
}
