using System;
using System.Reflection;

namespace triaxis.Reflection
{
    /// <summary>
    /// Provides efficient access to one property of an arbitrary object
    /// </summary>
    public interface IPropertyAccessor
    {
        /// <summary>
        /// Gets the <see cref="PropertyInfo" /> of the accessed property
        /// </summary>
        PropertyInfo Property { get; }
    }
}
