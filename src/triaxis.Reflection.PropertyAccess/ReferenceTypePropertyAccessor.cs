using System;
using System.Reflection;

namespace triaxis.Reflection
{
     /// <summary>
    /// Provides efficient access to one property of an object of the specified type
    /// </summary>
    internal class ReferenceTypePropertyAccessor<TTarget, TValue> : IPropertyManipulator<TTarget, TValue>
        where TTarget : class
    {
        /// <summary>
        /// Delegate for reading of the property value
        /// </summary>
        public delegate TValue GetterDelegate(TTarget target);
        /// <summary>
        /// Delegate for writing of the property value
        /// </summary>
        public delegate void SetterDelegate(TTarget target, TValue value);

        private readonly PropertyInfo _propertyInfo;
        private readonly GetterDelegate _getter;
        private readonly SetterDelegate _setter;

        /// <summary>
        /// Creates a <see cref="ReferenceTypePropertyAccessor{TTarget,TValue}" /> from the specified <see cref="PropertyInfo" />
        /// </summary>
        public ReferenceTypePropertyAccessor(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;

            var getMethod = propertyInfo.GetGetMethod(true);
            var setMethod = propertyInfo.GetSetMethod(true);
            if (getMethod != null)
            {
                _getter = (GetterDelegate)Delegate.CreateDelegate(typeof(GetterDelegate), getMethod);
            }
            if (setMethod != null)
            {
                _setter = (SetterDelegate)Delegate.CreateDelegate(typeof(SetterDelegate), setMethod);
            }
        }

        public PropertyInfo Property
            => _propertyInfo;
        public bool CanRead
            => _getter != null;
        public bool CanWrite
            => _setter != null;

        object IPropertyGetter.Get(object target)
            => _getter((TTarget)target);
        TValue IPropertyGetter<TValue>.Get(object target)
            => _getter((TTarget)target);
        object IValueTypePropertyGetter<TTarget>.Get(ref TTarget target)
            => _getter(target);
        TValue IValueTypePropertyGetter<TTarget, TValue>.Get(ref TTarget target)
            => _getter(target);
        TValue IPropertyGetter<TTarget, TValue>.Get(TTarget target)
            => _getter(target);

        void IPropertySetter.Set(object target, object value)
            => _setter((TTarget)target, (TValue)value);
        void IPropertySetter<TValue>.Set(object target, TValue value)
            => _setter((TTarget)target, value);
        void IValueTypePropertySetter<TTarget>.Set(ref TTarget target, object value)
            => _setter(target, (TValue)value);
        void IValueTypePropertySetter<TTarget, TValue>.Set(ref TTarget target, TValue value)
            => _setter(target, value);
        void IPropertySetter<TTarget, TValue>.Set(TTarget target, TValue value)
            => _setter(target, value);
    }
}
