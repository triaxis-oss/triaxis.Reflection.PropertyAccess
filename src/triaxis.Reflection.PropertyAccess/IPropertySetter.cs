using System;

namespace triaxis.Reflection
{
    /// <summary>
    /// Interface for fast modification of properties of arbitrary objects
    /// </summary>
    public interface IPropertySetter : IPropertyAccessor
    {
        /// <summary>
        /// Sets the value of the property of the specified object
        /// </summary>
        /// <param name="target">Target object, or <see langword="null"/> for static properties</param>
        /// <param name="value">Value to set</param>
        void Set(object target, object value);
    }

    /// <summary>
    /// Interface for fast modification of properties of arbitrary objects
    /// </summary>
    public interface IPropertySetter<in TValue> : IPropertySetter
    {
        /// <summary>
        /// Sets the value of the property of the specified object
        /// </summary>
        /// <param name="target">Target object, or <see langword="null"/> for static properties</param>
        /// <param name="value">Value to set</param>
        void Set(object target, TValue value);
    }

    /// <summary>
    /// Interface for fast modification of properties of arbitrary objects
    /// </summary>
    public interface IPropertySetter<in TTarget, in TValue> : IPropertySetter<TValue>
    {
        /// <summary>
        /// Sets the value of the property of the specified object
        /// </summary>
        /// <param name="target">Target object</param>
        /// <param name="value">Value to set</param>
        void Set(TTarget target, TValue value);
    }
}
