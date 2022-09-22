using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using GeeArm64Disassembler = Gee.External.Capstone.Arm64.CapstoneArm64Disassembler;

namespace CapstoneSharp.Benchmarks;

public class CountBenchmarks : BaseDisassemblerBenchmarks
{
    [Benchmark]
    public int Count_Disassemble()
    {
        var span = Disassembler.Disassemble(Code, Code.Address);
        var count = span.Length;
        Disassembler.FreeInstructions(span);
        return count;
    }

    [Benchmark]
    public unsafe int Count_ManualIterate()
    {
        var count = 0;

        var ins = Disassembler.AllocInstruction();

        var code = (ReadOnlySpan<byte>)Code;
        var address = Code.Address;
        while (Disassembler.Iterate(ref code, ref address, ins))
        {
            count++;
        }

        Disassembler.FreeInstruction(ins);

        return count;
    }

    [Benchmark]
    public int Count_RefEnumerableIterate()
    {
        var count = 0;

        foreach (var instruction in Disassembler.Iterate(Code, Code.Address))
        {
            count++;
        }

        return count;
    }

    [Benchmark]
    public int Count_IEnumerableIterate()
    {
        var count = 0;

        foreach (var instruction in Disassembler.Iterate(Code, Code.Address).ToEnumerable())
        {
            count++;
        }

        return count;
    }

    [Benchmark]
    public int Count_CapstoneNET()
    {
        var instructions = GeeDisassembler.Iterate(Code, (long)Code.Address);
        return instructions.Count();
    }

    [Benchmark]
    public int Count_DisArm()
    {
        return Disarm.Disassembler.DisassembleOnDemand(Code.Value, Code.Address, continueOnError: true).Count();
    }
}
