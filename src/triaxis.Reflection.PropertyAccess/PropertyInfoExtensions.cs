using System;
using triaxis.Reflection;

namespace System.Reflection
{
    /// <summary>
    /// Helpers for quickly creating an <see cref="IPropertyAccessor"/> from <see cref="PropertyInfo"/>
    /// </summary>
    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// Gets an interface for efficient retrieval of the specified property
        /// from arbitrary objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IPropertyGetter GetGetter(this PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanRead)
            {
                throw new ArgumentException("Property is not readable", nameof(propertyInfo));
            }

            return GetManipulator(propertyInfo);
        }

        /// <summary>
        /// Gets an interface for efficient retrieval of the specified property
        /// from arbitrary objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IPropertyGetter<TValue> GetGetter<TValue>(this PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanRead)
            {
                throw new ArgumentException("Property is not readable", nameof(propertyInfo));
            }

            if (!typeof(TValue).IsAssignableFrom(propertyInfo.PropertyType))
            {
                throw new ArgumentException($"{typeof(TValue)} is not assignable from {propertyInfo.PropertyType}");
            }

            return (IPropertyGetter<TValue>)GetManipulator(propertyInfo);
        }


        /// <summary>
        /// Gets an interface for efficient retrieval of the specified property
        /// of value type objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IValueTypePropertyGetter<TTarget> GetValueTypeGetter<TTarget>(this PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanRead)
            {
                throw new ArgumentException("Property is not readable", nameof(propertyInfo));
            }

            return (IValueTypePropertyGetter<TTarget>)GetManipulator(propertyInfo);
        }

        /// <summary>
        /// Gets an interface for efficient modification of the specified property
        /// on arbitrary objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IPropertySetter GetSetter(this PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanWrite)
            {
                throw new ArgumentException("Property is not writable", nameof(propertyInfo));
            }

            return GetManipulator(propertyInfo);
        }

        /// <summary>
        /// Gets an interface for efficient modification of the specified property
        /// on arbitrary objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IPropertySetter<TValue> GetSetter<TValue>(this PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanWrite)
            {
                throw new ArgumentException("Property is not writable", nameof(propertyInfo));
            }

            if (!propertyInfo.PropertyType.IsAssignableFrom(typeof(TValue)))
            {
                throw new ArgumentException($"{propertyInfo.PropertyType} is not assignable from {typeof(TValue)}");
            }

            return (IPropertySetter<TValue>)GetManipulator(propertyInfo);
        }

        /// <summary>
        /// Gets an interface for efficient modification of the specified property
        /// on value type objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IValueTypePropertySetter<TTarget> GetValueTypeSetter<TTarget>(this PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanWrite)
            {
                throw new ArgumentException("Property is not writable", nameof(propertyInfo));
            }

            if (!typeof(TTarget).IsAssignableFrom(propertyInfo.DeclaringType))
            {
                throw new ArgumentException($"{typeof(TTarget)} is not assignable from {propertyInfo.DeclaringType}");
            }

            return (IValueTypePropertySetter<TTarget>)GetManipulator(propertyInfo);
        }

        /// <summary>
        /// Gets an interface for efficient retrieval and modification of the specified property
        /// from arbitrary objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IPropertyManipulator GetManipulator(this PropertyInfo propertyInfo)
        {
            return PropertyAccessorCache.Get(propertyInfo);
        }

        /// <summary>
        /// Gets an interface for efficient retrieval and modification of the specified property
        /// from arbitrary objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IPropertyManipulator<TValue> GetManipulator<TValue>(this PropertyInfo propertyInfo)
        {
            if (!typeof(TValue).Equals(propertyInfo.PropertyType))
            {
                throw new ArgumentException($"{typeof(TValue)} != {propertyInfo.PropertyType}");
            }

            bool isStatic = (propertyInfo.GetMethod ?? propertyInfo.SetMethod).IsStatic;

            if (isStatic)
            {
                return new StaticPropertyAccessor<TValue>(propertyInfo);
            }

            var type = typeof(ReferenceTypePropertyAccessor<,>).MakeGenericType(propertyInfo.DeclaringType, typeof(TValue));
            return (IPropertyManipulator<TValue>)Activator.CreateInstance(type, propertyInfo);
        }
    }
}
