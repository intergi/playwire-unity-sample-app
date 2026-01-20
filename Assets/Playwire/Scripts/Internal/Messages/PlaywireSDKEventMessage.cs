using System;
using UnityEngine;

/// <summary>
/// Data object holding any event message that iOS and Android bridges send to the plugin.
/// </summary>
[System.Serializable]
public class PlaywireSDKEventMessage
{
    /// <summary>
    /// Mandatory field. See full list of available options <see cref="PlaywireSDKConstant.Event">.
    /// </summary>
    public string name;
    /// <summary>
    /// Optional field. It's a non-null only for ad-related events.
    /// </summary>
    public string adUnitId;
    /// <summary>
    /// Optional field. It's a JSON string.
    /// </summary>
    public string parameters;

    /// <summary>
    /// Decodes a JSON string to event message object.
    /// </summary>
    /// <param name="jsonString">A string with the data received from iOS or Android bridges.</param>
    /// <returns> The event message object. </returns>
    public static PlaywireSDKEventMessage CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<PlaywireSDKEventMessage>(jsonString);
    }
}