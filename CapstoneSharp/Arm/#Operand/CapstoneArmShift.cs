namespace CapstoneSharp.Arm;

[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty", Justification = "This is a struct used for interop")]
public readonly struct CapstoneArmShift
{
    public CapstoneArmShiftType Operation { get; }

    public uint Value { get; }
}
