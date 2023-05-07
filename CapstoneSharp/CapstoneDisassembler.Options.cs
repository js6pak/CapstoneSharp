using CapstoneSharp.Interop;

namespace CapstoneSharp;

public abstract partial class CapstoneDisassembler<TId, TInstruction, TUnsafeInstruction>
{
    private protected void SetOption(CapstoneOptionType type, CapstoneOptionValue value)
    {
        _handle.EnsureAlive();
        CapstoneException.ThrowIfUnsuccessful(CapstoneImports.option(_handle, type, (nuint)value));
    }

    private protected void SetOption(CapstoneOptionType type, bool value)
    {
        SetOption(type, value ? CapstoneOptionValue.On : CapstoneOptionValue.Off);
    }

    private bool _enableInstructionDetails;

    /// <summary>
    /// Gets or sets a value indicating whether instruction details are enabled.
    /// </summary>
    public bool EnableInstructionDetails
    {
        get => _enableInstructionDetails;
        set => SetOption(CapstoneOptionType.Detail, _enableInstructionDetails = value);
    }

    private bool _enableSkipData;

    /// <summary>
    /// Gets or sets a value indicating whether skip data mode is enabled.
    /// </summary>
    public bool EnableSkipData
    {
        get => _enableSkipData;
        set => SetOption(CapstoneOptionType.SkipData, _enableSkipData = value);
    }
}
