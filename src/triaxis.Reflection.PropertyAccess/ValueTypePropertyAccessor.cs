using System;
using System.Reflection;

namespace triaxis.Reflection
{
    internal class ValueTypePropertyAccessor<TTarget, TValue> : IPropertyManipulator<TTarget, TValue>
        where TTarget : struct
    {
        private delegate TValue GetterDelegate(ref TTarget target);
        private delegate void SetterDelegate(ref TTarget target, TValue value);

        private readonly PropertyInfo _propertyInfo;
        private readonly GetterDelegate _getter;
        private readonly SetterDelegate _setter;

        public ValueTypePropertyAccessor(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;

            var getMethod = propertyInfo.GetGetMethod();
            var setMethod = propertyInfo.GetSetMethod();
            if (getMethod != null)
            {
                _getter = (GetterDelegate)Delegate.CreateDelegate(typeof(GetterDelegate), getMethod);
            }
            if (setMethod != null)
            {
                _setter = (SetterDelegate)Delegate.CreateDelegate(typeof(SetterDelegate), setMethod);
            }
        }

        public PropertyInfo Property => _propertyInfo;
        public bool CanRead => _getter != null;
        public bool CanWrite => _setter != null;

        object IPropertyGetter.Get(object target)
        {
            TTarget unboxed = (TTarget)target;
            return _getter(ref unboxed);
        }

        TValue IPropertyGetter<TValue>.Get(object target)
        {
            TTarget unboxed = (TTarget)target;
            return _getter(ref unboxed);
        }

        object IValueTypePropertyGetter<TTarget>.Get(ref TTarget target)
            => _getter(ref target);
        TValue IValueTypePropertyGetter<TTarget, TValue>.Get(ref TTarget target)
            => _getter(ref target);
        TValue IPropertyGetter<TTarget, TValue>.Get(TTarget target)
            => _getter(ref target);

        void IPropertySetter.Set(object target, object value)
            => throw new NotSupportedException("Cannot set property of a boxed value type");
        void IPropertySetter<TValue>.Set(object target, TValue value)
            => throw new NotSupportedException("Cannot set property of a boxed value type");
        void IValueTypePropertySetter<TTarget>.Set(ref TTarget target, object value)
            => _setter(ref target, (TValue)value);
        void IValueTypePropertySetter<TTarget, TValue>.Set(ref TTarget target, TValue value)
            => _setter(ref target, value);
        void IPropertySetter<TTarget, TValue>.Set(TTarget target, TValue value)
            => _setter(ref target, value);
    }
}
