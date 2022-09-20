using CapstoneSharp.Interop;

namespace CapstoneSharp.Arm64;

public sealed class CapstoneArm64Disassembler : CapstoneDisassembler<CapstoneArm64Instruction>
{
    public CapstoneArm64Disassembler() : base(CapstoneArch.Arm64, CapstoneMode.LittleEndian)
    {
    }
}
