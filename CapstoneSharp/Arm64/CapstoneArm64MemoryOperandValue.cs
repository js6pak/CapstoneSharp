namespace CapstoneSharp.Arm64;

public readonly struct CapstoneArm64MemoryOperandValue
{
    public CapstoneArm64RegisterId Base { get; }

    public CapstoneArm64RegisterId Index { get; }

    public int Displacement { get; }
}
