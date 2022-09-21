namespace CapstoneSharp.Arm;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "IdentifierTypo")]
public enum CapstoneArmMemoryBarrierOperation : uint
{
    Invalid = 0,
    RESERVED_0,
    OSHLD,
    OSHST,
    OSH,
    RESERVED_4,
    NSHLD,
    NSHST,
    NSH,
    RESERVED_8,
    ISHLD,
    ISHST,
    ISH,
    RESERVED_12,
    LD,
    ST,
    SY,
}
