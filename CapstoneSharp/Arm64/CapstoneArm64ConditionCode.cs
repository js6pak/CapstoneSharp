// ReSharper disable InconsistentNaming

namespace CapstoneSharp.Arm64;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "IdentifierTypo")]
public enum CapstoneArm64ConditionCode : uint
{
    Invalid = 0,

    /// <summary>
    /// Equal
    /// </summary>
    EQ = 1,

    /// <summary>
    /// Not equal
    /// </summary>
    NE = 2,

    /// <summary>
    /// Unsigned higher or same
    /// </summary>
    HS = 3,

    /// <summary>
    /// Unsigned lower or same
    /// </summary>
    LO = 4,

    /// <summary>
    /// Minus, negative
    /// </summary>
    MI = 5,

    /// <summary>
    /// Plus, positive or zero
    /// </summary>
    PL = 6,

    /// <summary>
    /// Overflow
    /// </summary>
    VS = 7,

    /// <summary>
    /// No overflow
    /// </summary>
    VC = 8,

    /// <summary>
    /// Unsigned higher
    /// </summary>
    HI = 9,

    /// <summary>
    /// Unsigned lower or same
    /// </summary>
    LS = 10,

    /// <summary>
    /// Greater than or equal
    /// </summary>
    GE = 11,

    /// <summary>
    /// Less than
    /// </summary>
    LT = 12,

    /// <summary>
    /// Signed greater than
    /// </summary>
    GT = 13,

    /// <summary>
    /// Signed less than or equal
    /// </summary>
    LE = 14,

    /// <summary>
    /// Always (unconditional)
    /// </summary>
    AL = 15,

    /// <summary>
    /// Always (unconditional)
    /// </summary>
    NV = 16,
}
