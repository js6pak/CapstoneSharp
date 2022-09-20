namespace CapstoneSharp.Interop;

/// <summary>
/// The exception that is thrown for a capstone error code.
/// </summary>
internal class CapstoneException : Exception
{
    public CapstoneException(CapstoneStatus code) : base(code.GetMessage())
    {
        Code = code;
    }

    public CapstoneStatus Code { get; }

    public static void ThrowIfUnsuccessful(CapstoneStatus code)
    {
        if (code != CapstoneStatus.Ok)
        {
            throw new CapstoneException(code);
        }
    }
}
