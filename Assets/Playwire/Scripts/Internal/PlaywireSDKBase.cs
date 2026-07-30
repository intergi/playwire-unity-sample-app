using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class PlaywireSDKBase
{
    public enum LogLevel
    {
        None = 0,
        Error = 1,
        Warning = 2,
        Info = 3
    }

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