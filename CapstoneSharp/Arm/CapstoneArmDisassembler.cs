using CapstoneSharp.Interop;

namespace CapstoneSharp.Arm;

/// <inheritdoc />
public sealed class CapstoneArmDisassembler : CapstoneDisassembler<CapstoneArmInstructionId, CapstoneArmInstruction, UnsafeCapstoneArmInstruction>
{
    /// <inheritdoc />
    public CapstoneArmDisassembler(Mode mode = Mode.LittleEndian) : base(CapstoneArch.Arm, (CapstoneMode)mode)
    {
    }

    /// <summary>
    /// Specifies ARM disassembler mode.
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

        /// <summary>
        /// ARM's Thumb mode, including Thumb-2.
        /// </summary>
        Thumb = CapstoneMode.Thumb,

        /// <summary>
        /// ARM's Cortex-M series.
        /// </summary>
        Mclass = CapstoneMode.Mclass,
    }

    /// <inheritdoc />
    public override unsafe UnsafeCapstoneArmInstruction* AllocInstruction()
    {
        var instruction = NativeMemoryExtensions.Alloc<UnsafeCapstoneArmInstruction>();
        instruction->Details = EnableInstructionDetails ? NativeMemoryExtensions.Alloc<UnsafeCapstoneArmInstructionDetails>() : default;
        return instruction;
    }

    /// <inheritdoc />
    public override unsafe void FreeInstruction(UnsafeCapstoneArmInstruction* ptr)
    {
        NativeMemoryExtensions.Free(ptr->Details);
        NativeMemoryExtensions.Free(ptr);
    }

    /// <inheritdoc />
    public override unsafe CapstoneArmInstruction WrapUnsafeInstruction(UnsafeCapstoneArmInstruction* ptr)
    {
        return new CapstoneArmInstruction(ptr);
    }
}
