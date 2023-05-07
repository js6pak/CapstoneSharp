using System.Runtime.ConstrainedExecution;
using CapstoneSharp.Interop;

namespace CapstoneSharp;

/// <inheritdoc cref="CapstoneSharp.ICapstoneInstructionDetails{TRegister,TGroup,TArchDetails}" />
public sealed unsafe class CapstoneInstructionDetails<TRegister, TGroup, TArchDetails> : CriticalFinalizerObject, IDisposable, ICapstoneInstructionDetails<TRegister, TGroup, TArchDetails>
    where TRegister : unmanaged, Enum
    where TGroup : unmanaged, Enum
    where TArchDetails : unmanaged, ICapstoneInstructionArchDetails
{
    private UnsafeCapstoneInstructionDetails<TRegister, TGroup, TArchDetails>* _handle;

    internal CapstoneInstructionDetails(UnsafeCapstoneInstructionDetails<TRegister, TGroup, TArchDetails>* handle)
    {
        _handle = handle;
    }

    ~CapstoneInstructionDetails()
    {
        Dispose();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        GC.SuppressFinalize(this);

        NativeMemoryExtensions.Free(_handle);
        _handle = default;
    }

    /// <inheritdoc />
    public ReadOnlySpan<TRegister> ImplicitlyReadRegisters => _handle->ImplicitlyReadRegisters;

    /// <inheritdoc />
    public ReadOnlySpan<TRegister> ImplicitlyWrittenRegisters => _handle->ImplicitlyWrittenRegisters;

    /// <inheritdoc />
    public ReadOnlySpan<TGroup> Groups => _handle->Groups;

    /// <inheritdoc />
    public bool BelongsToGroup(TGroup group) => _handle->BelongsToGroup(group);

    /// <inheritdoc />
    public TArchDetails ArchDetails => _handle->ArchDetails;
}
