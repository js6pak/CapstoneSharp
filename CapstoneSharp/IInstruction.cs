namespace CapstoneSharp;

public interface IInstruction<TSelf> where TSelf : unmanaged, IInstruction<TSelf>
{
    public uint Id { get; }
    public ulong Address { get; }
    public ushort Size { get; }
    public ReadOnlySpan<byte> Bytes { get; }
    public string Mnemonic { get; }
    public string Operands { get; }

#if NET6_0_OR_GREATER
#pragma warning disable CA2252
    internal static abstract unsafe TSelf* Alloc(bool allocateHandle);
    internal static abstract unsafe void Free(TSelf* instruction, nuint count);
#pragma warning restore CA2252
#endif
}
