using System;
using System.Reflection;
using NUnit.Framework;

namespace triaxis.Reflection.PropertyAccess.Tests
{
    public class FieldAccessorTests
    {
        public class ReferenceTarget
        {
            public int Value;
            public string Reference;

            public static int StaticValue;
            public static string StaticReference;

            [Test]
            public void GetValueProperty([Random(1)] int value)
            {
                Value = value;
                Assert.That(GetType().GetField("Value").GetGetter().Get(this), Is.EqualTo(value));
            }

            [Test]
            public void GetValuePropertyWithValueAccessor([Random(1)] int value)
            {
                Value = value;
                var target = this;
                Assert.That(GetType().GetField("Value").GetValueTypeGetter<ReferenceTarget>().Get(ref target), Is.EqualTo(value));
            }

            [Test]
            public void GetReferenceProperty([Values("instanceTest")] string value)
            {
                Reference = value;
                Assert.That(GetType().GetField("Reference").GetGetter().Get(this), Is.EqualTo(value));
            }

            [Test]
            public void GetStaticValueProperty([Random(1)] int value)
            {
                StaticValue = value;
                Assert.That(GetType().GetField("StaticValue").GetGetter().Get(null), Is.EqualTo(value));
            }

            [Test]
            public void GetStaticReferenceProperty([Values("staticTest")] string value)
            {
                StaticReference = value;
                Assert.That(GetType().GetField("StaticReference").GetGetter().Get(null), Is.EqualTo(value));
            }

            [Test]
            public void SetValueProperty([Random(1)] int value)
            {
                GetType().GetField("Value").GetSetter().Set(this, value);
                Assert.That(Value, Is.EqualTo(value));
            }

            [Test]
            public void SetValuePropertyWithValueAccessor([Random(1)] int value)
            {
                var target = this;
                GetType().GetField("Value").GetValueTypeSetter<ReferenceTarget>().Set(ref target, value);
                Assert.That(Value, Is.EqualTo(value));
            }

            [Test]
            public void SetReferenceProperty([Values("instanceTest")] string value)
            {
                GetType().GetField("Reference").GetSetter().Set(this, value);
                Assert.That(Reference, Is.EqualTo(value));
            }

            [Test]
            public void SetStaticValueProperty([Random(1)] int value)
            {
                GetType().GetField("StaticValue").GetSetter().Set(null, value);
                Assert.That(StaticValue, Is.EqualTo(value));
            }

            [Test]
            public void SetStaticReferenceProperty([Values("staticTest")] string value)
            {
                GetType().GetField("StaticReference").GetSetter().Set(null, value);
                Assert.That(StaticReference, Is.EqualTo(value));
            }
        }

        struct ValueTarget
        {
            public int Value;
            public string Reference;

            public static int StaticValue;
            public static string StaticReference;
        }

        class ValueTargetTests
        {
            [Test]
            public void GetValueProperty([Random(1)] int value)
            {
                var target = new ValueTarget { Value = value };
                Assert.That(typeof(ValueTarget).GetField("Value").GetGetter().Get(target), Is.EqualTo(value));
            }

            [Test]
            public void GetReferenceProperty([Values("instanceTest")] string value)
            {
                var target = new ValueTarget { Reference = value };
                Assert.That(typeof(ValueTarget).GetField("Reference").GetGetter().Get(target), Is.EqualTo(value));
            }

            [Test]
            public void GetStaticValueProperty([Random(1)] int value)
            {
                ValueTarget.StaticValue = value;
                Assert.That(typeof(ValueTarget).GetField("StaticValue").GetGetter().Get(null), Is.EqualTo(value));
            }

            [Test]
            public void GetStaticReferenceProperty([Values("staticTest")] string value)
            {
                ValueTarget.StaticReference = value;
                Assert.That(typeof(ValueTarget).GetField("StaticReference").GetGetter().Get(null), Is.EqualTo(value));
            }

            [Test]
            public void SetValueProperty([Random(1)] int value)
            {
                var target = new ValueTarget();
                typeof(ValueTarget).GetField("Value").GetValueTypeSetter<ValueTarget>().Set(ref target, value);
                Assert.That(target.Value, Is.EqualTo(value));
            }

            [Test]
            public void SetReferenceProperty([Values("instanceTest")] string value)
            {
                var target = new ValueTarget();
                typeof(ValueTarget).GetField("Reference").GetValueTypeSetter<ValueTarget>().Set(ref target, value);
                Assert.That(target.Reference, Is.EqualTo(value));
            }

            [Test]
            public void SetStaticValueProperty([Random(1)] int value)
            {
                typeof(ValueTarget).GetField("StaticValue").GetSetter().Set(null, value);
                Assert.That(ValueTarget.StaticValue, Is.EqualTo(value));
            }

            [Test]
            public void SetStaticReferenceProperty([Values("staticTest")] string value)
            {
                typeof(ValueTarget).GetField("StaticReference").GetSetter().Set(null, value);
                Assert.That(ValueTarget.StaticReference, Is.EqualTo(value));
            }
        }
    }
}
