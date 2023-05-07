using CapstoneSharp.Interop;

namespace CapstoneSharp;

/// <summary>
/// Represents a Capstone disassembler.
/// </summary>
/// <typeparam name="TId">The type of architecture specific instruction id.</typeparam>
/// <typeparam name="TInstruction">The type of architecture specific instruction.</typeparam>
/// <typeparam name="TUnsafeInstruction">The type of architecture specific unsafe instruction.</typeparam>
public abstract unsafe partial class CapstoneDisassembler<TId, TInstruction, TUnsafeInstruction> : IDisposable
    where TId : unmanaged, Enum
    where TInstruction : ICapstoneInstruction<TId>
    where TUnsafeInstruction : unmanaged, ICapstoneInstruction<TId>
{
    private CapstoneDisassemblerHandle _handle;

    private protected CapstoneDisassembler(CapstoneArch arch, CapstoneMode mode)
    {
        CapstoneException.ThrowIfUnsuccessful(CapstoneImports.open(arch, mode, out _handle));
    }

    /// <summary>
    /// Allocates an instruction.
    /// </summary>
    /// <returns>A pointer to the instruction.</returns>
    public abstract TUnsafeInstruction* AllocInstruction();

    /// <summary>
    /// Frees the specified instruction.
    /// </summary>
    /// <param name="ptr">A pointer to the instruction.</param>
    public abstract void FreeInstruction(TUnsafeInstruction* ptr);

    /// <summary>
    /// Allocates an instruction.
    /// </summary>
    /// <param name="instruction">A pointer to the instruction.</param>
    /// <returns>A <see cref="UnsafeInstructionOwner"/>.</returns>
    public UnsafeInstructionOwner AllocInstruction(out TUnsafeInstruction* instruction)
    {
        instruction = AllocInstruction();
        return new UnsafeInstructionOwner(this, instruction);
    }

    /// <summary>
    /// Represents a <see cref="IDisposable"/> wrapper that frees the instruction on <see cref="IDisposable.Dispose"/>.
    /// </summary>
    public readonly struct UnsafeInstructionOwner : IDisposable
    {
        private readonly CapstoneDisassembler<TId, TInstruction, TUnsafeInstruction> _disassembler;
        private readonly TUnsafeInstruction* _handle;

        internal UnsafeInstructionOwner(CapstoneDisassembler<TId, TInstruction, TUnsafeInstruction> disassembler, TUnsafeInstruction* handle)
        {
            _disassembler = disassembler;
            _handle = handle;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _disassembler.FreeInstruction(_handle);
        }
    }

    /// <summary>
    /// Wraps an <typeparamref name="TUnsafeInstruction"/> in a safe <typeparamref name="TInstruction"/>.
    /// </summary>
    /// <param name="ptr">The pointer to an <typeparamref name="TUnsafeInstruction"/>.</param>
    /// <returns>A wrapped <typeparamref name="TInstruction"/>.</returns>
    public abstract TInstruction WrapUnsafeInstruction(TUnsafeInstruction* ptr);

    /// <summary>
    /// Unsafely iterates specified <paramref name="code"/>.
    /// When used with caution this can be used to disassemble instructions with no managed allocations.
    /// </summary>
    /// <returns>true if there are more instructions to iterate; false if it's the end.</returns>
    public bool UnsafeIterate(byte** code, nuint* size, ulong* address, TUnsafeInstruction* instruction)
    {
        _handle.EnsureAlive();

        if (*size <= 0) return false;

        if (!CapstoneImports.disasm_iter(_handle, code, size, address, instruction))
        {
            var status = CapstoneImports.errno(_handle);
            if (status == CapstoneStatus.Ok) return false;
            throw new CapstoneException(status);
        }

        return true;
    }

    /// <inheritdoc cref="UnsafeIterate(byte**,nuint*,ulong*,TUnsafeInstruction*)"/>
    public bool UnsafeIterate(ref ReadOnlySpan<byte> code, ulong* address, TUnsafeInstruction* instruction)
    {
        _handle.EnsureAlive();

        if (code.Length <= 0) return false;

        fixed (byte* codePointer = code)
        {
            var size = (nuint)code.Length;
            var result = UnsafeIterate(&codePointer, &size, address, instruction);
            code = new ReadOnlySpan<byte>(codePointer, (int)size);
            return result;
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _handle.EnsureAlive();
        CapstoneException.ThrowIfUnsuccessful(CapstoneImports.close(ref _handle));
    }
}
