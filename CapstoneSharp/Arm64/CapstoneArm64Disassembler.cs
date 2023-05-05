using CapstoneSharp.Interop;

namespace CapstoneSharp.Arm64;

public sealed class CapstoneArm64Disassembler : CapstoneDisassembler<CapstoneArm64InstructionId, CapstoneArm64Instruction, UnsafeCapstoneArm64Instruction>
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

    public override unsafe UnsafeCapstoneArm64Instruction* AllocInstruction()
    {
        var instruction = NativeMemoryExtensions.Alloc<UnsafeCapstoneArm64Instruction>();
        instruction->Details = EnableInstructionDetails ? NativeMemoryExtensions.Alloc<UnsafeCapstoneArm64InstructionDetails>() : default;
        return instruction;
    }
    
    public override unsafe void FreeInstruction(UnsafeCapstoneArm64Instruction* ptr)
    {
        NativeMemoryExtensions.Free(ptr->Details);
        NativeMemoryExtensions.Free(ptr);
    }

    public override unsafe CapstoneArm64Instruction WrapUnsafeInstruction(UnsafeCapstoneArm64Instruction* ptr)
    {
        return new CapstoneArm64Instruction(ptr);
    }
}
