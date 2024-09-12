namespace Kubis1982.Shared.Kernel.Types
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public static class SpecificationExtensions
    {
        public static ISpecificationBuilder<T> Where<T>(this ISpecificationBuilder<T> specBuilder, Expression<Func<T, bool>> criteria) where T : class
        {
            Ardalis.Specification.ISpecificationBuilder<T> internalSpecificationBuilder = (Ardalis.Specification.ISpecificationBuilder<T>)specBuilder.InternalSpecificationBuilder;
            return new SpecificationBuilder<T>(Ardalis.Specification.SpecificationBuilderExtensions.Where(internalSpecificationBuilder, criteria));
        }

        public static ISpecificationBuilder<T> OrderBy<T>(this ISpecificationBuilder<T> specBuilder, Expression<Func<T, object?>> orderByExpression) where T : class
        {
            Ardalis.Specification.ISpecificationBuilder<T> internalSpecificationBuilder = (Ardalis.Specification.ISpecificationBuilder<T>)specBuilder.InternalSpecificationBuilder;
            return new SpecificationBuilder<T>(Ardalis.Specification.SpecificationBuilderExtensions.OrderBy(internalSpecificationBuilder, orderByExpression));
        }

        public static ISpecificationBuilder<T> OrderByDescending<T>(this ISpecificationBuilder<T> specBuilder, Expression<Func<T, object?>> orderByExpression) where T : class
        {
            Ardalis.Specification.ISpecificationBuilder<T> internalSpecificationBuilder = (Ardalis.Specification.ISpecificationBuilder<T>)specBuilder.InternalSpecificationBuilder;
            return new SpecificationBuilder<T>(Ardalis.Specification.SpecificationBuilderExtensions.OrderByDescending(internalSpecificationBuilder, orderByExpression));
        }

        public static ISpecificationBuilder<T> AsSplitQuery<T>(this ISpecificationBuilder<T> specBuilder) where T : class
        {
            Ardalis.Specification.ISpecificationBuilder<T> internalSpecificationBuilder = (Ardalis.Specification.ISpecificationBuilder<T>)specBuilder.InternalSpecificationBuilder;
            return new SpecificationBuilder<T>(Ardalis.Specification.SpecificationBuilderExtensions.AsSplitQuery(internalSpecificationBuilder));
        }

        public static IIncludableSpecificationBuilder<T, TProperty> Include<T, TProperty>(this ISpecificationBuilder<T> specBuilder, Expression<Func<T, TProperty>> includeExpression) where T : class
        {
            Ardalis.Specification.ISpecificationBuilder<T> internalSpecificationBuilder = (Ardalis.Specification.ISpecificationBuilder<T>)specBuilder.InternalSpecificationBuilder;
            return new IncludableSpecificationBuilder<T, TProperty>(Ardalis.Specification.SpecificationBuilderExtensions.Include(internalSpecificationBuilder, includeExpression));
        }

        public static IIncludableSpecificationBuilder<T, TProperty> ThenInclude<T, TPreviousProperty, TProperty>(this IIncludableSpecificationBuilder<T, IReadOnlyCollection<TPreviousProperty>> previusBuilder, Expression<Func<TPreviousProperty, TProperty>> thenIncludeExpression) where T : class
        {
            var internalSpecificationBuilder = (Ardalis.Specification.IIncludableSpecificationBuilder<T, IReadOnlyCollection<TPreviousProperty>>)previusBuilder.InternalSpecificationBuilder;
            return new IncludableSpecificationBuilder<T, TProperty>(Ardalis.Specification.IncludableBuilderExtensions.ThenInclude(internalSpecificationBuilder, thenIncludeExpression));
        }

        public static IIncludableSpecificationBuilder<T, TProperty> ThenInclude<T, TPreviousProperty, TProperty>(this IIncludableSpecificationBuilder<T, IEnumerable<TPreviousProperty>> previusBuilder, Expression<Func<TPreviousProperty, TProperty>> thenIncludeExpression) where T : class
        {
            var internalSpecificationBuilder = (Ardalis.Specification.IIncludableSpecificationBuilder<T, IEnumerable<TPreviousProperty>>)previusBuilder.InternalSpecificationBuilder;
            return new IncludableSpecificationBuilder<T, TProperty>(Ardalis.Specification.IncludableBuilderExtensions.ThenInclude(internalSpecificationBuilder, thenIncludeExpression));
        }

        public static IIncludableSpecificationBuilder<T, TProperty> ThenInclude<T, TPreviousProperty, TProperty>(this IIncludableSpecificationBuilder<T, TPreviousProperty> previusBuilder, Expression<Func<TPreviousProperty, TProperty>> thenIncludeExpression) where T : class
        {
            var internalSpecificationBuilder = (Ardalis.Specification.IIncludableSpecificationBuilder<T, TPreviousProperty>)previusBuilder.InternalSpecificationBuilder;
            return new IncludableSpecificationBuilder<T, TProperty>(Ardalis.Specification.IncludableBuilderExtensions.ThenInclude(internalSpecificationBuilder, thenIncludeExpression));
        }
    }
}
