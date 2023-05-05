namespace CapstoneSharp.Arm64;

public enum CapstoneArm64OperandType : uint
{
    /// <summary>
    ///     Indicates an invalid, or an uninitialized, operand type.
    /// </summary>
    Invalid = 0,

    /// <summary>
    ///     Indicates a register operand.
    /// </summary>
    Register,

    /// <summary>
    ///     Indicates an immediate operand.
    /// </summary>
    Immediate,

    /// <summary>
    ///     Indicates a memory operand.
    /// </summary>
    Memory,

    /// <summary>
    ///     Indicates a floating point operand.
    /// </summary>
    FloatingPoint,

    /// <summary>
    ///     Indicates a CImmediate operand.
    /// </summary>
    CImmediate = 64,

    /// <summary>
    ///     Indicates a MRS system register operand.
    /// </summary>
    MrsSystemRegister,

    /// <summary>
    ///     Indicates a MSR system register operand.
    /// </summary>
    MsrSystemRegister,

    /// <summary>
    ///     Indicates a Processor State (PSTATE) field operand.
    /// </summary>
    PState,

    /// <summary>
    ///     Indicates a system operation operand.
    /// </summary>
    SystemOperation,

    /// <summary>
    ///     Indicates a prefetch operation operand.
    /// </summary>
    PrefetchOperation,

    /// <summary>
    ///     Indicates a barrier operation operand.
    /// </summary>
    BarrierOperation,
}
