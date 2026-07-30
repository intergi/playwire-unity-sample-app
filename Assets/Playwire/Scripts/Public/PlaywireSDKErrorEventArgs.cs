using System;

public class PlaywireSDKErrorEventArgs : PlaywireSDKEventArgs
{
    public PlaywireAdError Error { get; }

    internal PlaywireSDKErrorEventArgs(string adUnitId, PlaywireAdError error) : base(adUnitId)
    {
        Error = error ?? throw new ArgumentNullException(nameof(error));
    }
}