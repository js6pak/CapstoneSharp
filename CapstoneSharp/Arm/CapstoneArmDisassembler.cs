using CapstoneSharp.Interop;

namespace CapstoneSharp.Arm;

public sealed class CapstoneArmDisassembler : CapstoneDisassembler<CapstoneArmInstructionId, CapstoneArmInstruction, UnsafeCapstoneArmInstruction>
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

    public override unsafe UnsafeCapstoneArmInstruction* AllocInstruction()
    {
        var instruction = NativeMemoryExtensions.Alloc<UnsafeCapstoneArmInstruction>();
        instruction->Details = EnableInstructionDetails ? NativeMemoryExtensions.Alloc<UnsafeCapstoneArmInstructionDetails>() : default;
        return instruction;
    }

    public override unsafe void FreeInstruction(UnsafeCapstoneArmInstruction* ptr)
    {
        NativeMemoryExtensions.Free(ptr->Details);
        NativeMemoryExtensions.Free(ptr);
    }

    public override unsafe CapstoneArmInstruction WrapUnsafeInstruction(UnsafeCapstoneArmInstruction* ptr)
    {
        return new CapstoneArmInstruction(ptr);
    }
}
