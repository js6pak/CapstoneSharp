namespace CapstoneSharp.Interop;

internal readonly unsafe struct CapstoneInstructionHandle
{
    private readonly void* _handle;

    public CapstoneInstructionHandle(void* handle)
    {
        _handle = handle;
    }

    public static implicit operator CapstoneInstructionHandle(void* handle)
    {
        return new CapstoneInstructionHandle(handle);
    }

    public static implicit operator void*(CapstoneInstructionHandle handle)
    {
        return handle._handle;
    }
}
