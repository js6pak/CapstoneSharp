namespace CapstoneSharp;

/// <summary>
/// Represents details about an instruction.
/// </summary>
/// <typeparam name="TRegister">The type of architecture specific register id.</typeparam>
/// <typeparam name="TGroup">The type of architecture specific group id.</typeparam>
/// <typeparam name="TArchDetails">The type of architecture specific details.</typeparam>
public interface ICapstoneInstructionDetails<TRegister, TGroup, TArchDetails>
    where TRegister : unmanaged, Enum
    where TGroup : unmanaged, Enum
    where TArchDetails : unmanaged, ICapstoneInstructionArchDetails
{
    /// <summary>
    /// Gets implicit registers read by this instruction.
    /// </summary>
    ReadOnlySpan<TRegister> ImplicitlyReadRegisters { get; }

    /// <summary>
    /// Gets implicit registers modified by this instruction.
    /// </summary>
    ReadOnlySpan<TRegister> ImplicitlyWrittenRegisters { get; }

    /// <summary>
    /// Gets groups this instruction belongs to.
    /// </summary>
    ReadOnlySpan<TGroup> Groups { get; }

    /// <summary>
    /// Determines whether this instruction belongs to the <paramref name="group"/>.
    /// </summary>
    /// <param name="group">The group to check.</param>
    /// <returns>true if instruction belongs to the <paramref name="group"/>; otherwise, false.</returns>
    bool BelongsToGroup(TGroup group);

    /// <summary>
    /// Gets architecture specific details of this instruction.
    /// </summary>
    TArchDetails ArchDetails { get; }
}
