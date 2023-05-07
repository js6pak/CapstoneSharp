using System.Runtime.CompilerServices;
#if NET6_0_OR_GREATER
using System.Runtime.InteropServices;
#endif

namespace CapstoneSharp.Interop;

/// <inheritdoc cref="CapstoneSharp.ICapstoneInstructionDetails{TRegister,TGroup,TArchDetails}" />
[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty", Justification = "This is a struct used for interop")]
#pragma warning disable CS0649
public unsafe struct UnsafeCapstoneInstructionDetails<TRegister, TGroup, TArchDetails> : ICapstoneInstructionDetails<TRegister, TGroup, TArchDetails>
    where TRegister : unmanaged, Enum
    where TGroup : unmanaged, Enum
    where TArchDetails : unmanaged, ICapstoneInstructionArchDetails
{
    [SuppressMessage("Design", "CA1065:Do not raise exceptions in unexpected locations")]
    static UnsafeCapstoneInstructionDetails()
    {
        if (sizeof(TRegister) != sizeof(uint) || Enum.GetUnderlyingType(typeof(TRegister)) != typeof(uint))
            throw new InvalidProgramException($"TRegister ({typeof(TRegister).FullName})'s underlying type has to be an uint");

        if (sizeof(TGroup) != sizeof(byte) || Enum.GetUnderlyingType(typeof(TGroup)) != typeof(byte))
            throw new InvalidProgramException($"TGroup ({typeof(TGroup).FullName})'s underlying type has to be a byte");
    }

    private fixed ushort _implicitlyReadRegisters[12];
    private readonly byte _implicitlyReadRegistersCount;

    /// <inheritdoc />
    public readonly ReadOnlySpan<TRegister> ImplicitlyReadRegisters
    {
        get
        {
            fixed (ushort* registers = _implicitlyReadRegisters)
            {
                return new ReadOnlySpan<TRegister>(registers, _implicitlyReadRegistersCount);
            }
        }
    }

    private fixed ushort _implicitlyWrittenRegisters[20];
    private readonly byte _implicitlyWrittenRegistersCount;

    /// <inheritdoc />
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

    private fixed byte _groups[8];
    private readonly byte _groupsCount;

    /// <inheritdoc />
    public readonly ReadOnlySpan<TGroup> Groups
    {
        get
        {
            fixed (byte* groups = _groups)
            {
                return new ReadOnlySpan<TGroup>(groups, _groupsCount);
            }
        }
    }

    /// <inheritdoc />
    public readonly bool BelongsToGroup(TGroup group)
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

    /// <inheritdoc />
    public TArchDetails ArchDetails { get; }
}
