using System.Diagnostics.CodeAnalysis;

namespace CapstoneSharp.Arm64;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "IdentifierTypo")]
public enum CapstoneArm64BarrierOperation : uint
{
    Invalid = 0,
    OSHLD = 0x1,
    OSHST = 0x2,
    OSH = 0x3,
    NSHLD = 0x5,
    NSHST = 0x6,
    NSH = 0x7,
    ISHLD = 0x9,
    ISHST = 0xa,
    ISH = 0xb,
    LD = 0xd,
    ST = 0xe,
    SY = 0xf,
}
