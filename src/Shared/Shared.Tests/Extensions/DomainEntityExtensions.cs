namespace ModularMonolith.Shared.Extensions
{
    using ModularMonolith.Shared.Kernel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class DomainEntityExtensions
    {
        public static EntityExtensions<TEntity> Extensions<TEntity>(this TEntity domainEntity) where TEntity : IDomainEntity => new(domainEntity);
    }

    public class EntityExtensions<TEntity>(TEntity domainEntity) where TEntity : IDomainEntity
    {
        public TEntity DomainEntity { get; } = domainEntity;

        public EntityExtensions<TEntity> SetValue<TProperty>(Expression<Func<TEntity, TProperty>> property, TProperty value)
        {
            var propertyInfo = property.GetPropertyInfoFromExpression();
            string fieldName = char.ToLower(propertyInfo.Name[0]) + propertyInfo.Name.Substring(1);
            FieldInfo fieldInfo = propertyInfo!.DeclaringType!.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic)!;
            if (fieldInfo == null)
            {
                propertyInfo.SetValue(DomainEntity, value);
                return this;
            }
            fieldInfo.SetValue(DomainEntity, value);
            return this;
        }

        public EntityExtensions<TEntity> SetList<TProperty>(Expression<Func<TEntity, IReadOnlyCollection<TProperty>>> property, List<TProperty> list)
        {
            var propertyInfo = property.GetPropertyInfoFromExpression();
            string fieldName = propertyInfo.Name.ToLower();
            FieldInfo fieldInfo = propertyInfo!.DeclaringType!.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic)!;
            fieldInfo.SetValue(DomainEntity, list);
            return this;
        }

        public TEvent GetEvent<TEvent>() where TEvent : IDomainEvent
        {
            TEvent? @event = DomainEntity.Events.OfType<TEvent>().SingleOrDefault();
            return @event ?? throw new ArgumentException($"Nie znaleziono zdarzenia {typeof(TEvent).Name}");
        }

        public bool HasEvent<TEvent>() where TEvent : IDomainEvent
        {
            return DomainEntity.Events.OfType<TEvent>().Any();
        }
    }
}
