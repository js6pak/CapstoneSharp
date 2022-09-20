using System.Diagnostics.CodeAnalysis;

namespace CapstoneSharp.Arm64;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "IdentifierTypo")]
public enum CapstoneArm64DcOperation : uint
{
    Invalid = 0,
    ZVA,
    IVAC,
    ISW,
    CVAC,
    CSW,
    CVAU,
    CIVAC,
    CISW,
}
