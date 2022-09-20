#pragma warning disable CS0649
using System.Diagnostics.CodeAnalysis;
using CapstoneSharp.Interop;

namespace CapstoneSharp.Arm64;

public readonly struct CapstoneInstructionArm64Details : ICapstoneInstructionArchDetails
{
    public CapstoneArm64ConditionCode ConditionCode { get; }

    public NativeBoolean UpdateFlags { get; }

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
