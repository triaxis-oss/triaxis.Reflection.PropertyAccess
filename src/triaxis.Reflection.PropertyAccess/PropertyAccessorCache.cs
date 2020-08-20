using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace triaxis.Reflection
{
    internal class PropertyAccessorCache
    {
        private static readonly ConcurrentDictionary<PropertyInfo, IPropertyManipulator> s_cache =
            new ConcurrentDictionary<PropertyInfo, IPropertyManipulator>();

        public static IPropertyManipulator Get(PropertyInfo propertyInfo)
            => s_cache.GetOrAdd(propertyInfo, pi =>
            {
                Type type;
                if ((pi.GetMethod ?? pi.SetMethod).IsStatic)
                {
                    type = typeof(StaticPropertyAccessor<>).MakeGenericType(pi.PropertyType);
                }
                else if (pi.DeclaringType.IsValueType)
                {
                    type = typeof(ValueTypePropertyAccessor<,>).MakeGenericType(pi.DeclaringType, pi.PropertyType);
                }
                else
                {
                    type = typeof(ReferenceTypePropertyAccessor<,>).MakeGenericType(pi.DeclaringType, pi.PropertyType);
                }
                return (IPropertyManipulator)Activator.CreateInstance(type, propertyInfo);
            });
    }
}
