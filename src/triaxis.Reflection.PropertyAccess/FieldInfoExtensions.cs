using System;
using triaxis.Reflection;

#if NETSTANDARD2_1_OR_GREATER

namespace System.Reflection
{
    /// <summary>
    /// Helpers for quickly creating an <see cref="IPropertyAccessor"/> from <see cref="FieldInfo"/>
    /// </summary>
    public static class FieldInfoExtensions
    {
        /// <summary>
        /// Gets an interface for efficient retrieval of the specified field
        /// from arbitrary objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IPropertyGetter GetGetter(this FieldInfo fieldInfo)
        {
            return GetManipulator(fieldInfo);
        }

        /// <summary>
        /// Gets an interface for efficient retrieval of the specified property
        /// from arbitrary objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IPropertyGetter<TValue> GetGetter<TValue>(this FieldInfo fieldInfo)
        {
            if (!typeof(TValue).IsAssignableFrom(fieldInfo.FieldType))
            {
                throw new ArgumentException($"{typeof(TValue)} is not assignable from {fieldInfo.FieldType}");
            }

            return (IPropertyGetter<TValue>)GetManipulator(fieldInfo);
        }


        /// <summary>
        /// Gets an interface for efficient retrieval of the specified property
        /// of value type objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IValueTypePropertyGetter<TTarget> GetValueTypeGetter<TTarget>(this FieldInfo fieldInfo)
        {
            return (IValueTypePropertyGetter<TTarget>)GetManipulator(fieldInfo);
        }

        /// <summary>
        /// Gets an interface for efficient modification of the specified property
        /// on arbitrary objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IPropertySetter GetSetter(this FieldInfo fieldInfo)
        {
            return GetManipulator(fieldInfo);
        }

        /// <summary>
        /// Gets an interface for efficient modification of the specified property
        /// on arbitrary objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IPropertySetter<TValue> GetSetter<TValue>(this FieldInfo fieldInfo)
        {
            if (!fieldInfo.FieldType.IsAssignableFrom(typeof(TValue)))
            {
                throw new ArgumentException($"{fieldInfo.FieldType} is not assignable from {typeof(TValue)}");
            }

            return (IPropertySetter<TValue>)GetManipulator(fieldInfo);
        }

        /// <summary>
        /// Gets an interface for efficient modification of the specified property
        /// on value type objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IValueTypePropertySetter<TTarget> GetValueTypeSetter<TTarget>(this FieldInfo fieldInfo)
        {
            if (!typeof(TTarget).IsAssignableFrom(fieldInfo.DeclaringType))
            {
                throw new ArgumentException($"{typeof(TTarget)} is not assignable from {fieldInfo.DeclaringType}");
            }

            return (IValueTypePropertySetter<TTarget>)GetManipulator(fieldInfo);
        }

        /// <summary>
        /// Gets an interface for efficient retrieval and modification of the specified property
        /// from arbitrary objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IPropertyManipulator GetManipulator(this FieldInfo fieldInfo)
        {
            return FieldAccessorCache.Get(fieldInfo);
        }


        /// <summary>
        /// Gets an interface for efficient retrieval and modification of the specified property
        /// from arbitrary objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IPropertyManipulator<TValue> GetManipulator<TValue>(this FieldInfo fieldInfo)
        {
            if (!typeof(TValue).Equals(fieldInfo.FieldType))
            {
                throw new ArgumentException($"{typeof(TValue)} != {fieldInfo.FieldType}");
            }

            return (IPropertyManipulator<TValue>)FieldAccessorCache.Get(fieldInfo);
        }

        /// <summary>
        /// Gets an interface for efficient retrieval and modification of the specified property
        /// from arbitrary objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IPropertyManipulator<TTarget, TValue> GetManipulator<TTarget, TValue>(this FieldInfo fieldInfo)
        {
            if (!typeof(TTarget).Equals(fieldInfo.DeclaringType))
            {
                throw new ArgumentException($"{typeof(TTarget)} != {fieldInfo.DeclaringType}");
            }

            if (!typeof(TValue).Equals(fieldInfo.FieldType))
            {
                throw new ArgumentException($"{typeof(TValue)} != {fieldInfo.FieldType}");
            }

            return (IPropertyManipulator<TTarget, TValue>)FieldAccessorCache.Get(fieldInfo);
        }
    }
}

#endif
