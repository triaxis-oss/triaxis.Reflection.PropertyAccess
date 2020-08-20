using System;

namespace triaxis.Reflection
{
    /// <summary>
    /// Interface for fast property retrieval on arbitrary objects
    /// </summary>
    public interface IPropertyGetter : IPropertyAccessor
    {
        /// <summary>
        /// Retrieves the value of the property of the specified object
        /// </summary>
        /// <param name="target">Target object, or <see langword="null"/> for static properties</param>
        /// <returns>Value of the property of the specified <paramref name="target" /></returns>
        object Get(object target);
    }

    /// <summary>
    /// Interface for fast property retrieval on arbitrary objects
    /// </summary>
    public interface IPropertyGetter<out TValue> : IPropertyGetter
    {
        /// <summary>
        /// Retrieves the value of the property of the specified object
        /// </summary>
        /// <param name="target">Target object, or <see langword="null"/> for static properties</param>
        /// <returns>Value of the property of the specified <paramref name="target" /></returns>
        new TValue Get(object target);
    }

    /// <summary>
    /// Interface for fast property retrieval on arbitrary objects
    /// </summary>
    public interface IPropertyGetter<in TTarget, out TValue> : IPropertyGetter<TValue>
    {
        /// <summary>
        /// Retrieves the value of the property of the specified object
        /// </summary>
        /// <param name="target">Target object</param>
        /// <returns>Value of the property of the specified <paramref name="target" /></returns>
        TValue Get(TTarget target);
    }
}
