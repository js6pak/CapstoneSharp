namespace CapstoneSharp.Arm64;

public enum CapstoneArm64PState : uint
{
    Invalid = 0,
    SPSEL = 0x05,
    DAIFSET = 0x1e,
    DAIFCLR = 0x1f,
}
