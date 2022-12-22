using System;
using System.Reflection;
using System.Reflection.Emit;

#if NETSTANDARD2_1_OR_GREATER

namespace triaxis.Reflection
{
    /// <summary>
    /// Provides efficient access to a static property
    /// </summary>
    internal class StaticFieldAccessor<TValue> : IPropertyManipulator<TValue>
    {
        /// <summary>
        /// Delegate for reading of the property value
        /// </summary>
        public delegate TValue GetterDelegate();
        /// <summary>
        /// Delegate for writing of the property value
        /// </summary>
        public delegate void SetterDelegate(TValue value);

        private readonly FieldInfo _fieldInfo;
        private readonly GetterDelegate _getter;
        private readonly SetterDelegate _setter;

        public StaticFieldAccessor(FieldInfo fieldInfo)
        {
            System.Diagnostics.Debug.Assert(typeof(TValue) == fieldInfo.FieldType);

            _fieldInfo = fieldInfo;

            _getter = (GetterDelegate)new DynamicMethod($"get_{fieldInfo.Name}", typeof(TValue), Type.EmptyTypes, fieldInfo.DeclaringType, true)
                .WithIl(il =>
                {
                    il.Emit(OpCodes.Ldsfld, fieldInfo);
                    il.Emit(OpCodes.Ret);
                })
                .CreateDelegate(typeof(GetterDelegate));

            _setter = (SetterDelegate)new DynamicMethod($"set_{fieldInfo.Name}", typeof(void), new[] { typeof(TValue) }, fieldInfo.DeclaringType, true)
                .WithIl(il =>
                {
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Stsfld, fieldInfo);
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
            => _getter();
        TValue IPropertyGetter<TValue>.Get(object target)
            => _getter();

        void IPropertySetter.Set(object target, object value)
            => _setter((TValue)value);
        void IPropertySetter<TValue>.Set(object target, TValue value)
            => _setter(value);
    }
}

#endif
