using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CapstoneSharp.Arm64;

public readonly struct CapstoneArm64Operand
{
    public int VectorIndex { get; }

    public CapstoneArm64VectorArrangementSpecifier VectorArrangementSpecifier { get; }

    public CapstoneArm64VectorElementSizeSpecifier VectorElementSizeSpecifier { get; }

    public CapstoneArm64Shift Shift { get; }

    public CapstoneArm64ExtenderType ExtenderType { get; }

    public CapstoneArm64OperandType Type { get; }

    private readonly Union _union;

    public CapstoneAccessType Access { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void EnsureType(CapstoneArm64OperandType desiredType)
    {
        if (Type != desiredType)
            throw new InvalidOperationException($"Can't get this when {Type} isn't {nameof(CapstoneArm64OperandType)}.{desiredType}");
    }

    public CapstoneArm64RegisterId Register
    {
        get
        {
            EnsureType(CapstoneArm64OperandType.Register);
            return (CapstoneArm64RegisterId)_union.Register;
        }
    }

    public CapstoneArm64MrsSystemRegister MrsSystemRegister
    {
        get
        {
            EnsureType(CapstoneArm64OperandType.MrsSystemRegister);
            return (CapstoneArm64MrsSystemRegister)_union.Register;
        }
    }

    public CapstoneArm64MsrSystemRegister MsrSystemRegister
    {
        get
        {
            EnsureType(CapstoneArm64OperandType.MsrSystemRegister);
            return (CapstoneArm64MsrSystemRegister)_union.Register;
        }
    }

    public long Immediate
    {
        get
        {
            EnsureType(CapstoneArm64OperandType.Immediate);
            return _union.Immediate;
        }
    }

    public double FloatingPoint
    {
        get
        {
            EnsureType(CapstoneArm64OperandType.FloatingPoint);
            return _union.FloatingPoint;
        }
    }

    public CapstoneArm64MemoryOperandValue Memory
    {
        get
        {
            EnsureType(CapstoneArm64OperandType.Memory);
            return _union.Memory;
        }
    }

    public CapstoneArm64PState PState
    {
        get
        {
            EnsureType(CapstoneArm64OperandType.PState);
            return _union.PState;
        }
    }

    public uint SystemOperation
    {
        get
        {
            EnsureType(CapstoneArm64OperandType.SystemOperation);
            return _union.SystemOperation;
        }
    }

    public CapstoneArm64PrefetchOperation PrefetchOperation
    {
        get
        {
            EnsureType(CapstoneArm64OperandType.PrefetchOperation);
            return _union.PrefetchOperation;
        }
    }

    public CapstoneArm64BarrierOperation BarrierOperation
    {
        get
        {
            EnsureType(CapstoneArm64OperandType.BarrierOperation);
            return _union.BarrierOperation;
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    private readonly struct Union
    {
        [FieldOffset(0)]
        public readonly uint Register;

        [FieldOffset(0)]
        public readonly long Immediate;

        [FieldOffset(0)]
        public readonly double FloatingPoint;

        [FieldOffset(0)]
        public readonly CapstoneArm64MemoryOperandValue Memory;

        [FieldOffset(0)]
        public readonly CapstoneArm64PState PState;

        [FieldOffset(0)]
        public readonly uint SystemOperation;

        [FieldOffset(0)]
        public readonly CapstoneArm64PrefetchOperation PrefetchOperation;

        [FieldOffset(0)]
        public readonly CapstoneArm64BarrierOperation BarrierOperation;
    }
}
