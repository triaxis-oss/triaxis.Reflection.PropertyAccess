using System;

namespace triaxis.Reflection
{
    /// <summary>
    /// Interface for fast modification of properties of value types
    /// </summary>
    public interface IValueTypePropertySetter<TTarget> : IPropertyAccessor
    {
        /// <summary>
        /// Sets the value of the property of the specified structure
        /// </summary>
        /// <param name="target">Target structure</param>
        /// <param name="value">Value to set</param>
        void Set(ref TTarget target, object value);
    }

    /// <summary>
    /// Interface for fast modification of properties of arbitrary objects
    /// </summary>
    public interface IValueTypePropertySetter<TTarget, in TValue> : IValueTypePropertySetter<TTarget>
    {
        /// <summary>
        /// Sets the value of the property of the specified object
        /// </summary>
        /// <param name="target">Target object</param>
        /// <param name="value">Value to set</param>
        void Set(ref TTarget target, TValue value);
    }
}
