using System;
using UnityEngine;

/// <summary>
/// Data object holding an ad reward data that user earned once finish watching a rewarded ad content.
/// </summary>
[System.Serializable]
public class PlaywireSDKAdReward
{
    /// <summary>
    /// Type of the ad reward. This value may be empty.
    /// </summary>
    public string type;
    /// <summary>
    /// Amount of the ad reward. This value may hold zero value.
    /// </summary>
    public string amount;

    /// <summary>
    /// Decodes a JSON string to an ad reward object.
    /// </summary>
    /// <param name="jsonString">A string with the data received from iOS or Android bridges.</param>
    /// <returns> The ad reward object. </returns>
    public static PlaywireSDKAdReward CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<PlaywireSDKAdReward>(jsonString);
    }
}