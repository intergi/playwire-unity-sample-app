#if UNITY_IOS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlaywireSDKiOS : PlaywireSDKBase
{
    static PlaywireSDKiOS()
    {
        InitCallback();
    }

    #region Initialization

    public static void InitializeSDK(string publisherId, string appId)
    {
        _PlaywireInitializeSDK(publisherId, appId);
    }

    public static void StartConsoleLogger()
    {
        _PlaywireStartConsoleLogger();
    }

    public static bool Test { 
        get {
            return _PlaywireGetTestAds();
        }
        set {
            _PlaywireSetTestAds(value);
        }
    }

    public static new PlaywireSDKBase.CMP CMP { 
        get {
            try {
                string typeString = _PlaywireGetCMP();
    			PlaywireSDKBase.CMP type = (PlaywireSDKBase.CMP)Enum.Parse(typeof(PlaywireSDKBase.CMP), typeString, true);
                return type;
		    } catch (Exception) {
                return PlaywireSDKBase.CMP.GoogleUMP;
            }
        }
        set {
            _PlaywireSetCMP(value.ToString());
        }
    }

    #endregion Initialization

    #region Targeting

    public static void SetGlobalTargeting(PlaywireSDKTargeting targeting) 
    {
        string targets = targeting != null ? targeting.ToString() : null;
        _PlaywireSetGlobalTargeting(targets);
    }

    #endregion Targeting

    #region Banners

    public static void SetBannerTargeting(string adUnitId, PlaywireSDKTargeting targeting) 
    {
        string targets = targeting != null ? targeting.ToString() : null;
        _PlaywireSetBannerTargeting(adUnitId, targets);
    }

    public static void LoadBanner(string adUnitId, AdPosition position, PlaywireSDKTargeting targeting = null)
    {
        string loadTargets = targeting != null ? targeting.ToString() : null;
        _PlaywireLoadBanner(adUnitId, position.ToString(), loadTargets);
    }

    public static void ShowBanner(string adUnitId)
    {
        _PlaywireShowBanner(adUnitId);
    }

    public static void HideBanner(string adUnitId)
    {
        _PlaywireHideBanner(adUnitId);
    }

    public static void DestroyBanner(string adUnitId)
    {
        _PlaywireDestroyBanner(adUnitId);
    }

    public static void RefreshBanner(string adUnitId)
    {
        _PlaywireRefreshBanner(adUnitId);
    }

    #endregion Banners

    #region Interstitials

    public static void SetInterstitialTargeting(string adUnitId, PlaywireSDKTargeting targeting) 
    {
        string targets = targeting != null ? targeting.ToString() : null;
        _PlaywireSetInterstitialTargeting(adUnitId, targets);
    }

    public static void LoadInterstitial(string adUnitId, PlaywireSDKTargeting targeting = null)
    {
        string loadTargets = targeting != null ? targeting.ToString() : null;
        _PlaywireLoadInterstitial(adUnitId, loadTargets);
    }

    public static bool IsInterstitialReady(string adUnitId)
    {
        return _PlaywireIsInterstitialReady(adUnitId);
    }

    public static void ShowInterstitial(string adUnitId)
    {
        _PlaywireShowInterstitial(adUnitId);
    }

    #endregion Interstitials

    #region Rewarded

    public static void SetRewardedTargeting(string adUnitId, PlaywireSDKTargeting targeting) 
    {
        string targets = targeting != null ? targeting.ToString() : null;
        _PlaywireSetRewardedTargeting(adUnitId, targets);
    }

    public static void LoadRewarded(string adUnitId, PlaywireSDKTargeting targeting = null)
    {
        string loadTargets = targeting != null ? targeting.ToString() : null;
        _PlaywireLoadRewarded(adUnitId, loadTargets);
    }

    public static bool IsRewardedReady(string adUnitId)
    {
        return _PlaywireIsRewardedReady(adUnitId);
    }

    public static void ShowRewarded(string adUnitId)
    {
        _PlaywireShowRewarded(adUnitId);
    }

    #endregion Rewarded

    #region AppOpenAd

    public static void SetAppOpenAdTargeting(string adUnitId, PlaywireSDKTargeting targeting) 
    {
        string targets = targeting != null ? targeting.ToString() : null;
        _PlaywireSetAppOpenAdTargeting(adUnitId, targets);
    }

    public static void LoadAppOpenAd(string adUnitId, PlaywireSDKTargeting targeting = null)
    {
        string loadTargets = targeting != null ? targeting.ToString() : null;
        _PlaywireLoadAppOpenAd(adUnitId, loadTargets);
    }

    public static bool IsAppOpenAdReady(string adUnitId)
    {
        return _PlaywireIsAppOpenAdReady(adUnitId);
    }

    public static void ShowAppOpenAd(string adUnitId)
    {
        _PlaywireShowAppOpenAd(adUnitId);
    }

    public static void SetAppOpenAdReloadOnExpiration(string adUnitId, bool isEnabled)
    {
        _PlaywireSetAppOpenAdReloadOnExpiration(adUnitId, isEnabled);
    }

    public static bool GetAppOpenAdReloadOnExpiration(string adUnitId)
    {
        return _PlaywireGetAppOpenAdReloadOnExpiration(adUnitId);
    }

    #endregion AppOpenAd

    #region Rewarded Interstitial

    public static void SetRewardedInterstitialTargeting(string adUnitId, PlaywireSDKTargeting targeting) 
    {
        string targets = targeting != null ? targeting.ToString() : null;
        _PlaywireSetRewardedInterstitialTargeting(adUnitId, targets);
    }

    public static void LoadRewardedInterstitial(string adUnitId, PlaywireSDKTargeting targeting = null)
    {
        string loadTargets = targeting != null ? targeting.ToString() : null;
        _PlaywireLoadRewardedInterstitial(adUnitId, loadTargets);
    }

    public static bool IsRewardedInterstitialReady(string adUnitId)
    {
        return _PlaywireIsRewardedInterstitialReady(adUnitId);
    }

    public static void ShowRewardedInterstitial(string adUnitId)
    {
        _PlaywireShowRewardedInterstitial(adUnitId);
    }

    #endregion Rewarded Interstitial

    #region DllImports
    #if ENABLE_IL2CPP && UNITY_ANDROID
        private static void _PlaywireInitializeSDK(string publisherId, string appId) {}

        private static void _PlaywireStartConsoleLogger() {}

        private static void _PlaywireSetGlobalTargeting(string targeting) {}

        private static void _PlaywireSetBannerTargeting(string adUnitId, string targeting) {}

        private static void _PlaywireLoadBanner(string adUnitId, string position, string targeting) {}

        private static void _PlaywireShowBanner(string adUnitId) {}

        private static void _PlaywireHideBanner(string adUnitId) {}

        private static void _PlaywireRefreshBanner(string adUnitId) {}        

        private static void _PlaywireSetInterstitialTargeting(string adUnitId, string targeting) {}

        private static void _PlaywireLoadInterstitial(string adUnitId, string targeting) {}

        private static bool _PlaywireIsInterstitialReady(string adUnitId) { 
            return false;
        }

        private static void _PlaywireShowInterstitial(string adUnitId) {}

        private static void _PlaywireSetRewardedTargeting(string adUnitId, string targeting) {}

        private static void _PlaywireLoadRewarded(string adUnitId, string targeting) {}
        
        private static bool _PlaywireIsRewardedReady(string adUnitId) {
            return false;
        }

        private static void _PlaywireShowRewarded(string adUnitId) {}

        private static void _PlaywireSetAppOpenAdTargeting(string adUnitId, string targeting) {}

        private static void _PlaywireLoadAppOpenAd(string adUnitId, string targeting) {}

        private static bool _PlaywireIsAppOpenAdReady(string adUnitId) { 
            return false;
        }

        private static void _PlaywireShowAppOpenAd(string adUnitId) {}

        private static void _PlaywireSetAppOpenAdReloadOnExpiration(string adUnitId, bool isEnabled) {}

        private static bool _PlaywireGetAppOpenAdReloadOnExpiration(string adUnitId) {
            return false;
        }

        private static void _PlaywireSetRewardedInterstitialTargeting(string adUnitId, string targeting) {}

        private static void _PlaywireLoadRewardedInterstitial(string adUnitId, string targeting) {}

        private static void _PlaywireShowRewardedInterstitial(string adUnitId) {}

        private static bool _PlaywireIsRewardedInterstitialReady(string adUnitId) {
            return false;
        }

        private static void _PlaywireSetTestAds(bool isEnabled) {}

        private static bool _PlaywireGetTestAds() {
            return false;
        }

        private static void _PlaywireSetCMP(string type) {}

        private static string _PlaywireGetCMP() {
            return "GoogleUMP";
        }
    #else
        [DllImport("__Internal")]
        private static extern void _PlaywireInitializeSDK(string publisherId, string appId);

        [DllImport("__Internal")]
        private static extern void _PlaywireStartConsoleLogger();

        [DllImport("__Internal")]
        private static extern void _PlaywireSetGlobalTargeting(string targeting);

        [DllImport("__Internal")]
        private static extern void _PlaywireSetBannerTargeting(string adUnitId, string targeting);

        [DllImport("__Internal")]
        private static extern void _PlaywireLoadBanner(string adUnitId, string position, string targeting);

        [DllImport("__Internal")]
        private static extern void _PlaywireShowBanner(string adUnitId);

        [DllImport("__Internal")]
        private static extern void _PlaywireHideBanner(string adUnitId);

        [DllImport("__Internal")]
        private static extern void _PlaywireDestroyBanner(string adUnitId);

        [DllImport("__Internal")]
        private static extern void _PlaywireRefreshBanner(string adUnitId);

        [DllImport("__Internal")]
        private static extern void _PlaywireSetInterstitialTargeting(string adUnitId, string targeting);

        [DllImport("__Internal")]
        private static extern void _PlaywireLoadInterstitial(string adUnitId, string targeting);

        [DllImport("__Internal")]
        private static extern bool _PlaywireIsInterstitialReady(string adUnitId);

        [DllImport("__Internal")]
        private static extern void _PlaywireShowInterstitial(string adUnitId);

        [DllImport("__Internal")]
        private static extern void _PlaywireSetRewardedTargeting(string adUnitId, string targeting);

        [DllImport("__Internal")]
        private static extern void _PlaywireLoadRewarded(string adUnitId, string targeting);

        [DllImport("__Internal")]
        private static extern bool _PlaywireIsRewardedReady(string adUnitId);

        [DllImport("__Internal")]
        private static extern void _PlaywireShowRewarded(string adUnitId);

        [DllImport("__Internal")]
        private static extern void _PlaywireSetAppOpenAdTargeting(string adUnitId, string targeting);

        [DllImport("__Internal")]
        private static extern void _PlaywireLoadAppOpenAd(string adUnitId, string targeting);

        [DllImport("__Internal")]
        private static extern bool _PlaywireIsAppOpenAdReady(string adUnitId);

        [DllImport("__Internal")]
        private static extern void _PlaywireShowAppOpenAd(string adUnitId);

        [DllImport("__Internal")]
        private static extern void _PlaywireSetAppOpenAdReloadOnExpiration(string adUnitId, bool isEnabled);

        [DllImport("__Internal")]
        private static extern bool _PlaywireGetAppOpenAdReloadOnExpiration(string adUnitId);

        [DllImport("__Internal")]
        private static extern void _PlaywireSetRewardedInterstitialTargeting(string adUnitId, string targeting);

        [DllImport("__Internal")]
        private static extern void _PlaywireLoadRewardedInterstitial(string adUnitId, string targeting);

        [DllImport("__Internal")]
        private static extern void _PlaywireShowRewardedInterstitial(string adUnitId);

        [DllImport("__Internal")]
        private static extern bool _PlaywireIsRewardedInterstitialReady(string adUnitId);

        [DllImport("__Internal")]
        private static extern void _PlaywireSetTestAds(bool isEnabled);

        [DllImport("__Internal")]
        private static extern bool _PlaywireGetTestAds();

        [DllImport("__Internal")]
        private static extern void _PlaywireSetCMP(string type);

        [DllImport("__Internal")]
        private static extern string _PlaywireGetCMP();
    #endif
    #endregion DllImports
}
#endif