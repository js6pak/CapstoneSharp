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
    public CapstoneArm64Disassembler Disassembler { get; } = new()
    {
        EnableInstructionDetails = true,
        EnableSkipData = true,
    };

    public CapstoneArm64Disassembler DisassemblerNoDetails { get; } = new()
    {
        EnableInstructionDetails = false,
        EnableSkipData = true,
    };

    public GeeArm64Disassembler GeeDisassembler { get; } = new(Arm64DisassembleMode.LittleEndian)
    {
        EnableInstructionDetails = true,
        EnableSkipDataMode = true,
    };

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        Disassembler.Dispose();
        DisassemblerNoDetails.Dispose();
        GeeDisassembler.Dispose();
    }

    public record CodeParam(string Name, ulong Address, byte[] Value)
    {
        public ReadOnlySpan<byte> AsSpan => Value;

        public override string ToString() => Name;
    }

    [ParamsSource(nameof(ValuesForCode))]
    public CodeParam Code { get; set; } = null!;

    public static IEnumerable<CodeParam> ValuesForCode
    {
        get
        {
            var dataSet = Environment.GetEnvironmentVariable("DATASET");
            switch (dataSet)
            {
                case "small":
                {
                    yield return new CodeParam("small test", 0x2c, new byte[] { 0x09, 0x00, 0x38, 0xd5, 0xbf, 0x40, 0x00, 0xd5, 0x0c, 0x05, 0x13, 0xd5, 0x20, 0x50, 0x02, 0x0e, 0x20, 0xe4, 0x3d, 0x0f, 0x00, 0x18, 0xa0, 0x5f, 0xa2, 0x00, 0xae, 0x9e, 0x9f, 0x37, 0x03, 0xd5, 0xbf, 0x33, 0x03, 0xd5, 0xdf, 0x3f, 0x03, 0xd5, 0x21, 0x7c, 0x02, 0x9b, 0x21, 0x7c, 0x00, 0x53, 0x00, 0x40, 0x21, 0x4b, 0xe1, 0x0b, 0x40, 0xb9, 0x20, 0x04, 0x81, 0xda, 0x20, 0x08, 0x02, 0x8b, 0x10, 0x5b, 0xe8, 0x3c });
                    yield return new CodeParam("CaGen", 0x0, new byte[] { 0xF4, 0x4F, 0xBE, 0xA9, 0xFD, 0x7B, 0x01, 0xA9, 0xFD, 0x43, 0x00, 0x91, 0xF3, 0x03, 0x00, 0xAA, 0x68, 0x06, 0x40, 0xF9, 0xE2, 0x03, 0x1F, 0xAA, 0x41, 0x20, 0x80, 0x52, 0x00, 0x01, 0x40, 0xF9, 0xF8, 0xF0, 0x4F, 0x94, 0x68, 0x06, 0x40, 0xF9, 0xE1, 0x03, 0x1F, 0xAA, 0x13, 0x05, 0x40, 0xF9, 0xE0, 0x03, 0x13, 0xAA, 0xA6, 0xF5, 0x3C, 0x94, 0xFD, 0x7B, 0x41, 0xA9, 0xE2, 0x03, 0x1F, 0xAA, 0xE1, 0x03, 0x00, 0x32, 0xE0, 0x03, 0x13, 0xAA, 0xF4, 0x4F, 0xC2, 0xA8, 0xA2, 0xF5, 0x3C, 0x14 });
                    break;
                }

                case "il2cpp":
                {
                    using var stream = typeof(CountBenchmarks).Assembly.GetManifestResourceStream("CapstoneSharp.Benchmarks.libil2cpp.so")!;
                    var elf = ELFReader.Load(stream, false);
                    var textSection = (ProgBitsSection<ulong>)elf.GetSection(".text");
                    yield return new CodeParam("libil2cpp.so .text", textSection.Offset, textSection.GetContents());
                    var il2CppSection = (ProgBitsSection<ulong>)elf.GetSection("il2cpp");
                    yield return new CodeParam("libil2cpp.so il2cpp", il2CppSection.Offset, il2CppSection.GetContents());
                    break;
                }

                default:
                    throw new ArgumentOutOfRangeException(nameof(dataSet));
            }
        }
    }
}
