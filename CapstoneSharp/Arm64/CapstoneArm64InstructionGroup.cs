namespace CapstoneSharp.Arm64;

/// <summary>
/// ARM64 instruction group id.
/// </summary>
public enum CapstoneArm64InstructionGroup : byte
{
    Invalid = 0,
    JUMP,
    CALL,
    RET,
    INT,
    PRIVILEGE = 6,
    BRANCH_RELATIVE,
    CRYPTO = 128,
    FPARMV8,
    NEON,
    CRC,
    ENDING,
}
