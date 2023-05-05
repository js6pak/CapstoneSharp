namespace CapstoneSharp.Arm64;

[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty", Justification = "This is a struct used for interop")]
public readonly struct CapstoneArm64MemoryOperandValue
{
    public CapstoneArm64RegisterId Base { get; }

    public CapstoneArm64RegisterId Index { get; }

    public int Displacement { get; }
}
