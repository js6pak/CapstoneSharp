using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using CapstoneSharp.Arm64;
using Gee.External.Capstone.Arm64;

namespace CapstoneSharp.Benchmarks;

[Config(typeof(Config))]
public class CountJumpInstructionsBenchmarks : BaseDisassemblerBenchmarks
{
    public class Config : ManualConfig
    {
        public Config()
        {
            WithSummaryStyle(SummaryStyle.Default.WithSizeUnit(SizeUnit.MB));
        }
    }

    [Benchmark]
    public int CountJumpInstructions_CapstoneSharp()
    {
        var count = 0;

        foreach (var instruction in Disassembler.Iterate(Code, Code.Address))
        {
            if (instruction.Details.BelongsToGroup(CapstoneArm64InstructionGroup.JUMP))
            {
                count++;
            }
        }

        return count;
    }

    [Benchmark]
    public int CountJumpInstructions_CapstoneNET()
    {
        var count = 0;
        var instructions = GeeDisassembler.Iterate(Code, (long)Code.Address);

        foreach (var instruction in instructions)
        {
            if (instruction.Details.BelongsToGroup(Arm64InstructionGroupId.ARM64_GRP_JUMP))
            {
                count++;
            }
        }

        return count;
    }
}
