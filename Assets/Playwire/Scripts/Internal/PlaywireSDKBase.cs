using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class PlaywireSDKBase
{
    public enum AdPosition
    {
        TopLeft,
        TopCenter,
        TopRight,
        CenterLeft,
        Center,
        CenterRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    public enum CMP
    {
        GoogleUMP,
        AlreadyLaunched,
        None
    }

    protected static void InitCallback()
    {
        var callback = new GameObject("PlaywireSDKCallback", typeof(PlaywireSDKCallback));
        var callbackComponent = callback.GetComponent<PlaywireSDKCallback>();
    }
}