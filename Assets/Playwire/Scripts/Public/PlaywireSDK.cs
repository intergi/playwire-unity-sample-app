
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;

/// <summary>
/// The Playwire Unity API for publishers.
/// <para>
/// Publishers integrating with Playwire should make all calls through this class, and handle any desired events from the <see cref="PlaywireSDKCallback"/> class.
/// </para>
/// </summary>
public class PlaywireSDK :
#if UNITY_ANDROID
    PlaywireSDKAndroid
#elif UNITY_IPHONE || UNITY_IOS
    PlaywireSDKiOS
#else
    PlaywireSDKUnityEditor
#endif
{
    private const string sdkVersion = "12.1.1";

    /// <summary>
    /// The version of the Playwire Unity SDK.
    /// </summary>
    public static string Version
    {
        get { return sdkVersion; }
    }
}
