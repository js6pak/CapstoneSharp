using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CapstoneSharp.Interop;

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
            throw new InvalidProgramException("TId's underlying type has to be an uint");
    }

    public uint RawId { get; }

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

    public ushort Size { get; }

    private fixed byte _bytes[16];

    public readonly ReadOnlySpan<byte> Bytes
    {
        get
        {
            fixed (byte* bytes = _bytes)
            {
                return new ReadOnlySpan<byte>(bytes, Size);
            }
        }
    }

    private fixed byte _mnemonic[32];

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

    /// <summary>Pointer to cs_detail</summary>
    /// <remarks>detail pointer is only valid when detail option is turned on</remarks>
    private UnsafeCapstoneInstructionDetails<TRegister, TGroup, TArchDetails>* _details;

    public UnsafeCapstoneInstructionDetails<TRegister, TGroup, TArchDetails>* Details
    {
        get
        {
            if (IsSkippedData) throw new InvalidOperationException("Cannot get details for data");
            return _details;
        }

        set => _details = value;
    }

    public override string ToString()
    {
        return string.Join(" ", Mnemonic, Operands);
    }
}
