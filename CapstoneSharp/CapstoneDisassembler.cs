using System.Collections;
using System.Runtime.CompilerServices;
using CapstoneSharp.Interop;

namespace CapstoneSharp;

public abstract unsafe class CapstoneDisassembler<TInstruction> : IDisposable where TInstruction : unmanaged, IInstruction<TInstruction>
{
    private CapstoneDisassemblerHandle _handle;

    private protected CapstoneDisassembler(CapstoneArch arch, CapstoneMode mode)
    {
        CapstoneException.ThrowIfUnsuccessful(CapstoneImports.open(arch, mode, out _handle));
    }

    private protected void SetOption(CapstoneOptionType type, CapstoneOptionValue value)
    {
        _handle.EnsureAlive();
        CapstoneException.ThrowIfUnsuccessful(CapstoneImports.option(_handle, type, (nuint)value));
    }

    private protected void SetOption(CapstoneOptionType type, bool value)
    {
        SetOption(type, value ? CapstoneOptionValue.On : CapstoneOptionValue.Off);
    }

    private bool _enableInstructionDetails;

    public bool EnableInstructionDetails
    {
        get => _enableInstructionDetails;
        set => SetOption(CapstoneOptionType.Detail, _enableInstructionDetails = value);
    }

    private bool _enableSkipData;

    public bool EnableSkipData
    {
        get => _enableSkipData;
        set => SetOption(CapstoneOptionType.SkipData, _enableSkipData = value);
    }

    public ReadOnlySpan<TInstruction> Disassemble(ReadOnlySpan<byte> code, ulong address, int count = 0)
    {
        _handle.EnsureAlive();

        fixed (byte* codePointer = code)
        {
            count = (int)CapstoneImports.disasm(_handle, codePointer, (nuint)code.Length, address, (nuint)count, out var instructions);
            if (count == 0) CapstoneException.ThrowIfUnsuccessful(CapstoneImports.errno(_handle));
            return new ReadOnlySpan<TInstruction>(instructions, count);
        }
    }

    public TInstruction* AllocInstruction()
    {
#if NET6_0_OR_GREATER
        // Use our own alloc to save ~1000 bytes by allocating specific arch details struct
        return TInstruction.Alloc(EnableInstructionDetails);
#else
        return (TInstruction*)CapstoneImports.malloc(_handle);
#endif
    }

    public void FreeInstruction(TInstruction* instruction, nuint count = 1)
    {
#if NET6_0_OR_GREATER
        TInstruction.Free(instruction, count);
#else
        CapstoneImports.free(instruction, count);
#endif
    }

    public void FreeInstructions(ReadOnlySpan<TInstruction> instructions)
    {
        FreeInstruction(UnsafeExtensions.AsPointer(ref instructions), (nuint)instructions.Length);
    }

    public bool Iterate(byte** code, nuint* size, ref ulong address, TInstruction* instruction)
    {
        _handle.EnsureAlive();

        if (*size <= 0) return false;

        if (!CapstoneImports.disasm_iter(_handle, code, size, UnsafeExtensions.AsPointer(ref address), instruction))
        {
            var status = CapstoneImports.errno(_handle);
            if (status == CapstoneStatus.Ok) return false;
            throw new CapstoneException(status);
        }

        return true;
    }

    public bool Iterate(ref ReadOnlySpan<byte> code, ref ulong address, TInstruction* instruction)
    {
        _handle.EnsureAlive();

        if (code.Length <= 0) return false;

        fixed (byte* codePointer = code)
        {
            var size = (nuint)code.Length;
            var result = Iterate(&codePointer, &size, ref address, instruction);
            code = new ReadOnlySpan<byte>(codePointer, (int)size);
            return result;
        }
    }

    public InstructionEnumerator Iterate(byte* code, nuint size, ulong address)
    {
        return new InstructionEnumerator(this, code, size, address);
    }

    public RefInstructionEnumerator Iterate(ReadOnlySpan<byte> code, ulong address)
    {
        return new RefInstructionEnumerator(this, code, address);
    }

    public struct InstructionEnumerator : IEnumerable<TInstruction>, IEnumerator<TInstruction>
    {
        private readonly CapstoneDisassembler<TInstruction> _disassembler;
        private readonly TInstruction* _instruction;
        private byte* _code;
        private nuint _size;
        private ulong _address;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal InstructionEnumerator(CapstoneDisassembler<TInstruction> disassembler, byte* code, nuint size, ulong address)
        {
            _disassembler = disassembler;
            _instruction = disassembler.AllocInstruction();
            _code = code;
            _size = size;
            _address = address;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            fixed (byte** code = &_code)
            fixed (nuint* size = &_size)
            {
                return _disassembler.Iterate(code, size, ref _address, _instruction);
            }
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator.Current => Current;

        public TInstruction Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => *_instruction;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            _disassembler.FreeInstruction(_instruction);
        }

        public InstructionEnumerator GetEnumerator() => this;
        IEnumerator<TInstruction> IEnumerable<TInstruction>.GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => this;
    }

    public ref struct RefInstructionEnumerator
    {
        private readonly CapstoneDisassembler<TInstruction> _disassembler;
        private readonly TInstruction* _instruction;
        private ReadOnlySpan<byte> _code;
        private ulong _address;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RefInstructionEnumerator(CapstoneDisassembler<TInstruction> disassembler, ReadOnlySpan<byte> code, ulong address)
        {
            _disassembler = disassembler;
            _instruction = disassembler.AllocInstruction();
            _code = code;
            _address = address;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            return _disassembler.Iterate(ref _code, ref _address, _instruction);
        }

        public readonly TInstruction Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => *_instruction;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void Dispose()
        {
            _disassembler.FreeInstruction(_instruction);
        }

        public readonly RefInstructionEnumerator GetEnumerator() => this;

        public InstructionEnumerator ToEnumerable() => new InstructionEnumerator(_disassembler, UnsafeExtensions.AsPointer(ref _code), (nuint)_code.Length, _address);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _handle.EnsureAlive();

        CapstoneException.ThrowIfUnsuccessful(CapstoneImports.close(ref _handle));
    }
}
