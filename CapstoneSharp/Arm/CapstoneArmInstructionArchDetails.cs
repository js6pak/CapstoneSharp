namespace CapstoneSharp.Arm;

/// <inheritdoc />
[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty", Justification = "This is a struct used for interop")]
#pragma warning disable CS0649
public readonly struct CapstoneArmInstructionArchDetails : ICapstoneInstructionArchDetails
{
    /// <summary>
    /// Gets a value indicating whether user-mode registers are loaded (for LDM/STM instructions).
    /// </summary>
    public NativeBoolean IsUserMode { get; }

    /// <summary>
    /// Gets scalar size for vector instructions.
    /// </summary>
    public int VectorSize { get; }

    /// <summary>
    /// Gets data type for elements of vector instructions.
    /// </summary>
    public CapstoneArmVectorDataType VectorDataType { get; }

    /// <summary>
    /// Gets the CPS mode for CPS instruction.
    /// </summary>
    public CapstoneArmCpsModeType CpsMode { get; }

    /// <summary>
    /// Gets the CPS flag for CPS instruction.
    /// </summary>
    public CapstoneArmCpsFlagType CpsFlag { get; }

    /// <summary>
    /// Gets the condition code.
    /// </summary>
    public CapstoneArmConditionCode ConditionCode { get; }

    /// <summary>
    /// Gets a value indicating whether this instruction updates flags.
    /// </summary>
    public NativeBoolean UpdateFlags { get; }

    /// <summary>
    /// Gets a value indicating whether this instruction requests writeback.
    /// </summary>
    public NativeBoolean WriteBack { get; }

    /// <summary>
    /// Gets the operation for some memory barrier instructions.
    /// </summary>
    public CapstoneArmMemoryBarrierOperation MemoryBarrierOperation { get; }

    private readonly byte _operandsCount;

    private readonly OperandsFixedBuffer _operands;

    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members")]
    [SuppressMessage("Performance", "CS0169:The private field 'class member' is never used")]
    [SuppressMessage("Performance", "CA1823:Avoid unused private fields")]
    private readonly struct OperandsFixedBuffer
    {
        private readonly CapstoneArmOperand _0;
        private readonly CapstoneArmOperand _1;
        private readonly CapstoneArmOperand _2;
        private readonly CapstoneArmOperand _3;
        private readonly CapstoneArmOperand _4;
        private readonly CapstoneArmOperand _5;
        private readonly CapstoneArmOperand _6;
        private readonly CapstoneArmOperand _7;
        private readonly CapstoneArmOperand _8;
        private readonly CapstoneArmOperand _9;
        private readonly CapstoneArmOperand _10;
        private readonly CapstoneArmOperand _11;
        private readonly CapstoneArmOperand _12;
        private readonly CapstoneArmOperand _13;
        private readonly CapstoneArmOperand _14;
        private readonly CapstoneArmOperand _15;
        private readonly CapstoneArmOperand _16;
        private readonly CapstoneArmOperand _17;
        private readonly CapstoneArmOperand _18;
        private readonly CapstoneArmOperand _19;
        private readonly CapstoneArmOperand _20;
        private readonly CapstoneArmOperand _21;
        private readonly CapstoneArmOperand _22;
        private readonly CapstoneArmOperand _23;
        private readonly CapstoneArmOperand _24;
        private readonly CapstoneArmOperand _25;
        private readonly CapstoneArmOperand _26;
        private readonly CapstoneArmOperand _27;
        private readonly CapstoneArmOperand _28;
        private readonly CapstoneArmOperand _29;
        private readonly CapstoneArmOperand _30;
        private readonly CapstoneArmOperand _31;
        private readonly CapstoneArmOperand _32;
        private readonly CapstoneArmOperand _33;
        private readonly CapstoneArmOperand _34;
        private readonly CapstoneArmOperand _35;
    }

    /// <summary>
    /// Gets the operands of this instruction.
    /// </summary>
    public unsafe ReadOnlySpan<CapstoneArmOperand> Operands
    {
        get
        {
            fixed (void* pThis = &_operands)
            {
                return new ReadOnlySpan<CapstoneArmOperand>(pThis, _operandsCount);
            }
        }
    }
}
