using System;

namespace triaxis.Reflection
{
    /// <summary>
    /// Interface for fast retrieval of properties of value types
    /// </summary>
    public interface IValueTypePropertyGetter<TTarget> : IPropertyAccessor
    {
        /// <summary>
        /// Retrieves the value of the property of the specified structure
        /// </summary>
        /// <param name="target">Target structure</param>
        /// <returns>Value of the property of the specified <paramref name="target" /></returns>
        object Get(ref TTarget target);
    }

    /// <summary>
    /// Interface for fast retrieval of properties of value types
    /// </summary>
    public interface IValueTypePropertyGetter<TTarget, out TValue> : IValueTypePropertyGetter<TTarget>
    {
        /// <summary>
        /// Retrieves the value of the property of the specified structure
        /// </summary>
        /// <param name="target">Target structure</param>
        /// <returns>Value of the property of the specified <paramref name="target" /></returns>
        new TValue Get(ref TTarget target);
    }
}
