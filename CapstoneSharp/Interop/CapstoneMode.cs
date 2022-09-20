namespace CapstoneSharp.Interop;

/// <summary>
/// Mode type
/// </summary>
[Flags]
internal enum CapstoneMode
{
    /// <summary>
    /// little-endian mode (default mode)
    /// </summary>
    LittleEndian = 0,

    /// <summary>
    /// 32-bit ARM
    /// </summary>
    Arm = 0,

    /// <summary>
    /// 16-bit mode (X86)
    /// </summary>
    X16 = 1 << 1,

    /// <summary>
    /// 32-bit mode (X86)
    /// </summary>
    X32 = 1 << 2,

    /// <summary>
    /// 64-bit mode (X86, PPC)
    /// </summary>
    X64 = 1 << 3,

    /// <summary>
    /// ARM's Thumb mode, including Thumb-2
    /// </summary>
    Thumb = 1 << 4,

    /// <summary>
    /// ARM's Cortex-M series
    /// </summary>
    Mclass = 1 << 5,

    /// <summary>
    /// ARMv8 A32 encodings for ARM
    /// </summary>
    V8 = 1 << 6,

    /// <summary>
    /// MicroMips mode (MIPS)
    /// </summary>
    Micro = 1 << 4,

    /// <summary>
    /// Mips III ISA
    /// </summary>
    Mips3 = 1 << 5,

    /// <summary>
    /// Mips32r6 ISA
    /// </summary>
    Mips32R6 = 1 << 6,

    /// <summary>
    /// Mips II ISA
    /// </summary>
    Mips2 = 1 << 7,

    /// <summary>
    /// SparcV9 mode (Sparc)
    /// </summary>
    V9 = 1 << 4,

    /// <summary>
    /// Quad Processing eXtensions mode (PPC)
    /// </summary>
    Qpx = 1 << 4,

    /// <summary>
    /// M68K 68000 mode
    /// </summary>
    M68K000 = 1 << 1,

    /// <summary>
    /// M68K 68010 mode
    /// </summary>
    M68K010 = 1 << 2,

    /// <summary>
    /// M68K 68020 mode
    /// </summary>
    M68K020 = 1 << 3,

    /// <summary>
    /// M68K 68030 mode
    /// </summary>
    M68K030 = 1 << 4,

    /// <summary>
    /// M68K 68040 mode
    /// </summary>
    M68K040 = 1 << 5,

    /// <summary>
    /// M68K 68060 mode
    /// </summary>
    M68K060 = 1 << 6,

    /// <summary>
    /// big-endian mode
    /// </summary>
    BigEndian = 1 << 31,

    /// <summary>
    /// Mips32 ISA (Mips)
    /// </summary>
    Mips32 = 1 << 2,

    /// <summary>
    /// Mips64 ISA (Mips)
    /// </summary>
    Mips64 = 1 << 3,

    /// <summary>
    /// M680X Hitachi 6301,6303 mode
    /// </summary>
    M680X6301 = 1 << 1,

    /// <summary>
    /// M680X Hitachi 6309 mode
    /// </summary>
    M680X6309 = 1 << 2,

    /// <summary>
    /// M680X Motorola 6800,6802 mode
    /// </summary>
    M680X6800 = 1 << 3,

    /// <summary>
    /// M680X Motorola 6801,6803 mode
    /// </summary>
    M680X6801 = 1 << 4,

    /// <summary>
    /// M680X Motorola/Freescale 6805 mode
    /// </summary>
    M680X6805 = 1 << 5,

    /// <summary>
    /// M680X Motorola/Freescale/NXP 68HC08 mode
    /// </summary>
    M680X6808 = 1 << 6,

    /// <summary>
    /// M680X Motorola 6809 mode
    /// </summary>
    M680X6809 = 1 << 7,

    /// <summary>
    /// M680X Motorola/Freescale/NXP 68HC11 mode
    /// </summary>
    M680X6811 = 1 << 8,

    /// <summary>
    /// M680X Motorola/Freescale/NXP CPU12
    /// </summary>
    M680XCpu12 = 1 << 9,

    /// <summary>
    /// M680X Freescale/NXP HCS08 mode
    /// </summary>
    M680XHcs08 = 1 << 10,
}
