﻿namespace ModularMonolith.Shared.Kernel.Types
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    [DebuggerDisplay("{Key}: {Name}")]
    public abstract class Enumeration<TKey> : IComparable where TKey : IComparable, IConvertible
    {
        public string Name { get; private set; }

        public TKey Key { get; private set; }

        protected Enumeration(TKey key, string name) => (Key, Name) = (key, name);

        public override string ToString() => $"{Key}: {Name}";

        public override bool Equals(object? obj)
        {
            if (obj is not Enumeration<TKey> otherValue)
            {
                return false;
            }
            return Key?.Equals(otherValue.Key) ?? false;
        }

        public override int GetHashCode() => Key!.GetHashCode();

        public int CompareTo(object? obj) => Key.CompareTo((obj as Enumeration<TKey>)!.Key ?? default);

        public static bool operator ==(Enumeration<TKey> left, Enumeration<TKey> right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(Enumeration<TKey> left, Enumeration<TKey> right)
        {
            return !(left == right);
        }

        public static bool operator <(Enumeration<TKey> left, Enumeration<TKey> right)
        {
            return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0;
        }

        public static bool operator <=(Enumeration<TKey> left, Enumeration<TKey> right)
        {
            return ReferenceEquals(left, null) || left.CompareTo(right) <= 0;
        }

        public static bool operator >(Enumeration<TKey> left, Enumeration<TKey> right)
        {
            return !ReferenceEquals(left, null) && left.CompareTo(right) > 0;
        }

        public static bool operator >=(Enumeration<TKey> left, Enumeration<TKey> right)
        {
            return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0;
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration<TKey> =>
            typeof(T).GetProperties(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                        .Select(f => f.GetValue(null))
                        .Cast<T>();

        public static T? GetOrDefault<T>(string propertyName) where T : Enumeration<TKey>
        {
            object? ob = typeof(T).GetProperties(BindingFlags.Public |
                                 BindingFlags.Static |
                                 BindingFlags.DeclaredOnly)
                .FirstOrDefault(n => n.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase))?
                .GetValue(null);

            if (ob == null)
            { return null; }

            return (T?)ob;
        }

        public static T Get<T>(string propertyName) where T : Enumeration<TKey>
        {
            return GetOrDefault<T>(propertyName) ?? throw new ArgumentException($"Nie znaleziono własności `{propertyName}` na `{typeof(T).Name}`");
        }

        public static T FromValue<T>(TKey value) where T : Enumeration<TKey>
        {
            var matchingItem = GetAll<T>().FirstOrDefault(item => item.Key.Equals(value));
            return matchingItem ?? throw new InvalidOperationException($"'{value}' nie została znaleziona w {typeof(T)}");
        }
    }
}
