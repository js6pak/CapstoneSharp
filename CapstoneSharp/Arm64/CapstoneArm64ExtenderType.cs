using System.Diagnostics.CodeAnalysis;

namespace CapstoneSharp.Arm64;

/// <summary>
/// ARM64 extender type
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "IdentifierTypo")]
public enum CapstoneArm64ExtenderType : uint
{
    Invalid = 0,
    UXTB = 1,
    UXTH = 2,
    UXTW = 3,
    UXTX = 4,
    SXTB = 5,
    SXTH = 6,
    SXTW = 7,
    SXTX = 8,
}
