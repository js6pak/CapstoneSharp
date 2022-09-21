namespace CapstoneSharp.Arm;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "IdentifierTypo")]
public enum CapstoneArmShiftType : uint
{
    Invalid = 0,
    ASR,
    LSL,
    LSR,
    ROR,
    RRX,
    ASR_REG,
    LSL_REG,
    LSR_REG,
    ROR_REG,
    RRX_REG,
}
