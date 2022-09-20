using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#pragma warning disable CS0649
namespace CapstoneSharp;

using System;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CapstoneInstructionDetails<TArchDetails, TRegister, TGroup>
    where TArchDetails : unmanaged, ICapstoneInstructionArchDetails
    where TRegister : unmanaged, Enum
    where TGroup : unmanaged, Enum
{
    static CapstoneInstructionDetails()
    {
        if (sizeof(TRegister) != sizeof(uint) || Enum.GetUnderlyingType(typeof(TRegister)) != typeof(uint))
            throw new InvalidProgramException("TRegister's underlying type has to be an uint");

        if (sizeof(TGroup) != sizeof(byte) || Enum.GetUnderlyingType(typeof(TGroup)) != typeof(byte))
            throw new InvalidProgramException("TGroup's underlying type has to be a byte");
    }

    /// <summary>
    /// list of implicit registers read by this insn
    /// </summary>
    private fixed ushort _implicitlyReadRegisters[12];

    /// <summary>
    /// number of implicit registers read by this insn
    /// </summary>
    private byte _implicitlyReadRegistersCount;

    public ReadOnlySpan<TRegister> ImplicitlyReadRegisters
    {
        get
        {
            fixed (ushort* registers = _implicitlyReadRegisters)
            {
                return new ReadOnlySpan<TRegister>(registers, _implicitlyReadRegistersCount);
            }
        }
    }

    /// <summary>
    /// list of implicit registers modified by this insn
    /// </summary>
    private fixed ushort _implicitlyWrittenRegisters[20];

    /// <summary>
    /// number of implicit registers modified by this insn
    /// </summary>
    private byte _implicitlyWrittenRegistersCount;

    public ReadOnlySpan<TRegister> ImplicitlyWrittenRegisters
    {
        get
        {
            fixed (ushort* registers = _implicitlyWrittenRegisters)
            {
                return new ReadOnlySpan<TRegister>(registers, _implicitlyWrittenRegistersCount);
            }
        }
    }

    /// <summary>
    /// list of group this instruction belong to
    /// </summary>
    private fixed byte _groups[8];

    /// <summary>
    /// number of groups this insn belongs to
    /// </summary>
    private byte _groupsCount;

    public ReadOnlySpan<TGroup> Groups
    {
        get
        {
            fixed (byte* groups = _groups)
            {
                return new ReadOnlySpan<TGroup>(groups, _groupsCount);
            }
        }
    }

    public bool BelongsToGroup(TGroup group)
    {
        var groupAsByte = Unsafe.As<TGroup, byte>(ref group);

#if NET6_0_OR_GREATER
        return MemoryMarshal.Cast<TGroup, byte>(Groups).Contains(groupAsByte);
#else
        for (var i = 0; i < _groupsCount; i++)
        {
            if (_groups[i] == groupAsByte) return true;
        }

        return false;
#endif
    }

    public TArchDetails ArchDetails { get; }
}
