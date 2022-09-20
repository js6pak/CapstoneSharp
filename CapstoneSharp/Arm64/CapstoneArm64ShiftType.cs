using System.Diagnostics.CodeAnalysis;

namespace CapstoneSharp.Arm64;

/// <summary>
/// ARM64 shift type
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "IdentifierTypo")]
public enum CapstoneArm64ShiftType : uint
{
    Invalid = 0,
    LSL = 1,
    MSL = 2,
    LSR = 3,
    ASR = 4,
    ROR = 5,
}
