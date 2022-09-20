#pragma warning disable CS0649
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CapstoneSharp;

/// <summary>
/// Detail information of disassembled instruction
/// </summary>
public unsafe struct CapstoneInstruction<TId, TArchDetails, TRegister, TGroup> : IInstruction
    where TId : unmanaged, Enum
    where TArchDetails : unmanaged, ICapstoneInstructionArchDetails
    where TRegister : unmanaged, Enum
    where TGroup : unmanaged, Enum
{
    private TId _id;

    /// <summary>Instruction ID (basically a numeric ID for the instruction mnemonic)</summary>
    /// <remarks>NOTE: in Skipdata mode, "data" instruction has 0 for this id field.</remarks>
    public readonly TId Id => _id;

    uint IInstruction.Id => Unsafe.As<TId, uint>(ref _id);

    /// <summary>Address (EIP) of this instruction</summary>
    public ulong Address { get; }

    /// <summary>Size of this instruction</summary>
    public ushort Size { get; }

    /// <summary>Machine bytes of this instruction, with number of bytes indicated by <see cref="Size"/> above</summary>
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

    /// <summary>Ascii text of instruction mnemonic</summary>
    private fixed byte _mnemonic[32];

    public readonly string Mnemonic
    {
        get
        {
            fixed (byte* mnemonic = _mnemonic)
            {
                return Marshal.PtrToStringAnsi((IntPtr)mnemonic) ?? throw new ArgumentNullException(nameof(_mnemonic));
            }
        }
    }

    /// <summary>Ascii text of instruction operands</summary>
    private fixed byte _operands[160];

    public readonly string Operands
    {
        get
        {
            fixed (byte* operands = _operands)
            {
                return Marshal.PtrToStringAnsi((IntPtr)operands) ?? throw new ArgumentNullException(nameof(_operands));
            }
        }
    }

    /// <summary>Pointer to cs_detail</summary>
    /// <remarks>detail pointer is only valid when detail option is turned on</remarks>
    private readonly CapstoneInstructionDetails<TArchDetails, TRegister, TGroup>* _details;

    public ref CapstoneInstructionDetails<TArchDetails, TRegister, TGroup> Details => ref *_details;
}
