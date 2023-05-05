using System.Runtime.InteropServices;

namespace CapstoneSharp.Interop;

internal static unsafe class NativeMemoryExtensions
{
    public static T* Alloc<T>() where T : unmanaged
    {
#if NET6_0_OR_GREATER
        return (T*)NativeMemory.AllocZeroed((nuint)sizeof(T));
#else
        return (T*)Marshal.AllocHGlobal((IntPtr)sizeof(T));
#endif
    }

    public static void Free(void* ptr)
    {
#if NET6_0_OR_GREATER
        NativeMemory.Free(ptr);
#else
        Marshal.FreeHGlobal((IntPtr)ptr);
#endif
    }
}
