using System;

/// <summary>
/// Data object holding ad-related event parameters.
/// </summary>
public class PlaywireSDKEventArgs : EventArgs
{
    public string AdUnitId { get; }
    public PlaywireSDKEventArgs(string adUnitId)
    {
        AdUnitId = adUnitId;
    }
}