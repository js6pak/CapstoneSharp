namespace CapstoneSharp;

public interface IInstruction
{
    public uint Id { get; }
    public ulong Address { get; }
    public ushort Size { get; }
    public ReadOnlySpan<byte> Bytes { get; }
    public string Mnemonic { get; }
    public string Operands { get; }
}
