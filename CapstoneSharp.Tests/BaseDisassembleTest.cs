namespace CapstoneSharp.Tests;

public abstract class BaseDisassembleTest<TInstruction> where TInstruction : unmanaged, IInstruction<TInstruction>
{
    protected abstract byte[] Code { get; }
    protected abstract ulong Address { get; }

    protected abstract CapstoneDisassembler<TInstruction> Disassembler { get; }

    protected abstract void Verify(TInstruction instruction, ref int i);

    [Fact]
    public void Disassemble()
    {
        var i = 0;

        var instructions = Disassembler.Disassemble(Code, Address);
        foreach (var instruction in instructions)
        {
            Verify(instruction, ref i);
        }

        Disassembler.FreeInstructions(instructions);
    }

    [Fact]
    public unsafe void Iterate()
    {
        var i = 0;

        var instruction = Disassembler.AllocInstruction();

        var code = (ReadOnlySpan<byte>)Code;
        var address = Address;
        while (Disassembler.Iterate(ref code, ref address, instruction))
        {
            Verify(*instruction, ref i);
        }

        Disassembler.FreeInstruction(instruction);
    }

    [Fact]
    public void RefEnumerableIterate()
    {
        var i = 0;

        foreach (var instruction in Disassembler.Iterate(Code, Address))
        {
            Verify(instruction, ref i);
        }
    }

    [Fact]
    public void ToEnumerableIterate()
    {
        var i = 0;

        foreach (var instruction in Disassembler.Iterate(Code, Address).ToEnumerable())
        {
            Verify(instruction, ref i);
        }
    }
}
