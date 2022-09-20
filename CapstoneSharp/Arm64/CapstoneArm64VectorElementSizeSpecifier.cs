using System.Diagnostics.CodeAnalysis;

namespace CapstoneSharp.Arm64;

/// <summary>
/// Vector element size specifier
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "IdentifierTypo")]
public enum CapstoneArm64VectorElementSizeSpecifier : uint
{
    Invalid = 0,
    B,
    H,
    S,
    D,
}
