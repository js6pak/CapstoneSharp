using System.Runtime.InteropServices;

namespace CapstoneSharp.Interop;

/// <summary>
/// All type of errors encountered by Capstone API.
/// These are values returned by cs_errno()
/// </summary>
internal enum CapstoneStatus : uint
{
    /// <summary>
    /// No error: everything was fine
    /// </summary>
    Ok = 0,

    /// <summary>
    /// Out-Of-Memory error: cs_open(), cs_disasm(), cs_disasm_iter()
    /// </summary>
    OutOfMemory,

    /// <summary>
    /// Unsupported architecture: cs_open()
    /// </summary>
    UnsupportedArchitecture,

    /// <summary>
    /// Invalid handle: cs_op_count(), cs_op_index()
    /// </summary>
    InvalidHandle,

    /// <summary>
    /// Invalid csh argument: cs_close(), cs_errno(), cs_option()
    /// </summary>
    InvalidCsh,

    /// <summary>
    /// Invalid/unsupported mode: cs_open()
    /// </summary>
    InvalidMode,

    /// <summary>
    /// Invalid/unsupported option: cs_option()
    /// </summary>
    InvalidOption,

    /// <summary>
    /// Information is unavailable because detail option is OFF
    /// </summary>
    UnsupportedDetail,

    /// <summary>
    /// Dynamic memory management uninitialized (see CS_OPT_MEM)
    /// </summary>
    MemSetup,

    /// <summary>
    /// Unsupported version (bindings)
    /// </summary>
    UnsupportedVersion,

    /// <summary>
    /// Access irrelevant data in "diet" engine
    /// </summary>
    Diet,

    /// <summary>
    /// Access irrelevant data for "data" instruction in SKIPDATA mode
    /// </summary>
    SkipData,

    /// <summary>
    /// X86 AT&amp;T syntax is unsupported (opt-out at compile time)
    /// </summary>
    UnsupportedSyntaxAtt,

    /// <summary>
    /// X86 Intel syntax is unsupported (opt-out at compile time)
    /// </summary>
    UnsupportedSyntaxIntel,

    /// <summary>
    /// X86 Masm syntax is unsupported (opt-out at compile time)
    /// </summary>
    UnsupportedSyntaxMasm,
}

internal static class CapstoneStatusExtensions
{
    public static string GetMessage(this CapstoneStatus status)
    {
        return Marshal.PtrToStringAnsi(CapstoneImports.strerror(status)) ?? throw new InvalidOperationException("No message found for status " + status);
    }
}
