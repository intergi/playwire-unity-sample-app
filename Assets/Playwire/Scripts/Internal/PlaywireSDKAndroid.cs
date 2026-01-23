using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlaywireSDKAndroid : PlaywireSDKBase
{
    private static readonly AndroidJavaClass PluginClass = new AndroidJavaClass("com.playwire.unityplugin.PlaywireUnityPlugin");
    internal static string LogTag = "PW_PlaywireSDKAndroid";

    static PlaywireSDKAndroid()
    {
        InitCallback();
    }

    #region Initialization

    public static void InitializeSDK(string publisherId, string appId)
    {
        try {
            AndroidJavaClass TotalPluginClass = new AndroidJavaClass("com.playwire.unityplugin.PlaywireTotalInitializer");
            if (TotalPluginClass != null) {
                TotalPluginClass.CallStatic("initializeSdk", publisherId, appId);
                return;
            }
        } catch(Exception e) {
            Debug.LogError($"[{LogTag}] Android Plugin Class intialization error: {e.Message}"); 
        }

        try {
            AndroidJavaClass CoppaPluginClass = new AndroidJavaClass("com.playwire.unityplugin.PlaywireCOPPAInitializer");
            if (CoppaPluginClass != null) {
                CoppaPluginClass.CallStatic("initializeSdk", publisherId, appId);
                return;
            }
        } catch(Exception e) {
            Debug.LogError($"[{LogTag}] Android Plugin Class intialization error: {e.Message}"); 
        }

        Debug.LogError($"[{LogTag}] Android Plugin Class can't be found."); 
    }
    public static void StartConsoleLogger()
    {
        PluginClass.CallStatic("startConsoleLogger");
    }

    public static bool Test { 
        get {
            return PluginClass.CallStatic<bool>("getTestAds");
        }
        set {
            PluginClass.CallStatic("setTestAds", value);
        }
    }

    public static new PlaywireSDKBase.CMP CMP {

        get {
            try {
                string typeString = PluginClass.CallStatic<string>("getCMP");
    			PlaywireSDKBase.CMP type = (PlaywireSDKBase.CMP)Enum.Parse(typeof(PlaywireSDKBase.CMP), typeString, true);
                return type;
		    } catch (Exception) {
                return PlaywireSDKBase.CMP.GoogleUMP;
            }
        }
        set {
            PluginClass.CallStatic("setCMP", value.ToString());
        }
    }

    #endregion Initialization

    #region Targeting

    public static void SetGlobalTargeting(PlaywireSDKTargeting targeting) 
    {
        string targets = targeting != null ? targeting.ToString() : null;
        PluginClass.CallStatic("setGlobalTargeting", targets);
    }

    #endregion Targeting

    #region Banners

    public static void SetBannerTargeting(string adUnitId, PlaywireSDKTargeting targeting) 
    {
        string targets = targeting != null ? targeting.ToString() : null;
        PluginClass.CallStatic("setBannerTargeting", adUnitId, targets);
    }

    public static void LoadBanner(string adUnitId, AdPosition position, PlaywireSDKTargeting targeting = null)
    {
        string loadTargets = targeting != null ? targeting.ToString() : null;
        PluginClass.CallStatic("loadBanner", adUnitId, position.ToString(), loadTargets);
    }

    public static void ShowBanner(string adUnitId)
    {
        PluginClass.CallStatic("showBanner", adUnitId);
    }

    public static void HideBanner(string adUnitId)
    {
        PluginClass.CallStatic("hideBanner", adUnitId);
    }

    public static void DestroyBanner(string adUnitId)
    {
        PluginClass.CallStatic("destroyBanner", adUnitId);
    }

    public static void RefreshBanner(string adUnitId)
    {
        PluginClass.CallStatic("refreshBanner", adUnitId);
    }

    #endregion Banners

    #region Interstitials

    public static void SetInterstitialTargeting(string adUnitId, PlaywireSDKTargeting targeting) 
    {
        string targets = targeting != null ? targeting.ToString() : null;
        PluginClass.CallStatic("setInterstitialTargeting", adUnitId, targets);
    }

    public static void LoadInterstitial(string adUnitId, PlaywireSDKTargeting targeting = null)
    {
        string loadTargets = targeting != null ? targeting.ToString() : null;
        PluginClass.CallStatic("loadInterstitial", adUnitId, loadTargets);
    }

    public static bool IsInterstitialReady(string adUnitId)
    {
        return PluginClass.CallStatic<bool>("isInterstitialReady", adUnitId);
    }

    public static void ShowInterstitial(string adUnitId)
    {
        PluginClass.CallStatic("showInterstitial", adUnitId);
    }

    #endregion Interstitials

    #region Rewarded

    public static void SetRewardedTargeting(string adUnitId, PlaywireSDKTargeting targeting) 
    {
        string targets = targeting != null ? targeting.ToString() : null;
        PluginClass.CallStatic("setRewardedTargeting", adUnitId, targets);
    }
    public static void LoadRewarded(string adUnitId, PlaywireSDKTargeting targeting = null) 
    {
        string loadTargets = targeting != null ? targeting.ToString() : null;
        PluginClass.CallStatic("loadRewarded", adUnitId, loadTargets);
    }

    public static bool IsRewardedReady(string adUnitId)
    {
        return PluginClass.CallStatic<bool>("isRewardedReady", adUnitId);
    }

    public static void ShowRewarded(string adUnitId)
    {
        PluginClass.CallStatic("showRewarded", adUnitId);
    }

    #endregion Rewarded

    #region AppOpenAd

    public static void SetAppOpenAdTargeting(string adUnitId, PlaywireSDKTargeting targeting) 
    {
        string targets = targeting != null ? targeting.ToString() : null;
        PluginClass.CallStatic("setAppOpenAdTargeting", adUnitId, targets);
    }

    public static void LoadAppOpenAd(string adUnitId, PlaywireSDKTargeting targeting = null)
    {
        string loadTargets = targeting != null ? targeting.ToString() : null;
        PluginClass.CallStatic("loadAppOpenAd", adUnitId, loadTargets);
    }

    public static bool IsAppOpenAdReady(string adUnitId)
    {
        return PluginClass.CallStatic<bool>("isAppOpenAdReady", adUnitId);
    }

    public static void ShowAppOpenAd(string adUnitId)
    {
        PluginClass.CallStatic("showAppOpenAd", adUnitId);
    }

    public static void SetAppOpenAdReloadOnExpiration(string adUnitId, bool isEnabled)
    {
        PluginClass.CallStatic("setAppOpenAdReloadOnExpiration", adUnitId, isEnabled);
    }

    public static bool GetAppOpenAdReloadOnExpiration(string adUnitId)
    {
        return PluginClass.CallStatic<bool>("getAppOpenAdReloadOnExpiration", adUnitId);
    }

    #endregion AppOpenAd

    #region Rewarded Interstitial

    public static void SetRewardedInterstitialTargeting(string adUnitId, PlaywireSDKTargeting targeting) 
    {
        string targets = targeting != null ? targeting.ToString() : null;
        PluginClass.CallStatic("setRewardedInterstitialTargeting", adUnitId, targets);
    }

    public static void LoadRewardedInterstitial(string adUnitId, PlaywireSDKTargeting targeting = null)
    {
        string loadTargets = targeting != null ? targeting.ToString() : null;
        PluginClass.CallStatic("loadRewardedInterstitial", adUnitId, loadTargets);
    }

    public static bool IsRewardedInterstitialReady(string adUnitId)
    {
        return PluginClass.CallStatic<bool>("isRewardedInterstitialReady", adUnitId);
    }

    public static void ShowRewardedInterstitial(string adUnitId)
    {
        PluginClass.CallStatic("showRewardedInterstitial", adUnitId);
    }

    #endregion Rewarded Interstitial
}
