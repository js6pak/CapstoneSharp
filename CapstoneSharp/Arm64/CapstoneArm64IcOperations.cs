using System.Diagnostics.CodeAnalysis;

namespace CapstoneSharp.Arm64;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "IdentifierTypo")]
public enum CapstoneArm64IcOperations : uint
{
    Invalid = 0,
    IALLUIS,
    IALLU,
    IVAU,
}
