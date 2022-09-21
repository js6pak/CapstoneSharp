using CapstoneSharp.Interop;

namespace CapstoneSharp.Arm64;

public sealed class CapstoneArm64Disassembler : CapstoneDisassembler<CapstoneArm64Instruction>
{
    public CapstoneArm64Disassembler(Mode mode = Mode.LittleEndian) : base(CapstoneArch.Arm64, (CapstoneMode)mode)
    {
    }

    [Flags]
    public enum Mode
    {
        LittleEndian = CapstoneMode.LittleEndian,
        BigEndian = CapstoneMode.BigEndian,
    }
}
