using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CapstoneSharp.Arm;

[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty", Justification = "This is a struct used for interop")]
#pragma warning disable CS0649
public readonly struct CapstoneArmOperand
{
    public int VectorIndex { get; }

    public CapstoneArmShift Shift { get; }

    public CapstoneArmOperandType Type { get; }

    private readonly Union _union;

    public NativeBoolean IsSubtracted { get; }

    public CapstoneAccessType Access { get; }

    public sbyte NeonLane { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void EnsureType(CapstoneArmOperandType desiredType)
    {
        if (Type != desiredType)
            throw new InvalidOperationException($"Can't get this when {Type} isn't {nameof(CapstoneArmOperandType)}.{desiredType}");
    }

    public CapstoneArmRegisterId Register
    {
        get
        {
            EnsureType(CapstoneArmOperandType.Register);
            return (CapstoneArmRegisterId)_union.Register;
        }
    }

    public int Immediate
    {
        get
        {
            EnsureType(CapstoneArmOperandType.Immediate);
            return _union.Immediate;
        }
    }

    public double FloatingPoint
    {
        get
        {
            EnsureType(CapstoneArmOperandType.FloatingPoint);
            return _union.FloatingPoint;
        }
    }

    public CapstoneArmMemoryOperandValue Memory
    {
        get
        {
            EnsureType(CapstoneArmOperandType.Memory);
            return _union.Memory;
        }
    }

    public CapstoneArmSetEndType SetEndOperation
    {
        get
        {
            EnsureType(CapstoneArmOperandType.SetEndOperation);
            return _union.SetEndOperation;
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    private readonly struct Union
    {
        [FieldOffset(0)]
        public readonly uint Register;

        [FieldOffset(0)]
        public readonly int Immediate;

        [FieldOffset(0)]
        public readonly double FloatingPoint;

        [FieldOffset(0)]
        public readonly CapstoneArmMemoryOperandValue Memory;

        [FieldOffset(0)]
        public readonly CapstoneArmSetEndType SetEndOperation;
    }
}
