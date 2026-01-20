using System;

/// <summary>
/// Data object holding ad reward event parameters.
/// </summary>
public class PlaywireSDKAdRewardEventArgs : EventArgs
{
    public string AdUnitId { get; }
    public string Type { get; }
    public int Amount { get; }
    
    internal PlaywireSDKAdRewardEventArgs(string adUnitId, string type, string amount)
    {
        AdUnitId = adUnitId;
        Type = type;

        int amountValue = 0;
        Int32.TryParse(amount, out amountValue);            
        Amount = amountValue;
    }
}