using System;
using System.Reflection.Emit;

namespace triaxis.Reflection
{

    static class PrivateExtensions
    {
#if NETSTANDARD2_1_OR_GREATER
        public static DynamicMethod WithIl(this DynamicMethod dm, Action<ILGenerator> il)
        {
            il(dm.GetILGenerator());
            return dm;
        }
#endif
    }
}
