namespace CapstoneSharp.Interop;

internal readonly unsafe struct CapstoneDisassemblerHandle
{
    private readonly void* _handle;

    public CapstoneDisassemblerHandle(void* handle)
    {
        _handle = handle;
    }

    public void EnsureAlive()
    {
        if (_handle == default)
        {
            throw new ObjectDisposedException(nameof(CapstoneDisassemblerHandle), "Disassembler handle is null, are you trying to use it after Dispose?");
        }
    }
}
