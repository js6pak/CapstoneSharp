using CapstoneSharp.Interop;

namespace CapstoneSharp.Arm;

public sealed class CapstoneArmDisassembler : CapstoneDisassembler<CapstoneArmInstruction>
{
    public CapstoneArmDisassembler(Mode mode = Mode.LittleEndian) : base(CapstoneArch.Arm, (CapstoneMode)mode)
    {
    }

    [Flags]
    public enum Mode
    {
        LittleEndian = CapstoneMode.LittleEndian,
        BigEndian = CapstoneMode.BigEndian,
        Thumb = CapstoneMode.Thumb,
        Mclass = CapstoneMode.Mclass,
    }
}
