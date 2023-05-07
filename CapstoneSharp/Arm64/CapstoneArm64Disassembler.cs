using CapstoneSharp.Interop;

namespace CapstoneSharp.Arm64;

/// <inheritdoc />
public sealed class CapstoneArm64Disassembler : CapstoneDisassembler<CapstoneArm64InstructionId, CapstoneArm64Instruction, UnsafeCapstoneArm64Instruction>
{
    /// <inheritdoc />
    public CapstoneArm64Disassembler(Mode mode = Mode.LittleEndian) : base(CapstoneArch.Arm64, (CapstoneMode)mode)
    {
    }

    /// <summary>
    /// Specifies ARM64 disassembler mode.
    /// </summary>
    [Flags]
    public enum Mode
    {
        /// <summary>
        /// Little-endian mode.
        /// </summary>
        LittleEndian = CapstoneMode.LittleEndian,

        /// <summary>
        /// Big-endian mode.
        /// </summary>
        BigEndian = CapstoneMode.BigEndian,
    }

    /// <inheritdoc />
    public override unsafe UnsafeCapstoneArm64Instruction* AllocInstruction()
    {
        var instruction = NativeMemoryExtensions.Alloc<UnsafeCapstoneArm64Instruction>();
        instruction->Details = EnableInstructionDetails ? NativeMemoryExtensions.Alloc<UnsafeCapstoneArm64InstructionDetails>() : default;
        return instruction;
    }

    /// <inheritdoc />
    public override unsafe void FreeInstruction(UnsafeCapstoneArm64Instruction* ptr)
    {
        NativeMemoryExtensions.Free(ptr->Details);
        NativeMemoryExtensions.Free(ptr);
    }

    /// <inheritdoc />
    public override unsafe CapstoneArm64Instruction WrapUnsafeInstruction(UnsafeCapstoneArm64Instruction* ptr)
    {
        return new CapstoneArm64Instruction(ptr);
    }
}
