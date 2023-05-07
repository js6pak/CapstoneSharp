namespace CapstoneSharp;

/// <summary>
/// Represents a disassembled instruction.
/// </summary>
public interface ICapstoneInstruction
{
    /// <summary>
    /// Gets a value indicating whether this is a "data" instruction.
    /// </summary>
    bool IsSkippedData { get; }

    /// <summary>Gets the address of this instruction.</summary>
    ulong Address { get; }

    /// <summary>Gets the size of this instruction.</summary>
    ushort Size { get; }

    /// <summary>Gets the raw bytes of this instruction.</summary>
    ReadOnlySpan<byte> Bytes { get; }

    /// <summary>Gets the text representation of instruction mnemonic.</summary>
    string Mnemonic { get; }

    /// <summary>Gets the text representation of instruction operands.</summary>
    string Operands { get; }
}

/// <inheritdoc />
/// <typeparam name="TId">The type of architecture specific instruction id.</typeparam>
public interface ICapstoneInstruction<TId> : ICapstoneInstruction
    where TId : unmanaged, Enum
{
    /// <summary>Gets the instruction id (an enum for the instruction mnemonic).</summary>
    /// <remarks>In SkipData mode, "data" instruction has 0 for this id field.</remarks>
    TId Id { get; }
}
