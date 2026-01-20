#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlaywireSDKUnityEditor : PlaywireSDKBase
{
    private const string LogTag = "PW_PlaywireSDKUnityEditor";

    static PlaywireSDKUnityEditor()
    {
        Debug.Log($"[{LogTag}:PlaywireSDKUnityEditor] UnityEditor is not supported now.");
    }

    #region Initialization

    /// <summary>
    /// Initializes the Playwire Unity SDK. Call this method before loading and showing any ads requests.
    /// <param name="publisherId">A string value with the publisher identifier.</param>
    /// <param name="appId">A string value with an application identifier.</param>
    /// <para>
    /// See <see cref="PlaywireSDKCallback.OnSDKInitializedEvent"/> for the resulting triggered event.
    /// </para>
    /// </summary>
    public static void InitializeSDK(string publisherId, string appId)
    {
        Debug.Log($"[{LogTag}: InitializeSDK] UnityEditor is not supported now.");
    }

    /// <summary>
    /// Start writting logs to console.
    /// </summary>
    public static void StartConsoleLogger()
    {
        Debug.Log($"[{LogTag}:StartConsoleLogger] UnityEditor is not supported now.");
    }

    /// <summary>
    /// Configures CMP inside the Playwire Unity SDK. Set this value before initialization.
    /// </para>
    /// See <see cref="PlaywireSDKBase.CMP"/> for the available options.
    /// </para>
    /// </summary>
    public static PlaywireSDKBase.CMP CMP {
        get {
            Debug.Log($"[{LogTag}:CMP] UnityEditor is not supported now.");
            return PlaywireSDKBase.CMP.GoogleUMP;
        }
        set {
            Debug.Log($"[{LogTag}:CMP] UnityEditor is not supported now.");
        }
    }

    #endregion Initialization

    #region Targeting

    /// <summary>
    /// Set global targeting to be used for every ad request.
    /// <param name="targeting">A targeting value with any tags.</param>
    /// </summary>
    public static void SetGlobalTargeting(PlaywireSDKTargeting targeting) 
    {
        Debug.Log($"[{LogTag}:SetGlobalTargeting] UnityEditor is not supported now.");
    }

    #endregion Targeting

    #region Banners

    /// <summary>
    /// Set banner's targeting to be used for every ad request.
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    /// <param name="targeting">A targeting value with any tags.</param>
    public static void SetBannerTargeting(string adUnitId, PlaywireSDKTargeting targeting) 
    {
        Debug.Log($"[{LogTag}:SetBannerTargeting] UnityEditor is not supported now.");
    }

    /// <summary>
    /// Fetches a banner ad content.
    /// <para>
    /// Subscribe for <see cref="PlaywireSDKCallback.Banner.OnLoadedEvent"/> to handle a successful response, otherwise see <see cref="PlaywireSDKCallback.Banner.OnFailedToLoadEvent"/>.
    /// </para>
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    /// <param name="position">A position of the loaded banner on the screen. See <see cref="PlaywireSDKBase.AdPosition"/>.</param>
    public static void LoadBanner(string adUnitId, AdPosition position, PlaywireSDKTargeting targeting = null)
    {
        Debug.Log($"[{LogTag}:LoadBanner] UnityEditor is not supported now.");
    }

    /// <summary>
    /// Displays an already-loaded banner ad.
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    public static void ShowBanner(string adUnitId)
    {
        Debug.Log($"[{LogTag}:ShowBanner] UnityEditor is not supported now.");
    }

    /// <summary>
    /// Hides a banner ad.
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    public static void HideBanner(string adUnitId)
    {
        Debug.Log($"[{LogTag}:HideBanner] UnityEditor is not supported now.");
    }

    /// <summary>
    /// Destroys a banner ad when it must be deallocated.
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    public static void DestroyBanner(string adUnitId)
    {
        Debug.Log($"[{LogTag}:DestroyBanner] UnityEditor is not supported now.");
    }


    /// <summary>
    /// Refreshes a banner ad content.
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    public static void RefreshBanner(string adUnitId)
    {
        Debug.Log($"[{LogTag}:RefreshBanner] UnityEditor is not supported now.");
    }

    #endregion Banners

    #region Interstitials

    /// <summary>
    /// Set interstitial's targeting to be used for every ad request.
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    /// <param name="targeting">A targeting value with any tags.</param>
    public static void SetInterstitialTargeting(string adUnitId, PlaywireSDKTargeting targeting) 
    {
        Debug.Log($"[{LogTag}:SetInterstitialTargeting] UnityEditor is not supported now.");
    }

    /// <summary>
    /// Fetches an interstitial ad content.
    /// <para>
    /// Subscribe for <see cref="PlaywireSDKCallback.Interstitial.OnLoadedEvent"/> to handle a successful response, otherwise see <see cref="PlaywireSDKCallback.Interstitial.OnFailedToLoadEvent"/>.
    /// </para>
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    public static void LoadInterstitial(string adUnitId, PlaywireSDKTargeting targeting = null)
    {
        Debug.Log($"[{LogTag}:LoadInterstitial] UnityEditor is not supported now.");
    }

    /// <summary>
    /// Whether the interstitial ad is ready to be presented or not.
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    /// <returns> If an interstitial ad has been loaded and hasn't been presented yet, this method returns <c>true</c>, otherwise - <c>false</c>. </returns>
    public static bool IsInterstitialReady(string adUnitId)
    {
        Debug.Log($"[{LogTag}:IsInterstitialReady] UnityEditor is not supported now.");
        return false;
    }

    /// Displays an already-loaded interstitial ad.
    /// <para>
    /// Subscribe for <see cref="PlaywireSDKCallback.Interstitial.OnOpenedEvent"/> or <see cref="PlaywireSDKCallback.Interstitial.OnFailedToOpenEvent"/> to observe presentation result.
    /// </para>
    /// <para>
    /// Subscribe for <see cref="PlaywireSDKCallback.Interstitial.OnClosedEvent"/> to observe dismissal result.
    /// </para>
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    public static void ShowInterstitial(string adUnitId)
    {
        Debug.Log($"[{LogTag}:ShowInterstitial] UnityEditor is not supported now.");
    }

    #endregion Interstitials

    #region Rewarded

    /// <summary>
    /// Set rewarded's targeting to be used for every ad request.
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    /// <param name="targeting">A targeting value with any tags.</param>
    public static void SetRewardedTargeting(string adUnitId, PlaywireSDKTargeting targeting) 
    {
        Debug.Log($"[{LogTag}:SetRewardedTargeting] UnityEditor is not supported now.");
    }

    /// <summary>
    /// Fetches a rewarded ad content.
    /// <para>
    /// Subscribe for <see cref="PlaywireSDKCallback.Rewarded.OnLoadedEvent"/> to handle a successful response, otherwise see <see cref="PlaywireSDKCallback.Rewarded.OnFailedToLoadEvent"/>.
    /// </para>
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    public static void LoadRewarded(string adUnitId, PlaywireSDKTargeting targeting = null)
    {
        Debug.Log($"[{LogTag}:LoadRewarded] UnityEditor is not supported now.");
    }

    /// <summary>
    /// Whether a rewarded ad is ready to be presented or not.
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    /// <returns> If a rewarded ad has been loaded and hasn't been presented yet, this method returns <c>true</c>, otherwise - <c>false</c>. </returns>
    public static bool IsRewardedReady(string adUnitId)
    {
        Debug.Log($"[{LogTag}:IsRewardedReady] UnityEditor is not supported now.");
        return false;
    }

    /// <summary>
    /// Displays an already-loaded rewarded ad.
    /// <para>
    /// Subscribe for <see cref="PlaywireSDKCallback.Rewarded.OnOpenedEvent"/> or <see cref="PlaywireSDKCallback.Rewarded.OnFailedToOpenEvent"/> to observe a presentation result.
    /// </para>
    /// <para>
    /// Subscribe for <see cref="PlaywireSDKCallback.Rewarded.OnClosedEvent"/> to observe a dismissal result.
    /// </para>
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    public static void ShowRewarded(string adUnitId)
    {
        Debug.Log($"[{LogTag}:ShowRewarded] UnityEditor is not supported now.");
    }

    #endregion Rewarded

    #region AppOpenAd

    /// <summary>
    /// Set app open ad's targeting to be used for every ad request.
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    /// <param name="targeting">A targeting value with any tags.</param>
    public static void SetAppOpenAdTargeting(string adUnitId, PlaywireSDKTargeting targeting) 
    {
        Debug.Log($"[{LogTag}:SetAppOpenAdTargeting] UnityEditor is not supported now.");
    }

    /// <summary>
    /// Fetches an app open ad content.
    /// <para>
    /// Subscribe for <see cref="PlaywireSDKCallback.AppOpenAd.OnLoadedEvent"/> to handle a successful response, otherwise see <see cref="PlaywireSDKCallback.AppOpenAd.OnFailedToLoadEvent"/>.
    /// </para>
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    public static void LoadAppOpenAd(string adUnitId, PlaywireSDKTargeting targeting = null)
    {
        Debug.Log($"[{LogTag}:LoadAppOpenAd] UnityEditor is not supported now.");
    }

    /// <summary>
    /// Whether an app open ad is ready to be presented or not.
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    /// <returns> If an app open ad has been loaded and hasn't been presented yet, this method returns <c>true</c>, otherwise - <c>false</c>. </returns>
    public static bool IsAppOpenAdReady(string adUnitId)
    {
        Debug.Log($"[{LogTag}:IsAppOpenAdReady] UnityEditor is not supported now.");
        return false;
    }

    /// <summary>
    /// Displays an already-loaded app open ad.
    /// <para>
    /// Subscribe for <see cref="PlaywireSDKCallback.AppOpenAd.OnOpenedEvent"/> or <see cref="PlaywireSDKCallback.AppOpenAd.OnFailedToOpenEvent"/> to observe a presentation result.
    /// </para>
    /// <para>
    /// Subscribe for <see cref="PlaywireSDKCallback.AppOpenAd.OnClosedEvent"/> to observe a dismissal result.
    /// </para>
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    public static void ShowAppOpenAd(string adUnitId)
    {
        Debug.Log($"[{LogTag}:ShowAppOpenAd] UnityEditor is not supported now.");
    }

    /// <summary>
    /// Sets an app open ad content auto reload on expiration. If you present the expired ad content, an ad impression won't be recorded.
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    /// <param name="isEnabled">A boolean value to define if auto reload is enabled or not.</param>
    public static void SetAppOpenAdReloadOnExpiration(string adUnitId, bool isEnabled)
    {
        Debug.Log($"[{LogTag}:SetAppOpenAdReloadOnExpiration] UnityEditor is not supported now.");
    }

    /// <summary>
    /// Whether an app open ad content reload is enabled or not.
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    /// <returns> If an app open ad content auto reload is enabled, this method returns <c>true</c>, otherwise - <c>false</c>. </returns>
    public static bool GetAppOpenAdReloadOnExpiration(string adUnitId)
    {
        Debug.Log($"[{LogTag}:GetAppOpenAdReloadOnExpiration] UnityEditor is not supported now.");
        return false;
    }

    #endregion AppOpenAd

    #region Rewarded Interstitial

    /// <summary>
    /// Set rewarded interstitial's targeting to be used for every ad request.
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    /// <param name="targeting">A targeting value with any tags.</param>
    public static void SetRewardedInterstitialTargeting(string adUnitId, PlaywireSDKTargeting targeting) 
    {
        Debug.Log($"[{LogTag}:SetRewardedInterstitialTargeting] UnityEditor is not supported now.");
    }

    /// <summary>
    /// Fetches a rewarded interstitial ad content.
    /// <para>
    /// Subscribe for <see cref="PlaywireSDKCallback.RewardedInterstitial.OnLoadedEvent"/> to handle a successful response, otherwise see <see cref="PlaywireSDKCallback.RewardedInterstitial.OnFailedToLoadEvent"/>.
    /// </para>
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    public static void LoadRewardedInterstitial(string adUnitId, PlaywireSDKTargeting targeting = null)
    {
        Debug.Log($"[{LogTag}:LoadRewardedInterstitial] UnityEditor is not supported now.");
    }

    /// <summary>
    /// Whether a rewarded interstitial ad is ready to be presented or not.
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    /// <returns> If a rewarded interstitial ad has been loaded and hasn't been presented yet, this method returns <c>true</c>, otherwise - <c>false</c>. </returns>
    public static bool IsRewardedInterstitialReady(string adUnitId)
    {
        return false;
    }

    /// <summary>
    /// Displays an already-loaded rewarded interstitial ad.
    /// <para>
    /// Subscribe for <see cref="PlaywireSDKCallback.RewardedInterstitial.OnOpenedEvent"/> or <see cref="PlaywireSDKCallback.RewardedInterstitial.OnFailedToOpenEvent"/> to observe a presentation result.
    /// </para>
    /// <para>
    /// Subscribe for <see cref="PlaywireSDKCallback.RewardedInterstitial.OnClosedEvent"/> to observe a dismissal result.
    /// </para>
    /// </summary>
    /// <param name="adUnitId">A string with the ad unit id.</param>
    public static void ShowRewardedInterstitial(string adUnitId)
    {
        Debug.Log($"[{LogTag}:ShowRewardedInterstitial] UnityEditor is not supported now.");
    }

    #endregion Rewarded Interstitial
}
#endif