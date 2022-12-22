using System;
using System.Reflection;
using System.Reflection.Emit;

#if NETSTANDARD2_1_OR_GREATER

namespace triaxis.Reflection
{
    /// <summary>
    /// Provides efficient access to one property of an object of the specified type
    /// </summary>
    internal class ReferenceTypeFieldAccessor<TTarget, TValue> : IPropertyManipulator<TTarget, TValue>
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

        private readonly FieldInfo _fieldInfo;
        private readonly GetterDelegate _getter;
        private readonly SetterDelegate _setter;

        public ReferenceTypeFieldAccessor(FieldInfo fieldInfo)
        {
            _fieldInfo = fieldInfo;

            _getter = (GetterDelegate)new DynamicMethod($"get_{fieldInfo.Name}", typeof(TValue), new[] { typeof(TTarget) }, fieldInfo.DeclaringType, true)
                .WithIl(il =>
                {
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldfld, fieldInfo);
                    il.Emit(OpCodes.Ret);
                })
                .CreateDelegate(typeof(GetterDelegate));

            _setter = (SetterDelegate)new DynamicMethod($"set_{fieldInfo.Name}", typeof(void), new[] { typeof(TTarget), typeof(TValue) }, fieldInfo.DeclaringType, true)
                .WithIl(il =>
                {
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldarg_1);
                    il.Emit(OpCodes.Stfld, fieldInfo);
                    il.Emit(OpCodes.Ret);
                })
                .CreateDelegate(typeof(SetterDelegate));
        }

        PropertyInfo IPropertyAccessor.Property
            => null;
        public FieldInfo Field
            => _fieldInfo;
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

#endif
