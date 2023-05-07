using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CapstoneSharp.Interop;

/// <inheritdoc cref="CapstoneSharp.ICapstoneInstruction" />
/// <typeparam name="TId"><inheritdoc cref="CapstoneSharp.ICapstoneInstruction{T}" /></typeparam>
/// <typeparam name="TRegister">The type of architecture specific register id.</typeparam>
/// <typeparam name="TGroup">The type of architecture specific group id.</typeparam>
/// <typeparam name="TArchDetails">The type of architecture specific details.</typeparam>
[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty", Justification = "This is a struct used for interop")]
#pragma warning disable CS0649
public unsafe struct UnsafeCapstoneInstruction<TId, TRegister, TGroup, TArchDetails> : ICapstoneInstruction<TId>
    where TId : unmanaged, Enum
    where TRegister : unmanaged, Enum
    where TGroup : unmanaged, Enum
    where TArchDetails : unmanaged, ICapstoneInstructionArchDetails
{
    [SuppressMessage("Design", "CA1065:Do not raise exceptions in unexpected locations")]
    static UnsafeCapstoneInstruction()
    {
        if (sizeof(TId) != sizeof(uint) || Enum.GetUnderlyingType(typeof(TId)) != typeof(uint))
            throw new InvalidProgramException($"TId ({typeof(TId).Name})'s underlying type has to be an uint");

        RuntimeHelpers.RunClassConstructor(typeof(UnsafeCapstoneInstructionDetails<TRegister, TGroup, TArchDetails>).TypeHandle);
    }

    /// <summary>
    /// Gets the raw instruction id (uint instead of an enum).
    /// </summary>
    public uint RawId { get; }

    /// <inheritdoc />
    public readonly TId Id
    {
        get
        {
            var id = RawId;
            return Unsafe.As<uint, TId>(ref id);
        }
    }

    /// <inheritdoc />
    public readonly bool IsSkippedData => RawId == 0;

    /// <inheritdoc />
    public ulong Address { get; }

    /// <inheritdoc />
    public ushort Size { get; }

    private fixed byte _bytes[16];

    /// <inheritdoc />
    public ReadOnlySpan<byte> Bytes
    {
        get
#if NET7_0_OR_GREATER
            => MemoryMarshal.CreateReadOnlySpan(ref _bytes[0], Size);
#else
        {
            fixed (byte* bytes = _bytes)
            {
                return new ReadOnlySpan<byte>(bytes, Size);
            }
        }
#endif
    }

    private fixed byte _mnemonic[32];

    /// <inheritdoc />
    public readonly string Mnemonic
    {
        get
        {
            fixed (byte* mnemonic = _mnemonic)
            {
                return Marshal.PtrToStringAnsi((IntPtr)mnemonic) ?? throw new InvalidOperationException();
            }
        }
    }

    private fixed byte _operands[160];

    /// <inheritdoc />
    public readonly string Operands
    {
        get
        {
            fixed (byte* operands = _operands)
            {
                return Marshal.PtrToStringAnsi((IntPtr)operands) ?? throw new InvalidOperationException();
            }
        }
    }

    private UnsafeCapstoneInstructionDetails<TRegister, TGroup, TArchDetails>* _details;

    /// <summary>
    /// Gets or sets the pointer to instruction details.
    /// </summary>
    /// <remarks>Returns a null pointer when <see cref="CapstoneDisassembler{TId,TInstruction,TUnsafeInstruction}.EnableInstructionDetails"/> is false.</remarks>
    /// <exception cref="InvalidOperationException">Thrown when <see cref="IsSkippedData"/> is true.</exception>
    public UnsafeCapstoneInstructionDetails<TRegister, TGroup, TArchDetails>* Details
    {
        get
        {
            if (IsSkippedData) throw new InvalidOperationException("Cannot get details for a \"data\" instruction");
            return _details;
        }

        set => _details = value;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return string.Join(" ", Mnemonic, Operands);
    }
}
