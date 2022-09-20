using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CapstoneSharp;

internal static unsafe class UnsafeExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T* AsPointer<T>(ref T value) where T : unmanaged => (T*)Unsafe.AsPointer(ref value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T* AsPointer<T>(ref ReadOnlySpan<T> span) where T : unmanaged => AsPointer(ref MemoryMarshal.GetReference(span));
}
