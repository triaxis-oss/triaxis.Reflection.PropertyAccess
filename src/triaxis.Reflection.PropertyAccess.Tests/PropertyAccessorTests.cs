using System;
using System.Reflection;
using NUnit.Framework;

namespace triaxis.Reflection.PropertyAccess.Tests
{
    public class PropertyAccessorTests
    {
        public class ReferenceTarget
        {
            public int Value { get; set; }
            public string Reference { get; set; }

            public static int StaticValue { get; set; }
            public static string StaticReference { get; set; }

            [Test]
            public void GetValueProperty([Random(1)] int value)
            {
                Value = value;
                Assert.That(GetType().GetProperty("Value").GetGetter().Get(this), Is.EqualTo(value));
            }

            [Test]
            public void GetValuePropertyWithValueAccessor([Random(1)] int value)
            {
                Value = value;
                var target = this;
                Assert.That(GetType().GetProperty("Value").GetValueTypeGetter<ReferenceTarget>().Get(ref target), Is.EqualTo(value));
            }

            [Test]
            public void GetReferenceProperty([Values("instanceTest")] string value)
            {
                Reference = value;
                Assert.That(GetType().GetProperty("Reference").GetGetter().Get(this), Is.EqualTo(value));
            }

            [Test]
            public void GetStaticValueProperty([Random(1)] int value)
            {
                StaticValue = value;
                Assert.That(GetType().GetProperty("StaticValue").GetGetter().Get(null), Is.EqualTo(value));
            }

            [Test]
            public void GetStaticReferenceProperty([Values("staticTest")] string value)
            {
                StaticReference = value;
                Assert.That(GetType().GetProperty("StaticReference").GetGetter().Get(null), Is.EqualTo(value));
            }

            [Test]
            public void SetValueProperty([Random(1)] int value)
            {
                GetType().GetProperty("Value").GetSetter().Set(this, value);
                Assert.That(Value, Is.EqualTo(value));
            }

            [Test]
            public void SetValuePropertyWithValueAccessor([Random(1)] int value)
            {
                var target = this;
                GetType().GetProperty("Value").GetValueTypeSetter<ReferenceTarget>().Set(ref target, value);
                Assert.That(Value, Is.EqualTo(value));
            }

            [Test]
            public void SetReferenceProperty([Values("instanceTest")] string value)
            {
                GetType().GetProperty("Reference").GetSetter().Set(this, value);
                Assert.That(Reference, Is.EqualTo(value));
            }

            [Test]
            public void SetStaticValueProperty([Random(1)] int value)
            {
                GetType().GetProperty("StaticValue").GetSetter().Set(null, value);
                Assert.That(StaticValue, Is.EqualTo(value));
            }

            [Test]
            public void SetStaticReferenceProperty([Values("staticTest")] string value)
            {
                GetType().GetProperty("StaticReference").GetSetter().Set(null, value);
                Assert.That(StaticReference, Is.EqualTo(value));
            }
        }

        struct ValueTarget
        {
            public int Value { get; set; }
            public string Reference { get; set; }

            public static int StaticValue { get; set; }
            public static string StaticReference { get; set; }
        }

        class ValueTargetTests
        {
            [Test]
            public void GetValueProperty([Random(1)] int value)
            {
                var target = new ValueTarget { Value = value };
                Assert.That(typeof(ValueTarget).GetProperty("Value").GetGetter().Get(target), Is.EqualTo(value));
            }

            [Test]
            public void GetReferenceProperty([Values("instanceTest")] string value)
            {
                var target = new ValueTarget { Reference = value };
                Assert.That(typeof(ValueTarget).GetProperty("Reference").GetGetter().Get(target), Is.EqualTo(value));
            }

            [Test]
            public void GetStaticValueProperty([Random(1)] int value)
            {
                ValueTarget.StaticValue = value;
                Assert.That(typeof(ValueTarget).GetProperty("StaticValue").GetGetter().Get(null), Is.EqualTo(value));
            }

            [Test]
            public void GetStaticReferenceProperty([Values("staticTest")] string value)
            {
                ValueTarget.StaticReference = value;
                Assert.That(typeof(ValueTarget).GetProperty("StaticReference").GetGetter().Get(null), Is.EqualTo(value));
            }

            [Test]
            public void SetValueProperty([Random(1)] int value)
            {
                var target = new ValueTarget();
                typeof(ValueTarget).GetProperty("Value").GetValueTypeSetter<ValueTarget>().Set(ref target, value);
                Assert.That(target.Value, Is.EqualTo(value));
            }

            [Test]
            public void SetReferenceProperty([Values("instanceTest")] string value)
            {
                var target = new ValueTarget();
                typeof(ValueTarget).GetProperty("Reference").GetValueTypeSetter<ValueTarget>().Set(ref target, value);
                Assert.That(target.Reference, Is.EqualTo(value));
            }

            [Test]
            public void SetStaticValueProperty([Random(1)] int value)
            {
                typeof(ValueTarget).GetProperty("StaticValue").GetSetter().Set(null, value);
                Assert.That(ValueTarget.StaticValue, Is.EqualTo(value));
            }

            [Test]
            public void SetStaticReferenceProperty([Values("staticTest")] string value)
            {
                typeof(ValueTarget).GetProperty("StaticReference").GetSetter().Set(null, value);
                Assert.That(ValueTarget.StaticReference, Is.EqualTo(value));
            }
        }
    }
}
