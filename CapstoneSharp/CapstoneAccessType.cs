namespace CapstoneSharp;

/// <summary>
/// Common instruction operand access types - to be consistent across all architectures.
/// </summary>
[Flags]
public enum CapstoneAccessType : byte
{
    /// <summary>Uninitialized/invalid access type.</summary>
    Invalid = 0,

    /// <summary>Reads from memory or register.</summary>
    Read = 1 << 0,

    /// <summary>Writes to memory or register.</summary>
    Write = 1 << 1,
}
