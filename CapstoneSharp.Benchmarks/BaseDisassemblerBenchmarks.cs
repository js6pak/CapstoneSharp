using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;
using ELFSharp.ELF;
using ELFSharp.ELF.Sections;
using Gee.External.Capstone.Arm64;
using CapstoneArm64Disassembler = CapstoneSharp.Arm64.CapstoneArm64Disassembler;
using GeeArm64Disassembler = Gee.External.Capstone.Arm64.CapstoneArm64Disassembler;

namespace CapstoneSharp.Benchmarks;

[MemoryDiagnoser]
#if _WINDOWS
[BenchmarkDotNet.Diagnostics.Windows.Configs.NativeMemoryProfiler]
#endif
[StopOnFirstError]
public abstract class BaseDisassemblerBenchmarks
{
    protected CapstoneArm64Disassembler Disassembler { get; } = new()
    {
        EnableInstructionDetails = true,
    };

    protected GeeArm64Disassembler GeeDisassembler { get; } = new(Arm64DisassembleMode.LittleEndian)
    {
        EnableInstructionDetails = true,
    };

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        Disassembler.Dispose();
        GeeDisassembler.Dispose();
    }

    private static CodeParam ReadEmbeddedResource(string fileName)
    {
        using var stream = typeof(CountBenchmarks).Assembly.GetManifestResourceStream("CapstoneSharp.Benchmarks." + fileName)!;
        var elf = ELFReader.Load(stream, false);
        var section = (ProgBitsSection<ulong>)elf.GetSection(".text");
        return new CodeParam(fileName, section.Offset, section.GetContents());
    }

    public record CodeParam(string Name, ulong Address, byte[] Value)
    {
        public static implicit operator byte[](CodeParam param) => param.Value;
        public static implicit operator ReadOnlySpan<byte>(CodeParam param) => param.Value;
        public override string ToString() => Name;
    }

    [ParamsSource(nameof(ValuesForCode))]
    public CodeParam Code { get; set; }

    public static IEnumerable<CodeParam> ValuesForCode
    {
        get
        {
            yield return new CodeParam("small test", 0x2c, new byte[] { 0x09, 0x00, 0x38, 0xd5, 0xbf, 0x40, 0x00, 0xd5, 0x0c, 0x05, 0x13, 0xd5, 0x20, 0x50, 0x02, 0x0e, 0x20, 0xe4, 0x3d, 0x0f, 0x00, 0x18, 0xa0, 0x5f, 0xa2, 0x00, 0xae, 0x9e, 0x9f, 0x37, 0x03, 0xd5, 0xbf, 0x33, 0x03, 0xd5, 0xdf, 0x3f, 0x03, 0xd5, 0x21, 0x7c, 0x02, 0x9b, 0x21, 0x7c, 0x00, 0x53, 0x00, 0x40, 0x21, 0x4b, 0xe1, 0x0b, 0x40, 0xb9, 0x20, 0x04, 0x81, 0xda, 0x20, 0x08, 0x02, 0x8b, 0x10, 0x5b, 0xe8, 0x3c });
            yield return ReadEmbeddedResource("libil2cpp.so");
        }
    }
}
