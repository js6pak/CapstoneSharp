using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using CapstoneSharp.Interop;

namespace CapstoneSharp;

/// <inheritdoc cref="CapstoneSharp.ICapstoneInstruction" />
/// <typeparam name="TId"><inheritdoc cref="CapstoneSharp.ICapstoneInstruction{T}" /></typeparam>
/// <typeparam name="TRegister">The type of architecture specific register id.</typeparam>
/// <typeparam name="TGroup">The type of architecture specific group id.</typeparam>
/// <typeparam name="TArchDetails">The type of architecture specific details.</typeparam>
public sealed unsafe class CapstoneInstruction<TId, TRegister, TGroup, TArchDetails> : CriticalFinalizerObject, IDisposable, ICapstoneInstruction<TId>
    where TId : unmanaged, Enum
    where TRegister : unmanaged, Enum
    where TGroup : unmanaged, Enum
    where TArchDetails : unmanaged, ICapstoneInstructionArchDetails
{
    static CapstoneInstruction()
    {
        RuntimeHelpers.RunClassConstructor(typeof(UnsafeCapstoneInstruction<TId, TRegister, TGroup, TArchDetails>).TypeHandle);
    }

    private UnsafeCapstoneInstruction<TId, TRegister, TGroup, TArchDetails>* _handle;
    private readonly CapstoneInstructionDetails<TRegister, TGroup, TArchDetails>? _details;

    internal CapstoneInstruction(UnsafeCapstoneInstruction<TId, TRegister, TGroup, TArchDetails>* handle)
    {
        _handle = handle;

        if (!IsSkippedData && handle->Details != default)
        {
            _details = new CapstoneInstructionDetails<TRegister, TGroup, TArchDetails>(handle->Details);
        }
    }

    ~CapstoneInstruction()
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
    public TId Id => _handle->Id;

    /// <inheritdoc />
    public bool IsSkippedData => _handle->IsSkippedData;

    /// <inheritdoc />
    public ulong Address => _handle->Address;

    /// <inheritdoc />
    public ushort Size => _handle->Size;

    /// <inheritdoc />
    public ReadOnlySpan<byte> Bytes => _handle->Bytes;

    /// <inheritdoc />
    public string Mnemonic => _handle->Mnemonic;

    /// <inheritdoc />
    public string Operands => _handle->Operands;

    /// <summary>
    /// Gets the instruction details.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when <see cref="IsSkippedData"/> is true.</exception>
    /// <exception cref="InvalidOperationException">Thrown when <see cref="CapstoneDisassembler{TId,TInstruction,TUnsafeInstruction}.EnableInstructionDetails"/> is false.</exception>
    public CapstoneInstructionDetails<TRegister, TGroup, TArchDetails> Details
    {
        get
        {
            if (IsSkippedData) throw new InvalidOperationException("Cannot get details for a \"data\" instruction");
            return _details ?? throw new InvalidOperationException("Details support is disabled in the CapstoneDisassembler");
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return string.Join(" ", Mnemonic, Operands);
    }
}
