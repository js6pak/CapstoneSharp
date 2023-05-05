namespace CapstoneSharp.Arm64;

/// <summary>
/// Vector arrangement specifier (for FloatingPoint/Advanced SIMD insn)
/// </summary>
public enum CapstoneArm64VectorArrangementSpecifier : uint
{
    Invalid = 0,
    _8B,
    _16B,
    _4H,
    _8H,
    _2S,
    _4S,
    _1D,
    _2D,
    _1Q,
}
