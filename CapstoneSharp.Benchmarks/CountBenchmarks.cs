using BenchmarkDotNet.Attributes;
using GeeArm64Disassembler = Gee.External.Capstone.Arm64.CapstoneArm64Disassembler;

namespace CapstoneSharp.Benchmarks;

public class CountBenchmarks : BaseDisassemblerBenchmarks
{
    [Benchmark]
    public int CapstoneSharp_Iterate()
    {
        return Disassembler.Iterate(Code.Value, Code.Address).Count();
    }

    [Benchmark]
    public unsafe int CapstoneSharp_UnsafeIterate()
    {
        var count = 0;

        using (Disassembler.AllocInstruction(out var instruction))
        {
            fixed (byte* code = Code.Value)
            {
                var size = (nuint)Code.Value.Length;
                var address = Code.Address;
                while (Disassembler.UnsafeIterate(&code, &size, &address, instruction))
                {
                    count++;
                }
            }
        }

        return count;
    }

    [Benchmark]
    public int CapstoneNET()
    {
        return GeeDisassembler.Iterate(Code.Value, (long)Code.Address).Count();
    }

    [Benchmark]
    public int DisArm()
    {
        return Disarm.Disassembler.Disassemble(Code.Value, Code.Address, Disarm.Disassembler.Options.IgnoreErrors).Count();
    }
}
