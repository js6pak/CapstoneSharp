using System.Diagnostics.CodeAnalysis;
using CapstoneSharp.Arm;

namespace CapstoneSharp.Tests;

public sealed class ArmDisassembleTest : BaseDisassembleTest<CapstoneArmInstructionId, CapstoneArmInstruction, UnsafeCapstoneArmInstruction>
{
    protected override byte[] Code { get; } =
    {
        0x04, 0xD0, 0x4D, 0xE2,
        0x00, 0x00, 0x8D, 0xE5,
        0x00, 0x10, 0x9D, 0xE5,
        0x91, 0x01, 0x00, 0xE0,
        0x04, 0xD0, 0x8D, 0xE2,
        0x1E, 0xFF, 0x2F, 0xE1,
    };

    protected override ulong Address => 0;

    protected override CapstoneArmDisassembler Disassembler { get; } = new()
    {
        EnableInstructionDetails = true,
    };

    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
    private void Verify<TInstruction, TInstructionDetails>(int i, ref TInstruction instruction, ref TInstructionDetails details)
        where TInstruction : ICapstoneArmInstruction
        where TInstructionDetails : ICapstoneArmInstructionDetails
    {
        Assert.Equal(4, instruction.Size);
        Assert.True(instruction.Bytes.SequenceEqual(Code.AsSpan(i * 4, 4)));
        Assert.True(details.BelongsToGroup(CapstoneArmInstructionGroup.ARM));

        var archDetails = details.ArchDetails;

        switch (i)
        {
            case 0:
            {
                Assert.Equal("sub", instruction.Mnemonic);
                Assert.Equal("sp, sp, #4", instruction.Operands);

                Assert.Equal(CapstoneArmInstructionId.SUB, instruction.Id);
                Assert.Collection(archDetails.Operands.ToArray(),
                    operand =>
                    {
                        Assert.Equal(CapstoneArmOperandType.Register, operand.Type);
                        Assert.Equal(CapstoneArmRegisterId.SP, operand.Register);
                    },
                    operand =>
                    {
                        Assert.Equal(CapstoneArmOperandType.Register, operand.Type);
                        Assert.Equal(CapstoneArmRegisterId.SP, operand.Register);
                    },
                    operand =>
                    {
                        Assert.Equal(CapstoneArmOperandType.Immediate, operand.Type);
                        Assert.Throws<InvalidOperationException>(() => operand.Register);
                        Assert.Equal(0x4, operand.Immediate);
                    });

                break;
            }

            case 1:
            {
                Assert.Equal("str", instruction.Mnemonic);
                Assert.Equal("r0, [sp]", instruction.Operands);

                Assert.Equal(CapstoneArmInstructionId.STR, instruction.Id);
                Assert.Collection(archDetails.Operands.ToArray(),
                    operand =>
                    {
                        Assert.Equal(CapstoneArmOperandType.Register, operand.Type);
                        Assert.Equal(CapstoneArmRegisterId.R0, operand.Register);
                    },
                    operand =>
                    {
                        Assert.Equal(CapstoneArmOperandType.Memory, operand.Type);
                        Assert.Equal(CapstoneArmRegisterId.SP, operand.Memory.Base);
                    });

                break;
            }

            case 2:
            {
                Assert.Equal("ldr", instruction.Mnemonic);
                Assert.Equal("r1, [sp]", instruction.Operands);

                Assert.Equal(CapstoneArmInstructionId.LDR, instruction.Id);
                Assert.Collection(archDetails.Operands.ToArray(),
                    operand =>
                    {
                        Assert.Equal(CapstoneArmOperandType.Register, operand.Type);
                        Assert.Equal(CapstoneArmRegisterId.R1, operand.Register);
                    },
                    operand =>
                    {
                        Assert.Equal(CapstoneArmOperandType.Memory, operand.Type);
                        Assert.Equal(CapstoneArmRegisterId.SP, operand.Memory.Base);
                    });

                break;
            }

            case 3:
            {
                Assert.Equal("mul", instruction.Mnemonic);
                Assert.Equal("r0, r1, r1", instruction.Operands);

                Assert.Equal(CapstoneArmInstructionId.MUL, instruction.Id);
                Assert.Collection(archDetails.Operands.ToArray(),
                    operand =>
                    {
                        Assert.Equal(CapstoneArmOperandType.Register, operand.Type);
                        Assert.Equal(CapstoneArmRegisterId.R0, operand.Register);
                    },
                    operand =>
                    {
                        Assert.Equal(CapstoneArmOperandType.Register, operand.Type);
                        Assert.Equal(CapstoneArmRegisterId.R1, operand.Register);
                    },
                    operand =>
                    {
                        Assert.Equal(CapstoneArmOperandType.Register, operand.Type);
                        Assert.Equal(CapstoneArmRegisterId.R1, operand.Register);
                    });

                break;
            }

            case 4:
            {
                Assert.Equal("add", instruction.Mnemonic);
                Assert.Equal("sp, sp, #4", instruction.Operands);

                Assert.Equal(CapstoneArmInstructionId.ADD, instruction.Id);
                Assert.Collection(archDetails.Operands.ToArray(),
                    operand =>
                    {
                        Assert.Equal(CapstoneArmOperandType.Register, operand.Type);
                        Assert.Equal(CapstoneArmRegisterId.SP, operand.Register);
                    },
                    operand =>
                    {
                        Assert.Equal(CapstoneArmOperandType.Register, operand.Type);
                        Assert.Equal(CapstoneArmRegisterId.SP, operand.Register);
                    },
                    operand =>
                    {
                        Assert.Equal(CapstoneArmOperandType.Immediate, operand.Type);
                        Assert.Equal(4, operand.Immediate);
                    });

                break;
            }

            case 5:
            {
                Assert.Equal("bx", instruction.Mnemonic);
                Assert.Equal("lr", instruction.Operands);

                Assert.Equal(CapstoneArmInstructionId.BX, instruction.Id);
                Assert.Collection(archDetails.Operands.ToArray(),
                    operand =>
                    {
                        Assert.Equal(CapstoneArmOperandType.Register, operand.Type);
                        Assert.Equal(CapstoneArmRegisterId.LR, operand.Register);
                    });
                Assert.True(details.BelongsToGroup(CapstoneArmInstructionGroup.JUMP));

                break;
            }

            default:
                throw new ArgumentOutOfRangeException(nameof(instruction));
        }
    }

    protected override void Verify(int i, CapstoneArmInstruction instruction)
    {
        var details = instruction.Details;

        Verify(i, ref instruction, ref details);
    }

    protected override unsafe void Verify(int i, UnsafeCapstoneArmInstruction* instruction)
    {
        Verify(i, ref *instruction, ref *instruction->Details);
    }
}
