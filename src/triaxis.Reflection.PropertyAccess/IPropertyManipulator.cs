using System;

namespace triaxis.Reflection
{
    /// <summary>
    /// Interface for fast manipulation with properties of arbitrary objects
    /// </summary>
    public interface IPropertyManipulator : IPropertyGetter, IPropertySetter
    {
        /// <summary>
        /// Determines if the property can be read
        /// </summary>
        bool CanRead { get; }
        /// <summary>
        /// Determines if the property can be written
        /// </summary>
        bool CanWrite { get; }
    }

    /// <summary>
    /// Interface for fast manipulation with properties of arbitrary objects
    /// </summary>
    public interface IPropertyManipulator<TValue> : IPropertyManipulator, IPropertyGetter<TValue>, IPropertySetter<TValue>
    {

    }

    /// <summary>
    /// Interface for fast manipulation with properties of arbitrary objects
    /// </summary>
    public interface IPropertyManipulator<TTarget, TValue> : IPropertyManipulator<TValue>,
        IPropertyGetter<TTarget, TValue>, IPropertySetter<TTarget, TValue>,
        IValueTypePropertyGetter<TTarget>, IValueTypePropertySetter<TTarget>
    {

    }
}
