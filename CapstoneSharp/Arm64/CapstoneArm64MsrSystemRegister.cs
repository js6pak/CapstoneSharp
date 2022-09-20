using System.Diagnostics.CodeAnalysis;

namespace CapstoneSharp.Arm64;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "IdentifierTypo")]
public enum CapstoneArm64MsrSystemRegister : uint
{
    DBGDTRTX_EL0 = 0x9828,
    OSLAR_EL1 = 0x8084,
    PMSWINC_EL0 = 0xdce4,
    TRCOSLAR = 0x8884,
    TRCLAR = 0x8be6,
    ICC_EOIR1_EL1 = 0xc661,
    ICC_EOIR0_EL1 = 0xc641,
    ICC_DIR_EL1 = 0xc659,
    ICC_SGI1R_EL1 = 0xc65d,
    ICC_ASGI1R_EL1 = 0xc65e,
    ICC_SGI0R_EL1 = 0xc65f,
}
