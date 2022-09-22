using BenchmarkDotNet.Attributes;
using CapstoneSharp.Arm64;
using Disarm;
using Gee.External.Capstone.Arm64;

namespace CapstoneSharp.Benchmarks;

public class CountJumpInstructionsBenchmarks : BaseDisassemblerBenchmarks
{
    [Benchmark]
    public int CapstoneSharp_Iterate()
    {
        var count = 0;

        foreach (var instruction in Disassembler.Iterate(Code, Code.Address))
        {
            if (instruction.IsSkippedData) continue;
            if (instruction.Details.BelongsToGroup(CapstoneArm64InstructionGroup.JUMP))
            {
                count++;
            }
        }

        return count;
    }

    [Benchmark]
    public int CapstoneSharp_Iterate_NoDetails()
    {
        var count = 0;

        foreach (var instruction in DisassemblerNoDetails.Iterate(Code, Code.Address))
        {
            if (instruction.IsSkippedData) continue;
            if (instruction.Id is CapstoneArm64InstructionId.B or CapstoneArm64InstructionId.BL or CapstoneArm64InstructionId.BLR or CapstoneArm64InstructionId.BR)
            {
                count++;
            }
        }

        return count;
    }


    [Benchmark]
    public int CapstoneSharp_Disassemble()
    {
        var count = 0;

        var span = Disassembler.Disassemble(Code, Code.Address);
        foreach (var instruction in span)
        {
            if (instruction.IsSkippedData) continue;
            if (instruction.Details.BelongsToGroup(CapstoneArm64InstructionGroup.JUMP))
            {
                count++;
            }
        }

        Disassembler.FreeInstructions(span);

        return count;
    }

    [Benchmark]
    public int CapstoneNET()
    {
        var count = 0;
        var instructions = GeeDisassembler.Iterate(Code, (long)Code.Address);

        foreach (var instruction in instructions)
        {
            if (instruction.IsSkippedData) continue;
            if (instruction.Details.BelongsToGroup(Arm64InstructionGroupId.ARM64_GRP_JUMP))
            {
                count++;
            }
        }

        return count;
    }

    [Benchmark]
    public int DisArm_Disassemble()
    {
        var count = 0;
        var instructions = Disarm.Disassembler.Disassemble(Code.Value, Code.Address, continueOnError: true).Instructions;

        foreach (var instruction in instructions)
        {
            if (instruction.Mnemonic is Arm64Mnemonic.B or Arm64Mnemonic.BL or Arm64Mnemonic.BLR or Arm64Mnemonic.BR)
            {
                count++;
            }
        }

        return count;
    }

    [Benchmark]
    public int DisArm_DisassembleOnDemand()
    {
        var count = 0;
        var instructions = Disarm.Disassembler.DisassembleOnDemand(Code.Value, Code.Address, continueOnError: true);

        foreach (var instruction in instructions)
        {
            if (instruction.Mnemonic is Arm64Mnemonic.B or Arm64Mnemonic.BL or Arm64Mnemonic.BLR or Arm64Mnemonic.BR)
            {
                count++;
            }
        }

        return count;
    }
}
