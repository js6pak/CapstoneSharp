using System.Runtime.CompilerServices;
using CapstoneSharp.Arm64;

namespace CapstoneSharp.Tests;

public sealed class GenericsValidationTest
{
    private static void Check(Type type)
    {
        var exception = Assert.Throws<TypeInitializationException>(() =>
        {
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        });

        Exception innermostException = exception;

        while (innermostException.InnerException != null)
        {
            innermostException = innermostException.InnerException;
        }

        Assert.IsType<InvalidProgramException>(innermostException);
    }

    [Fact]
    public void TestStaticConstructors()
    {
        RuntimeHelpers.RunClassConstructor(typeof(CapstoneArm64Instruction).TypeHandle);
        Check(typeof(CapstoneInstruction<IllegalId, CapstoneArm64RegisterId, CapstoneArm64InstructionGroup, CapstoneArm64InstructionArchDetails>));
        Check(typeof(CapstoneInstruction<CapstoneArm64InstructionId, IllegalRegister, CapstoneArm64InstructionGroup, CapstoneArm64InstructionArchDetails>));
        Check(typeof(CapstoneInstruction<CapstoneArm64InstructionId, CapstoneArm64RegisterId, IllegalGroup, CapstoneArm64InstructionArchDetails>));
    }

    public enum IllegalId
    {
    }

    public enum IllegalRegister
    {
    }

    public enum IllegalGroup
    {
    }
}
