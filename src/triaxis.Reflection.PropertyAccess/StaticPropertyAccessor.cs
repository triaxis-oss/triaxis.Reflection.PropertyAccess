using System;
using System.Reflection;

namespace triaxis.Reflection
{
    /// <summary>
    /// Provides efficient access to a static property
    /// </summary>
    internal class StaticPropertyAccessor<TValue> : IPropertyManipulator<TValue>
    {
        /// <summary>
        /// Delegate for reading of the property value
        /// </summary>
        public delegate TValue GetterDelegate();
        /// <summary>
        /// Delegate for writing of the property value
        /// </summary>
        public delegate void SetterDelegate(TValue value);

        private readonly PropertyInfo _propertyInfo;
        private readonly GetterDelegate _getter;
        private readonly SetterDelegate _setter;

        /// <summary>
        /// Creates a <see cref="ReferenceTypePropertyAccessor{TTarget,TValue}" /> from the specified <see cref="PropertyInfo" />
        /// </summary>
        public StaticPropertyAccessor(PropertyInfo propertyInfo)
        {
            System.Diagnostics.Debug.Assert(typeof(TValue) == propertyInfo.PropertyType);

            _propertyInfo = propertyInfo;

            var getMethod = propertyInfo.GetMethod;
            var setMethod = propertyInfo.SetMethod;
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
            => _getter();
        TValue IPropertyGetter<TValue>.Get(object target)
            => _getter();

        void IPropertySetter.Set(object target, object value)
            => _setter((TValue)value);
        void IPropertySetter<TValue>.Set(object target, TValue value)
            => _setter(value);
    }
}
