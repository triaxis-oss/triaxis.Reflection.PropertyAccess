using System;
using System.Reflection;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;

namespace triaxis.Reflection.PropertyAccess.Benchmarks
{
    [IterationTime(100), WarmupCount(3), IterationCount(3)]
    public class Benchmarks
    {
        public class ReferenceTarget
        {
            public int ValueProperty { get; set; }
            public string ReferenceProperty { get; set; }
            public int ValueField;
            public string ReferenceField;
        }

        public struct ValueTarget
        {
            public int ValueProperty { get; set; }
            public string ReferenceProperty { get; set; }
            public int ValueField;
            public string ReferenceField;
        }

        public static class StaticTarget
        {
            public static int ValueProperty { get; set; }
            public static string ReferenceProperty { get; set; }
            public static int ValueField;
            public static string ReferenceField;
        }

        static readonly PropertyInfo rtVpI = typeof(ReferenceTarget).GetProperty("ValueProperty");
        static readonly FieldInfo rtVfI = typeof(ReferenceTarget).GetField("ValueField");
        static readonly PropertyInfo rtRpI = typeof(ReferenceTarget).GetProperty("ReferenceProperty");
        static readonly FieldInfo rtRfI = typeof(ReferenceTarget).GetField("ReferenceField");
        static readonly PropertyInfo vtVpI = typeof(ValueTarget).GetProperty("ValueProperty");
        static readonly FieldInfo vtVfI = typeof(ValueTarget).GetField("ValueField");
        static readonly PropertyInfo vtRpI = typeof(ValueTarget).GetProperty("ReferenceProperty");
        static readonly FieldInfo vtRfI = typeof(ValueTarget).GetField("ReferenceField");
        static readonly PropertyInfo stVpI = typeof(StaticTarget).GetProperty("ValueProperty");
        static readonly FieldInfo stVfI = typeof(StaticTarget).GetField("ValueField");
        static readonly PropertyInfo stRpI = typeof(StaticTarget).GetProperty("ReferenceProperty");
        static readonly FieldInfo stRfI = typeof(StaticTarget).GetField("ReferenceField");

        static readonly IPropertyManipulator<int> rtVpM = rtVpI.GetManipulator<int>();
        static readonly IPropertyManipulator<int> rtVfM = rtVfI.GetManipulator<int>();
        static readonly IPropertyManipulator<ValueTarget, int> vtVpM = vtVpI.GetManipulator<ValueTarget, int>();
        static readonly IPropertyManipulator<ValueTarget, int> vtVfM = vtVfI.GetManipulator<ValueTarget, int>();
        static readonly IPropertyManipulator<int> stVpM = stVpI.GetManipulator<int>();
        static readonly IPropertyManipulator<int> stVfM = stVfI.GetManipulator<int>();

        public ReferenceTarget rt = new();
        public ValueTarget vt = new();

        [Benchmark]
        public void ReferenceTargetValuePropertyInfo()
        {
            rtVpI.SetValue(rt, rtVpI.GetValue(rt));
        }

        [Benchmark]
        public void ReferenceTargetValuePropertyAccessor()
        {
            rtVpM.Set(rt, rtVpM.Get(rt));
        }

        [Benchmark]
        public void ReferenceTargetValueFieldInfo()
        {
            rtVfI.SetValue(rt, rtVfI.GetValue(rt));
        }

        [Benchmark]
        public void ReferenceTargetValueFieldAccessor()
        {
            rtVfM.Set(rt, rtVfM.Get(rt));
        }

        [Benchmark]
        public void ValueTargetValuePropertyInfo()
        {
            vtVpI.SetValue(vt, vtVpI.GetValue(vt));
        }

        [Benchmark]
        public void ValueTargetValuePropertyAccessor()
        {
            vtVpM.Set(vt, vtVpM.Get(vt));
        }

        [Benchmark]
        public void ValueTargetValueFieldInfo()
        {
            vtVfI.SetValue(vt, vtVfI.GetValue(vt));
        }

        [Benchmark]
        public void ValueTargetValueFieldAccessor()
        {
            vtVfM.Set(vt, vtVfM.Get(vt));
        }

        [Benchmark]
        public void StaticTargetValuePropertyInfo()
        {
            stVpI.SetValue(null, stVpI.GetValue(null));
        }

        [Benchmark]
        public void StaticTargetValuePropertyAccessor()
        {
            stVpM.Set(null, stVpM.Get(null));
        }

        [Benchmark]
        public void StaticTargetValueFieldInfo()
        {
            stVfI.SetValue(null, stVfI.GetValue(null));
        }

        [Benchmark]
        public void StaticTargetValueFieldAccessor()
        {
            stVfM.Set(null, stVfM.Get(null));
        }
    }
}
