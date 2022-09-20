namespace CapstoneSharp.Interop;

/// <summary>
/// Runtime option value (associated with <see cref="CapstoneOptionType"/>)
/// </summary>
internal enum CapstoneOptionValue : uint
{
    /// <summary>
    /// Turn OFF an option - default for CS_OPT_DETAIL, CS_OPT_SKIPDATA, CS_OPT_UNSIGNED.
    /// </summary>
    Off = 0,

    /// <summary>
    /// Turn ON an option (CS_OPT_DETAIL, CS_OPT_SKIPDATA).
    /// </summary>
    On = 3,

    /// <summary>
    /// Default asm syntax (CS_OPT_SYNTAX).
    /// </summary>
    SyntaxDefault = 0,

    /// <summary>
    /// X86 Intel asm syntax - default on X86 (CS_OPT_SYNTAX).
    /// </summary>
    SyntaxIntel,

    /// <summary>
    /// X86 ATT asm syntax (CS_OPT_SYNTAX).
    /// </summary>
    SyntaxAtt,

    /// <summary>
    /// Prints register name with only number (CS_OPT_SYNTAX)
    /// </summary>
    SyntaxNoregname,

    /// <summary>
    /// X86 Intel Masm syntax (CS_OPT_SYNTAX).
    /// </summary>
    SyntaxMasm,
}
