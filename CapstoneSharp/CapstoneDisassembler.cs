using System.Runtime.CompilerServices;
using CapstoneSharp.Interop;

namespace CapstoneSharp;

public abstract unsafe class CapstoneDisassembler<TInstruction> : IDisposable where TInstruction : unmanaged, IInstruction
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
        return (TInstruction*)CapstoneImports.malloc(_handle);
    }

    public void FreeInstruction(TInstruction* instruction, nuint count = 1)
    {
        CapstoneImports.free(instruction, count);
    }

    public void FreeInstructions(ReadOnlySpan<TInstruction> instructions)
    {
        FreeInstruction(UnsafeExtensions.AsPointer(ref instructions), (nuint)instructions.Length);
    }

    public bool Iterate(ref ReadOnlySpan<byte> code, ref ulong address, TInstruction* instruction)
    {
        _handle.EnsureAlive();

        if (code.Length <= 0) return false;

        fixed (byte* codePointer = code)
        {
            var size = (nuint)code.Length;

            if (!CapstoneImports.disasm_iter(_handle, &codePointer, &size, UnsafeExtensions.AsPointer(ref address), instruction))
            {
                var status = CapstoneImports.errno(_handle);
                if (status == CapstoneStatus.Ok) return false;
                throw new CapstoneException(status);
            }

            code = new ReadOnlySpan<byte>(codePointer, (int)size);
            return true;
        }
    }

    public InstructionEnumerator Iterate(ReadOnlySpan<byte> code, ulong address)
    {
        return new InstructionEnumerator(this, code, address);
    }

    public ref struct InstructionEnumerator
    {
        private readonly CapstoneDisassembler<TInstruction> _disassembler;
        private readonly TInstruction* _instruction = null;
        private ReadOnlySpan<byte> _code;
        private ulong _address;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal InstructionEnumerator(CapstoneDisassembler<TInstruction> disassembler, ReadOnlySpan<byte> code, ulong address)
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

        public readonly InstructionEnumerator GetEnumerator() => this;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _handle.EnsureAlive();

        CapstoneException.ThrowIfUnsuccessful(CapstoneImports.close(ref _handle));
    }
}
