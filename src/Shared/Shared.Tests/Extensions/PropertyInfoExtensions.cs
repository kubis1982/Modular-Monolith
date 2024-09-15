using System.Linq.Expressions;
using System.Reflection;

namespace ModularMonolith.Shared.Extensions;

public static class PropertyInfoExtensions
{
    public static PropertyInfo GetPropertyInfoFromExpression<TEntity, TProperty>(this Expression<Func<TEntity, TProperty>> propertyExpression)
    {
        if (propertyExpression == null)
        {
            throw new ArgumentNullException(nameof(propertyExpression));
        }

        MemberExpression? memberExpression = null;

        if (propertyExpression.Body is MemberExpression expression)
        {
            memberExpression = expression;
        }
        else if (propertyExpression.Body is UnaryExpression unaryExpression)
        {
            memberExpression = unaryExpression.Operand as MemberExpression;
        }

        if (memberExpression == null)
        {
            throw new ArgumentException("Invalid property expression", nameof(propertyExpression));
        }

        PropertyInfo? propertyInfo = memberExpression.Member as PropertyInfo;

        if (propertyInfo == null)
        {
            throw new ArgumentException("Invalid property expression", nameof(propertyExpression));
        }

        if (propertyInfo.DeclaringType == null)
        {
            throw new ArgumentException("Invalid property expression", nameof(propertyExpression));
        }

        if (!propertyInfo.DeclaringType.IsAssignableFrom(typeof(TEntity)))
        {
            throw new ArgumentException("Property does not belong to the specified entity type", nameof(propertyExpression));
        }

        if (propertyInfo.CanRead && propertyInfo.GetGetMethod(nonPublic: true) != null)
        {
            return propertyInfo;
        }

        return propertyInfo;
    }
}
