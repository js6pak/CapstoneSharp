using System.Runtime.InteropServices;

namespace CapstoneSharp.Interop;

internal static unsafe class CapstoneImports
{
    [DllImport("capstone", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_support", ExactSpelling = true)]
    public static extern NativeBoolean support(int query);

    [DllImport("capstone", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_open", ExactSpelling = true)]
    public static extern CapstoneStatus open(CapstoneArch arch, CapstoneMode mode, out CapstoneDisassemblerHandle handle);

    [DllImport("capstone", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_close", ExactSpelling = true)]
    public static extern CapstoneStatus close(ref CapstoneDisassemblerHandle handle);

    /// <summary>
    /// Set option for disassembling engine at runtime
    /// </summary>
    /// <param name="handle">handle returned by cs_open()</param>
    /// <param name="type">type of option to be set</param>
    /// <param name="value">option value corresponding with <paramref name="type"/></param>
    [DllImport("capstone", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_option", ExactSpelling = true)]
    public static extern CapstoneStatus option(CapstoneDisassemblerHandle handle, CapstoneOptionType type, nuint value);

    /// <summary>
    /// Report the last error number when some API function fail.
    /// Like glibc's errno, cs_errno might not retain its old value once accessed.
    /// </summary>
    /// <param name="handle">handle returned by cs_open()</param>
    /// <returns>error code of <see cref="CapstoneStatus"/> enum type</returns>
    [DllImport("capstone", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_errno", ExactSpelling = true)]
    public static extern CapstoneStatus errno(CapstoneDisassemblerHandle handle);

    /// <summary>
    /// Return a string describing given error code.
    /// </summary>
    /// <param name="code">error code</param>
    /// <returns>a pointer to a string that describes the error code passed in the argument <see cref="code"/></returns>
    [DllImport("capstone", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_strerror", ExactSpelling = true)]
    public static extern IntPtr strerror(CapstoneStatus code);

    /// <summary>
    /// Disassemble binary code, given the code buffer, size, address and number
    /// of instructions to be decoded.
    /// This API dynamically allocate memory to contain disassembled instruction.
    /// Resulting instructions will be put into <see cref="insn"/>
    /// </summary>
    /// <param name="handle">handle returned by cs_open()</param>
    /// <param name="code">buffer containing raw binary code to be disassembled.</param>
    /// <param name="codeSize">size of the above code buffer.</param>
    /// <param name="address">address of the first instruction in given raw code buffer.</param>
    /// <param name="count">number of instructions to be disassembled, or 0 to get all of them</param>
    /// <param name="insn">array of instructions filled in by this API.</param>
    /// <returns>the number of successfully disassembled instructions, or 0 if this function failed to disassemble the given code</returns>
    [DllImport("capstone", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_disasm", ExactSpelling = true)]
    public static extern nuint disasm(CapstoneDisassemblerHandle handle, byte* code, nuint codeSize, ulong address, nuint count, out CapstoneInstructionHandle insn);

    /// <summary>
    /// Free memory allocated by cs_malloc() or cs_disasm()
    /// </summary>
    /// <param name="insn">pointer returned by @insn argument in cs_disasm() or cs_malloc()</param>
    /// <param name="count">number of CapstoneInsturction structures returned by cs_disasm(), or 1 to free memory allocated by cs_malloc().</param>
    [DllImport("capstone", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_free", ExactSpelling = true)]
    public static extern void free(CapstoneInstructionHandle insn, nuint count);

    /// <summary>
    /// Allocate memory for 1 instruction to be used by cs_disasm_iter().
    /// </summary>
    /// <param name="handle">handle returned by cs_open()</param>
    /// <remarks>when no longer in use, you can reclaim the memory allocated for this instruction with cs_free(insn, 1)</remarks>
    [DllImport("capstone", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_malloc", ExactSpelling = true)]
    public static extern CapstoneInstructionHandle malloc(CapstoneDisassemblerHandle handle);

    /// <summary>
    /// Fast API to disassemble binary code, given the code buffer, size, address
    /// and number of instructions to be decoded.
    /// This API puts the resulting instruction into a given cache in <see cref="insn"/>.
    /// </summary>
    /// <param name="handle">handle returned by cs_open()</param>
    /// <param name="code">buffer containing raw binary code to be disassembled</param>
    /// <param name="size">size of above code</param>
    /// <param name="address">address of the first insn in given raw code buffer</param>
    /// <param name="insn">pointer to instruction to be filled in by this API.</param>
    /// <returns>true if this API successfully decode 1 instruction, or false otherwise.</returns>
    [DllImport("capstone", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_disasm_iter", ExactSpelling = true)]
    public static extern NativeBoolean disasm_iter(CapstoneDisassemblerHandle handle, byte** code, nuint* size, ulong* address, CapstoneInstructionHandle insn);

    [DllImport("capstone", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_reg_name", ExactSpelling = true)]
    public static extern byte* reg_name(CapstoneDisassemblerHandle handle, uint regId);

    [DllImport("capstone", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_insn_name", ExactSpelling = true)]
    public static extern byte* insn_name(CapstoneDisassemblerHandle handle, uint insnId);

    [DllImport("capstone", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_group_name", ExactSpelling = true)]
    public static extern byte* group_name(CapstoneDisassemblerHandle handle, uint groupId);

    [DllImport("capstone", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cs_regs_access", ExactSpelling = true)]
    public static extern CapstoneStatus regs_access(CapstoneDisassemblerHandle handle, CapstoneInstructionHandle insn, ushort* regsRead, byte* regsReadCount, ushort* regsWrite, byte* regsWriteCount);
}
