using System;
using triaxis.Reflection;

namespace System.Reflection
{
    /// <summary>
    /// Helpers for quickly creating an <see cref="IPropertyAccessor"/> from <see cref="MemberInfo"/>
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Gets an interface for efficient retrieval of the specified member
        /// from arbitrary objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IPropertyGetter GetGetter(this MemberInfo memberInfo)
        {
            return memberInfo switch
            {
                FieldInfo fi => fi.GetGetter(),
                PropertyInfo pi => pi.GetGetter(),
                _ => throw new NotSupportedException()
            };
        }

        /// <summary>
        /// Gets an interface for efficient retrieval of the specified member
        /// from arbitrary objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IPropertyGetter<TValue> GetGetter<TValue>(this MemberInfo memberInfo)
        {
            return memberInfo switch
            {
                FieldInfo fi => fi.GetGetter<TValue>(),
                PropertyInfo pi => pi.GetGetter<TValue>(),
                _ => throw new NotSupportedException()
            };
        }


        /// <summary>
        /// Gets an interface for efficient retrieval of the specified member
        /// of value type objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IValueTypePropertyGetter<TTarget> GetValueTypeGetter<TTarget>(this MemberInfo memberInfo)
        {
            return memberInfo switch
            {
                FieldInfo fi => fi.GetValueTypeGetter<TTarget>(),
                PropertyInfo pi => pi.GetValueTypeGetter<TTarget>(),
                _ => throw new NotSupportedException()
            };
        }

        /// <summary>
        /// Gets an interface for efficient modification of the specified member
        /// on arbitrary objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IPropertySetter GetSetter(this MemberInfo memberInfo)
        {
            return memberInfo switch
            {
                FieldInfo fi => fi.GetSetter(),
                PropertyInfo pi => pi.GetSetter(),
                _ => throw new NotSupportedException()
            };
        }

        /// <summary>
        /// Gets an interface for efficient modification of the specified member
        /// on arbitrary objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IPropertySetter<TValue> GetSetter<TValue>(this MemberInfo memberInfo)
        {
            return memberInfo switch
            {
                FieldInfo fi => fi.GetSetter<TValue>(),
                PropertyInfo pi => pi.GetSetter<TValue>(),
                _ => throw new NotSupportedException()
            };
        }

        /// <summary>
        /// Gets an interface for efficient modification of the specified member
        /// on value type objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IValueTypePropertySetter<TTarget> GetValueTypeSetter<TTarget>(this MemberInfo memberInfo)
        {
            return memberInfo switch
            {
                FieldInfo fi => fi.GetValueTypeSetter<TTarget>(),
                PropertyInfo pi => pi.GetValueTypeSetter<TTarget>(),
                _ => throw new NotSupportedException()
            };
        }

        /// <summary>
        /// Gets an interface for efficient retrieval and modification of the specified member
        /// from arbitrary objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IPropertyManipulator GetManipulator(this MemberInfo memberInfo)
        {
            return memberInfo switch
            {
                FieldInfo fi => fi.GetManipulator(),
                PropertyInfo pi => pi.GetManipulator(),
                _ => throw new NotSupportedException()
            };
        }

        /// <summary>
        /// Gets an interface for efficient retrieval and modification of the specified member
        /// from arbitrary objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IPropertyManipulator<TValue> GetManipulator<TValue>(this MemberInfo memberInfo)
        {
            return memberInfo switch
            {
                FieldInfo fi => fi.GetManipulator<TValue>(),
                PropertyInfo pi => pi.GetManipulator<TValue>(),
                _ => throw new NotSupportedException()
            };
        }


        /// <summary>
        /// Gets an interface for efficient retrieval and modification of the specified member
        /// from arbitrary objects without using reflection (e.g. in a loop)
        /// </summary>
        public static IPropertyManipulator<TTarget, TValue> GetManipulator<TTarget, TValue>(this MemberInfo memberInfo)
        {
            return memberInfo switch
            {
                FieldInfo fi => fi.GetManipulator<TTarget, TValue>(),
                PropertyInfo pi => pi.GetManipulator<TTarget, TValue>(),
                _ => throw new NotSupportedException()
            };
        }
    }
}
