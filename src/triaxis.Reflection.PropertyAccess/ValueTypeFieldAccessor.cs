using System;
using System.Reflection;
using System.Reflection.Emit;

#if NETSTANDARD2_1_OR_GREATER

namespace triaxis.Reflection
{
    internal class ValueTypeFieldAccessor<TTarget, TValue> : IPropertyManipulator<TTarget, TValue>
        where TTarget : struct
    {
        private delegate TValue GetterDelegate(ref TTarget target);
        private delegate void SetterDelegate(ref TTarget target, TValue value);

        private readonly FieldInfo _fieldInfo;
        private readonly GetterDelegate _getter;
        private readonly SetterDelegate _setter;

        public ValueTypeFieldAccessor(FieldInfo fieldInfo)
        {
            _fieldInfo = fieldInfo;

            _getter = (GetterDelegate)new DynamicMethod($"get_{fieldInfo.Name}", typeof(TValue), new[] { typeof(TTarget).MakeByRefType() }, fieldInfo.DeclaringType, true)
                .WithIl(il =>
                {
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldfld, fieldInfo);
                    il.Emit(OpCodes.Ret);
                })
                .CreateDelegate(typeof(GetterDelegate));

            _setter = (SetterDelegate)new DynamicMethod($"set_{fieldInfo.Name}", typeof(void), new[] { typeof(TTarget).MakeByRefType(), typeof(TValue) }, fieldInfo.DeclaringType, true)
                .WithIl(il =>
                {
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldarg_1);
                    il.Emit(OpCodes.Stfld, fieldInfo);
                    il.Emit(OpCodes.Ret);
                })
                .CreateDelegate(typeof(SetterDelegate));
        }

        PropertyInfo IPropertyAccessor.Property => null;
        public FieldInfo Field => _fieldInfo;
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

#endif
