using System.Diagnostics.CodeAnalysis;
using CapstoneSharp.Arm64;

namespace CapstoneSharp.Tests;

public class Arm64DisassembleTest : BaseDisassembleTest<CapstoneArm64Instruction>
{
    protected override byte[] Code { get; } =
    {
        0xFF, 0x43, 0x00, 0xD1,
        0xE0, 0x0F, 0x00, 0xB9,
        0xE0, 0x0F, 0x40, 0xB9,
        0x00, 0x7C, 0x00, 0x1B,
        0xFF, 0x43, 0x00, 0x91,
        0xFC, 0xFF, 0xFF, 0x17,
        0xC0, 0x03, 0x5F, 0xD6,
    };

    protected override ulong Address => 0;

    protected override CapstoneDisassembler<CapstoneArm64Instruction> Disassembler { get; } = new CapstoneArm64Disassembler
    {
        EnableInstructionDetails = true,
    };

    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
    protected override void Verify(CapstoneArm64Instruction instruction, ref int i)
    {
        Assert.Equal(4, instruction.Size);

        ref var details = ref instruction.Details;
        var archDetails = details.ArchDetails;

        switch (i)
        {
            case 0:
            {
                Assert.Equal("sub", instruction.Mnemonic);
                Assert.Equal("sp, sp, #0x10", instruction.Operands);

                Assert.Equal(CapstoneArm64InstructionId.SUB, instruction.Id);
                Assert.Collection(archDetails.Operands.ToArray(),
                    operand =>
                    {
                        Assert.Equal(CapstoneArm64OperandType.Register, operand.Type);
                        Assert.Equal(CapstoneArm64RegisterId.SP, operand.Register);
                    },
                    operand =>
                    {
                        Assert.Equal(CapstoneArm64OperandType.Register, operand.Type);
                        Assert.Equal(CapstoneArm64RegisterId.SP, operand.Register);
                    },
                    operand =>
                    {
                        Assert.Equal(CapstoneArm64OperandType.Immediate, operand.Type);
                        Assert.Throws<InvalidOperationException>(() => operand.Register);
                        Assert.Equal(0x10, operand.Immediate);
                    });

                break;
            }

            case 1:
            {
                Assert.Equal("str", instruction.Mnemonic);
                Assert.Equal("w0, [sp, #0xc]", instruction.Operands);

                Assert.Equal(CapstoneArm64InstructionId.STR, instruction.Id);
                Assert.Collection(archDetails.Operands.ToArray(),
                    operand =>
                    {
                        Assert.Equal(CapstoneArm64OperandType.Register, operand.Type);
                        Assert.Equal(CapstoneArm64RegisterId.W0, operand.Register);
                    },
                    operand =>
                    {
                        Assert.Equal(CapstoneArm64OperandType.Memory, operand.Type);
                        Assert.Equal(CapstoneArm64RegisterId.SP, operand.Memory.Base);
                        Assert.Equal(CapstoneArm64RegisterId.Invalid, operand.Memory.Index);
                        Assert.Equal(0xC, operand.Memory.Displacement);
                    });

                break;
            }

            case 2:
            {
                Assert.Equal("ldr", instruction.Mnemonic);
                Assert.Equal("w0, [sp, #0xc]", instruction.Operands);

                Assert.Equal(CapstoneArm64InstructionId.LDR, instruction.Id);
                Assert.Collection(archDetails.Operands.ToArray(),
                    operand =>
                    {
                        Assert.Equal(CapstoneArm64OperandType.Register, operand.Type);
                        Assert.Equal(CapstoneArm64RegisterId.W0, operand.Register);
                    },
                    operand =>
                    {
                        Assert.Equal(CapstoneArm64OperandType.Memory, operand.Type);
                        Assert.Equal(CapstoneArm64RegisterId.SP, operand.Memory.Base);
                        Assert.Equal(CapstoneArm64RegisterId.Invalid, operand.Memory.Index);
                        Assert.Equal(0xC, operand.Memory.Displacement);
                    });

                break;
            }

            case 3:
            {
                Assert.Equal("mul", instruction.Mnemonic);
                Assert.Equal("w0, w0, w0", instruction.Operands);

                Assert.Equal(CapstoneArm64InstructionId.MUL, instruction.Id);
                Assert.Collection(archDetails.Operands.ToArray(),
                    operand =>
                    {
                        Assert.Equal(CapstoneArm64OperandType.Register, operand.Type);
                        Assert.Equal(CapstoneArm64RegisterId.W0, operand.Register);
                    },
                    operand =>
                    {
                        Assert.Equal(CapstoneArm64OperandType.Register, operand.Type);
                        Assert.Equal(CapstoneArm64RegisterId.W0, operand.Register);
                    },
                    operand =>
                    {
                        Assert.Equal(CapstoneArm64OperandType.Register, operand.Type);
                        Assert.Equal(CapstoneArm64RegisterId.W0, operand.Register);
                    });

                break;
            }

            case 4:
            {
                Assert.Equal("add", instruction.Mnemonic);
                Assert.Equal("sp, sp, #0x10", instruction.Operands);

                Assert.Equal(CapstoneArm64InstructionId.ADD, instruction.Id);
                Assert.Collection(archDetails.Operands.ToArray(),
                    operand =>
                    {
                        Assert.Equal(CapstoneArm64OperandType.Register, operand.Type);
                        Assert.Equal(CapstoneArm64RegisterId.SP, operand.Register);
                    },
                    operand =>
                    {
                        Assert.Equal(CapstoneArm64OperandType.Register, operand.Type);
                        Assert.Equal(CapstoneArm64RegisterId.SP, operand.Register);
                    },
                    operand =>
                    {
                        Assert.Equal(CapstoneArm64OperandType.Immediate, operand.Type);
                        Assert.Equal(0x10, operand.Immediate);
                    });

                break;
            }

            case 5:
            {
                Assert.Equal("b", instruction.Mnemonic);
                Assert.Equal("#4", instruction.Operands);

                Assert.Equal(CapstoneArm64InstructionId.B, instruction.Id);
                Assert.Collection(details.Groups.ToArray(),
                    group => Assert.Equal(CapstoneArm64InstructionGroup.JUMP, group),
                    group => Assert.Equal(CapstoneArm64InstructionGroup.BRANCH_RELATIVE, group)
                );
                Assert.True(details.BelongsToGroup(CapstoneArm64InstructionGroup.JUMP));
                Assert.True(details.BelongsToGroup(CapstoneArm64InstructionGroup.BRANCH_RELATIVE));

                Assert.Collection(archDetails.Operands.ToArray(),
                    operand =>
                    {
                        Assert.Equal(CapstoneArm64OperandType.Immediate, operand.Type);
                        Assert.Equal(4, operand.Immediate);
                    });

                break;
            }

            case 6:
            {
                Assert.Equal("ret", instruction.Mnemonic);
                Assert.Equal(string.Empty, instruction.Operands);

                Assert.Equal(CapstoneArm64InstructionId.RET, instruction.Id);
                Assert.Collection(details.Groups.ToArray(), group => Assert.Equal(CapstoneArm64InstructionGroup.RET, group));
                Assert.True(details.BelongsToGroup(CapstoneArm64InstructionGroup.RET));

                Assert.Empty(archDetails.Operands.ToArray());

                break;
            }

            default:
                throw new ArgumentOutOfRangeException(nameof(instruction));
        }

        i++;
    }
}
