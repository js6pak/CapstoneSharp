namespace CapstoneSharp.Arm;

[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty", Justification = "This is a struct used for interop")]
public readonly struct CapstoneArmMemoryOperandValue
{
    public CapstoneArmRegisterId Base { get; }

    public CapstoneArmRegisterId Index { get; }

    public int Scale { get; }

    public int Displacement { get; }

    public int LeftShift { get; }
}
