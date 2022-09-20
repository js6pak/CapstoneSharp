namespace CapstoneSharp.Arm64;

public readonly struct CapstoneArm64Shift
{
    private readonly uint _value;

    public CapstoneArm64ShiftType Type { get; }

    public uint Value
    {
        get
        {
            if (Type == CapstoneArm64ShiftType.Invalid) throw new InvalidOperationException("Can't get shift value when type is invalid");
            return _value;
        }
    }
}
