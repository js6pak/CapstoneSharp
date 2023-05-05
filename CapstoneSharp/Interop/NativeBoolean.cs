// Taken from https://github.com/MochiLibraries/Biohazrd/blob/9e11ce098fa067438d7fbd032fb9a9cebffaab19/docs/BuiltInDeclarations/NativeBooleanDeclaration.md

#if NET7_0_OR_GREATER
global using NativeBoolean = System.Boolean;
using System.Runtime.CompilerServices;

[assembly: DisableRuntimeMarshalling]
#else
global using NativeBoolean = CapstoneSharp.Interop.NativeBoolean;

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CapstoneSharp.Interop;

[StructLayout(LayoutKind.Sequential)]
[SuppressMessage("Design", "CA1036:Override methods on comparable types")]
public readonly struct NativeBoolean : IComparable, IComparable<bool>, IEquatable<bool>, IComparable<NativeBoolean>, IEquatable<NativeBoolean>
{
    private readonly byte Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator bool(NativeBoolean b)
        => Unsafe.As<NativeBoolean, bool>(ref b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator NativeBoolean(bool b)
        => Unsafe.As<bool, NativeBoolean>(ref b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
        => Unsafe.As<byte, bool>(ref Unsafe.AsRef(in Value)).GetHashCode();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
        => Unsafe.As<byte, bool>(ref Unsafe.AsRef(in Value)).ToString();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString(IFormatProvider? provider)
        => Unsafe.As<byte, bool>(ref Unsafe.AsRef(in Value)).ToString(provider);

#if NET6_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryFormat(Span<char> destination, out int charsWritten)
        => Unsafe.As<byte, bool>(ref Unsafe.AsRef(in Value)).TryFormat(destination, out charsWritten);
#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
        => obj switch
        {
            bool boolean => this == boolean,
            NativeBoolean nativeBool => this == nativeBool,
            _ => false,
        };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(bool other)
        => (bool)this == other;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(NativeBoolean other)
        => this == other;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(object? obj)
        => Unsafe.As<byte, bool>(ref Unsafe.AsRef(in Value)).CompareTo(obj);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(bool obj)
        => Unsafe.As<byte, bool>(ref Unsafe.AsRef(in Value)).CompareTo(obj);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(NativeBoolean obj)
        => CompareTo(Unsafe.As<NativeBoolean, bool>(ref obj));

    public static bool operator ==(NativeBoolean left, NativeBoolean right)
    {
        return (bool)left == (bool)right;
    }

    public static bool operator !=(NativeBoolean left, NativeBoolean right)
    {
        return !(left == right);
    }
}
#endif
