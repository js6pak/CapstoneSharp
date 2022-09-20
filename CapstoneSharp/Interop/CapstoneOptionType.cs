namespace CapstoneSharp.Interop;

/// <summary>
/// Runtime option for the disassembled engine
/// </summary>
internal enum CapstoneOptionType : uint
{
    /// <summary>
    /// No option specified
    /// </summary>
    Invalid = 0,

    /// <summary>
    ///  Assembly output syntax
    /// </summary>
    Syntax,

    /// <summary>
    /// Break down instruction structure into details
    /// </summary>
    Detail,

    /// <summary>
    /// Change engine's mode at run-time
    /// </summary>
    Mode,

    /// <summary>
    /// User-defined dynamic memory related functions
    /// </summary>
    Memory,

    /// <summary>
    /// Skip data when disassembling. Then engine is in SKIPDATA mode
    /// </summary>
    SkipData,

    /// <summary>
    /// Setup user-defined function for SKIPDATA option
    /// </summary>
    SkipDataSetup,

    /// <summary>
    /// Customize instruction mnemonic
    /// </summary>
    Mnemonic,

    /// <summary>
    /// print immediate operands in unsigned form
    /// </summary>
    Unsigned,
}
