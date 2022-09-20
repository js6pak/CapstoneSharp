namespace CapstoneSharp.Interop;

/// <summary>
/// Architecture type
/// </summary>
internal enum CapstoneArch : uint
{
    /// <summary>
    /// ARM architecture (including Thumb, Thumb-2)
    /// </summary>
    Arm = 0,

    /// <summary>
    /// ARM-64, also called AArch64
    /// </summary>
    Arm64,

    /// <summary>
    /// Mips architecture
    /// </summary>
    Mips,

    /// <summary>
    /// X86 architecture (including x86 & x86-64)
    /// </summary>
    X86,

    /// <summary>
    /// PowerPC architecture
    /// </summary>
    PowerPc,

    /// <summary>
    /// Sparc architecture
    /// </summary>
    Sparc,

    /// <summary>
    /// SystemZ architecture
    /// </summary>
    SystemZ,

    /// <summary>
    /// XCore architecture
    /// </summary>
    XCore,

    /// <summary>
    /// 68K architecture
    /// </summary>
    M68K,

    /// <summary>
    /// TMS320C64x architecture
    /// </summary>
    Tms320C64X,

    /// <summary>
    /// 680X architecture
    /// </summary>
    M680X,

    /// <summary>
    /// Ethereum architecture
    /// </summary>
    Evm,

    Max,
    All = 0xFFFF,
}
