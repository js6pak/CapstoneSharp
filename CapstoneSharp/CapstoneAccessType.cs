namespace CapstoneSharp;

[Flags]
public enum CapstoneAccessType : byte
{
    Invalid = 0,
    Read = 1 << 0,
    Write = 1 << 1,
}
