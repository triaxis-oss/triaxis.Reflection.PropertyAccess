using System;
using System.Collections.Concurrent;
using System.Reflection;

#if NETSTANDARD2_1_OR_GREATER

namespace triaxis.Reflection
{
    internal class FieldAccessorCache
    {
        private static readonly ConcurrentDictionary<FieldInfo, IPropertyManipulator> s_cache =
            new ConcurrentDictionary<FieldInfo, IPropertyManipulator>();

        public static IPropertyManipulator Get(FieldInfo fieldInfo)
            => s_cache.GetOrAdd(fieldInfo, fi =>
            {
                Type type;
                if (fieldInfo.IsStatic)
                {
                    type = typeof(StaticFieldAccessor<>).MakeGenericType(fi.FieldType);
                }
                else if (fi.DeclaringType.IsValueType)
                {
                    type = typeof(ValueTypeFieldAccessor<,>).MakeGenericType(fi.DeclaringType, fi.FieldType);
                }
                else
                {
                    type = typeof(ReferenceTypeFieldAccessor<,>).MakeGenericType(fi.DeclaringType, fi.FieldType);
                }
                return (IPropertyManipulator)Activator.CreateInstance(type, fieldInfo);
            });
    }
}

#endif
