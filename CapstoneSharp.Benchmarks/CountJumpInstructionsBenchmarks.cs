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

        foreach (var instruction in Disassembler.Iterate(Code.Value, Code.Address))
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

        foreach (var instruction in DisassemblerNoDetails.Iterate(Code.Value, Code.Address))
        {
            if (instruction.IsSkippedData) continue;
            switch (instruction.Id)
            {
                case CapstoneArm64InstructionId.B:
                case CapstoneArm64InstructionId.BL:
                case CapstoneArm64InstructionId.BLR:
                case CapstoneArm64InstructionId.BR:
                case CapstoneArm64InstructionId.CBNZ:
                case CapstoneArm64InstructionId.CBZ:
                case CapstoneArm64InstructionId.TBNZ:
                case CapstoneArm64InstructionId.TBZ:
                    count++;
                    break;
            }
        }

        return count;
    }

    [Benchmark]
    public unsafe int CapstoneSharp_PinnedArrayIterate()
    {
        var count = 0;

        fixed (byte* code = Code.Value)
        {
            foreach (var instruction in Disassembler.Iterate(code, (nuint)Code.Value.Length, Code.Address))
            {
                if (instruction.IsSkippedData) continue;
                if (instruction.Details.BelongsToGroup(CapstoneArm64InstructionGroup.JUMP))
                {
                    count++;
                }
            }
        }

        return count;
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
                    if (instruction->IsSkippedData) continue;
                    if (instruction->Details->BelongsToGroup(CapstoneArm64InstructionGroup.JUMP))
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    [Benchmark]
    public int CapstoneNET()
    {
        var count = 0;
        var instructions = GeeDisassembler.Iterate(Code.Value, (long)Code.Address);

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
    public int DisArm()
    {
        var count = 0;
        var instructions = Disarm.Disassembler.Disassemble(Code.Value, Code.Address, Disarm.Disassembler.Options.IgnoreErrors);

        foreach (var instruction in instructions)
        {
            switch (instruction.Mnemonic)
            {
                case Arm64Mnemonic.B:
                case Arm64Mnemonic.BL:
                case Arm64Mnemonic.BLR:
                case Arm64Mnemonic.BR:
                case Arm64Mnemonic.CBNZ:
                case Arm64Mnemonic.CBZ:
                case Arm64Mnemonic.TBNZ:
                case Arm64Mnemonic.TBZ:
                    count++;
                    break;
            }
        }

        return count;
    }
}
