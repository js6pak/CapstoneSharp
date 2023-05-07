namespace CapstoneSharp.Tests;

public abstract class BaseDisassembleTest<TId, TInstruction, TUnsafeInstruction> : IDisposable
    where TId : unmanaged, Enum
    where TInstruction : ICapstoneInstruction<TId>
    where TUnsafeInstruction : unmanaged, ICapstoneInstruction<TId>
{
    protected abstract byte[] Code { get; }
    protected abstract ulong Address { get; }

    protected abstract CapstoneDisassembler<TId, TInstruction, TUnsafeInstruction> Disassembler { get; }

    protected abstract void Verify(int i, TInstruction instruction);
    protected abstract unsafe void Verify(int i, TUnsafeInstruction* instruction);

    [Fact]
    public void ArrayIterate()
    {
        var i = 0;

        foreach (var instruction in Disassembler.Iterate(Code, Address))
        {
            Verify(i, instruction);
            i++;
        }
    }

    [Fact]
    public unsafe void PinnedArrayIterate()
    {
        var i = 0;

        fixed (byte* codePtr = Code)
        {
            foreach (var instruction in Disassembler.Iterate(codePtr, (nuint)Code.Length, Address))
            {
                Verify(i, instruction);
                i++;
            }
        }
    }

    [Fact]
    public void SpanIterate()
    {
        var i = 0;

        foreach (var instruction in Disassembler.Iterate(Code.AsSpan(), Address))
        {
            Verify(i, instruction);
            i++;
        }
    }

    [Fact]
    public unsafe void UnsafeIterate()
    {
        var i = 0;

        using (Disassembler.AllocInstruction(out var instruction))
        {
            var code = (ReadOnlySpan<byte>)Code;
            var address = Address;
            while (Disassembler.UnsafeIterate(ref code, &address, instruction))
            {
                Verify(i, instruction);
                i++;
            }
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        Disassembler.Dispose();
    }
}
