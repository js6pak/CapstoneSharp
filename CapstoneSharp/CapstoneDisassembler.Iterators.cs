using System.Buffers;
using System.Collections;

namespace CapstoneSharp;

public abstract unsafe partial class CapstoneDisassembler<TId, TInstruction, TUnsafeInstruction>
{
    /// <summary>
    /// Returns an enumerator that iteratively disassembles the instructions in provided <paramref name="code"/>.
    /// </summary>
    /// <param name="code">Code to disassemble.</param>
    /// <param name="size">Size of the <paramref name="code"/> in bytes.</param>
    /// <param name="address">Address of the first instruction.</param>
    /// <returns>Returns an enumerator that iteratively disassembles the instructions in provided <paramref name="code"/></returns>
    public IEnumerable<TInstruction> Iterate(byte* code, nuint size, ulong address)
    {
        _handle.EnsureAlive();
        return new InstructionEnumerator(this, code, size, address);
    }

    /// <inheritdoc cref="Iterate(byte*,nuint,ulong)"/>
    /// <param name="code">Code to disassemble.</param>
    /// <param name="address">Address of the first instruction.</param>
    public IEnumerable<TInstruction> Iterate(ReadOnlyMemory<byte> code, ulong address)
    {
        _handle.EnsureAlive();
        return new MemoryInstructionEnumerator(this, code, address);
    }

    /// <inheritdoc cref="Iterate(byte*,nuint,ulong)"/>
    /// <param name="code">Code to disassemble.</param>
    /// <param name="address">Address of the first instruction.</param>
    public IEnumerable<TInstruction> Iterate(byte[] code, ulong address)
    {
        _handle.EnsureAlive();
        return Iterate((ReadOnlyMemory<byte>)code, address);
    }

    /// <inheritdoc cref="Iterate(byte*,nuint,ulong)"/>
    /// <param name="code">Code to disassemble.</param>
    /// <param name="address">Address of the first instruction.</param>
    public RefInstructionEnumerator Iterate(ReadOnlySpan<byte> code, ulong address)
    {
        _handle.EnsureAlive();
        return new RefInstructionEnumerator(this, code, address);
    }

    [DoesNotReturn]
    private static void ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen()
    {
        throw new InvalidOperationException("Enumeration has either not started or has already finished.");
    }

    /// <summary>
    /// Iteratively disassembles the instructions in byte* code.
    /// </summary>
    internal sealed class InstructionEnumerator : IEnumerable<TInstruction>, IEnumerator<TInstruction>
    {
        private readonly CapstoneDisassembler<TId, TInstruction, TUnsafeInstruction> _disassembler;
        private byte* _code;
        private nuint _size;
        private ulong _address;
        private TInstruction? _current;

        internal InstructionEnumerator(CapstoneDisassembler<TId, TInstruction, TUnsafeInstruction> disassembler, byte* code, nuint size, ulong address)
        {
            _disassembler = disassembler;
            _code = code;
            _size = size;
            _address = address;
        }

        /// <inheritdoc />
        public bool MoveNext()
        {
            fixed (byte** code = &_code)
            fixed (nuint* size = &_size)
            fixed (ulong* address = &_address)
            {
                var unsafeInstruction = _disassembler.AllocInstruction();
                var result = _disassembler.UnsafeIterate(code, size, address, unsafeInstruction);
                _current = result ? _disassembler.WrapUnsafeInstruction(unsafeInstruction) : default;
                return result;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
        }

        /// <inheritdoc />
        public TInstruction Current
        {
            get
            {
                if (_current == null) ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                return _current;
            }
        }

        object IEnumerator.Current => Current;

        /// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
        public InstructionEnumerator GetEnumerator() => this;

        IEnumerator<TInstruction> IEnumerable<TInstruction>.GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => this;
        void IEnumerator.Reset() => throw new NotSupportedException();
    }

    /// <summary>
    /// Iteratively disassembles the instructions in <see cref="ReadOnlyMemory{Byte}"/> code.
    /// </summary>
    internal sealed class MemoryInstructionEnumerator : IEnumerable<TInstruction>, IEnumerator<TInstruction>
    {
        private readonly CapstoneDisassembler<TId, TInstruction, TUnsafeInstruction> _disassembler;
        private MemoryHandle _memoryHandle;
        private byte* _code;
        private nuint _size;
        private ulong _address;
        private TInstruction? _current;

        internal MemoryInstructionEnumerator(CapstoneDisassembler<TId, TInstruction, TUnsafeInstruction> disassembler, ReadOnlyMemory<byte> memory, ulong address)
        {
            _disassembler = disassembler;
            _memoryHandle = memory.Pin();
            _code = (byte*)_memoryHandle.Pointer;
            _size = (nuint)memory.Length;
            _address = address;
        }

        /// <inheritdoc />
        public bool MoveNext()
        {
            fixed (byte** code = &_code)
            fixed (nuint* size = &_size)
            fixed (ulong* address = &_address)
            {
                var unsafeInstruction = _disassembler.AllocInstruction();
                var result = _disassembler.UnsafeIterate(code, size, address, unsafeInstruction);
                _current = result ? _disassembler.WrapUnsafeInstruction(unsafeInstruction) : default;
                return result;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _memoryHandle.Dispose();
        }

        /// <inheritdoc />
        public TInstruction Current
        {
            get
            {
                if (_current == null) ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                return _current;
            }
        }

        object IEnumerator.Current => Current;

        /// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
        public MemoryInstructionEnumerator GetEnumerator() => this;

        IEnumerator<TInstruction> IEnumerable<TInstruction>.GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => this;
        void IEnumerator.Reset() => throw new NotSupportedException();
    }

    /// <summary>
    /// Iteratively disassembles the instructions in <see cref="ReadOnlySpan{Byte}"/> code.
    /// </summary>
    public ref struct RefInstructionEnumerator
    {
        private readonly CapstoneDisassembler<TId, TInstruction, TUnsafeInstruction> _disassembler;
        private ReadOnlySpan<byte> _code;
        private ulong _address;
        private TInstruction? _current;

        internal RefInstructionEnumerator(CapstoneDisassembler<TId, TInstruction, TUnsafeInstruction> disassembler, ReadOnlySpan<byte> code, ulong address)
        {
            _disassembler = disassembler;
            _code = code;
            _address = address;
        }

        /// <inheritdoc cref="IEnumerator.MoveNext" />
        public bool MoveNext()
        {
            fixed (ulong* address = &_address)
            {
                var unsafeInstruction = _disassembler.AllocInstruction();
                var result = _disassembler.UnsafeIterate(ref _code, address, unsafeInstruction);
                _current = result ? _disassembler.WrapUnsafeInstruction(unsafeInstruction) : default;
                return result;
            }
        }

        /// <inheritdoc cref="IEnumerator{T}.Current" />
        public TInstruction Current
        {
            get
            {
                if (_current == null) ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                return _current;
            }
        }

        /// <inheritdoc cref="IDisposable.Dispose" />
        public readonly void Dispose()
        {
        }

        /// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
        public readonly RefInstructionEnumerator GetEnumerator() => this;
    }
}
