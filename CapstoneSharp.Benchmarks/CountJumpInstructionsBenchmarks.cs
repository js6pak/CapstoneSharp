using BenchmarkDotNet.Attributes;
using CapstoneSharp.Arm64;
using Gee.External.Capstone.Arm64;

namespace CapstoneSharp.Benchmarks;

public class CountJumpInstructionsBenchmarks : BaseDisassemblerBenchmarks
{
    [Benchmark]
    public int CountJumpInstructions_CapstoneSharp_Iterate()
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
    public int CountJumpInstructions_CapstoneSharp_Disassemble()
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
    public int CountJumpInstructions_CapstoneNET()
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
}
