namespace CapstoneSharp.Arm;

public readonly struct CapstoneArmMemoryOperandValue
{
    public CapstoneArmRegisterId Base { get; }

    public CapstoneArmRegisterId Index { get; }

    public int Scale { get; }

    public int Displacement { get; }

    public int LeftShift { get; }
}