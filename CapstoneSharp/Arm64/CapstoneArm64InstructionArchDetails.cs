namespace CapstoneSharp.Arm64;

/// <inheritdoc />
[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty", Justification = "This is a struct used for interop")]
#pragma warning disable CS0649
public readonly struct CapstoneArm64InstructionArchDetails : ICapstoneInstructionArchDetails
{
    /// <summary>
    /// Gets the condition code of this instruction.
    /// </summary>
    public CapstoneArm64ConditionCode ConditionCode { get; }

    /// <summary>
    /// Gets a value indicating whether this instruction updates flags.
    /// </summary>
    public NativeBoolean UpdateFlags { get; }

    /// <summary>
    /// Gets a value indicating whether this instruction requests writeback.
    /// </summary>
    public NativeBoolean Writeback { get; }

    private readonly byte _operandsCount;

    private readonly OperandsFixedBuffer _operands;

    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members")]
    [SuppressMessage("Performance", "CS0169:The private field 'class member' is never used")]
    [SuppressMessage("Performance", "CA1823:Avoid unused private fields")]
    private readonly struct OperandsFixedBuffer
    {
        private readonly CapstoneArm64Operand _0;
        private readonly CapstoneArm64Operand _1;
        private readonly CapstoneArm64Operand _2;
        private readonly CapstoneArm64Operand _3;
        private readonly CapstoneArm64Operand _4;
        private readonly CapstoneArm64Operand _5;
        private readonly CapstoneArm64Operand _6;
        private readonly CapstoneArm64Operand _7;
    }

    /// <summary>
    /// Gets the operands of this instruction.
    /// </summary>
    public unsafe ReadOnlySpan<CapstoneArm64Operand> Operands
    {
        get
        {
            fixed (void* pThis = &_operands)
            {
                return new ReadOnlySpan<CapstoneArm64Operand>(pThis, _operandsCount);
            }
        }
    }
}
