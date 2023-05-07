using CapstoneSharp.Arm64;

namespace CapstoneSharp.Tests;

public sealed class OptionsTest
{
    [Fact]
    public void SkipData()
    {
        using var disassembler = new CapstoneArm64Disassembler
        {
            EnableSkipData = true,
        };

        var code = "test"u8;

        foreach (var instruction in disassembler.Iterate(code, 0))
        {
            Assert.True(instruction.IsSkippedData);
            Assert.Equal(CapstoneArm64InstructionId.Invalid, instruction.Id);
            Assert.Equal(".byte", instruction.Mnemonic);
            Assert.Equal("0x74, 0x65, 0x73, 0x74", instruction.Operands);
            Assert.Throws<InvalidOperationException>(() => instruction.Details);
        }
    }

    [Fact]
    public void Details()
    {
        using var disassemblerNoDetails = new CapstoneArm64Disassembler { EnableInstructionDetails = false };
        using var disassemblerDetails = new CapstoneArm64Disassembler { EnableInstructionDetails = true };
        using var disassemblerDetailsSkipData = new CapstoneArm64Disassembler { EnableInstructionDetails = true, EnableSkipData = true };

        ReadOnlySpan<byte> code = stackalloc byte[] { 0xFF, 0x43, 0x00, 0xD1 };
        var data = "test"u8;

        foreach (var instruction in disassemblerNoDetails.Iterate(code, 0))
        {
            Assert.Throws<InvalidOperationException>(() => instruction.Details);
        }

        foreach (var instruction in disassemblerDetails.Iterate(code, 0))
        {
            Assert.NotNull(instruction.Details);
        }

        foreach (var instruction in disassemblerDetailsSkipData.Iterate(data, 0))
        {
            Assert.Throws<InvalidOperationException>(() => instruction.Details);
        }
    }
}
