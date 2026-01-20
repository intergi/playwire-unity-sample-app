using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

public class HomeScreen : MonoBehaviour
{
    // --- NATIVE BRIDGE IMPORTS ---
    #if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")] private static extern void _ShowAdTypeList(string jsonList);
    [DllImport("__Internal")] private static extern void _ShowDetailScreen(string title, string status);
    [DllImport("__Internal")] private static extern void _UpdateStatus(string status);
    #endif

    private const string LogTag = "PW_HomeScreen";

    [Serializable]
    public class AdTypeItem
    {
        public string key;         // Display Text
        public string adUnitName;  // Logic ID
    }

    [Serializable]
    public class AdTypeListWrapper
    {
        public AdTypeItem[] items;
    }

    private AdTypeItem[] adTypes = new AdTypeItem[]
    {
        new AdTypeItem { key = "App Open - GAM", adUnitName = "app-open-gam" },
        new AdTypeItem { key = "App Open - MAX", adUnitName = "app-open-max" },
        new AdTypeItem { key = "Banner 320x50 - GAM", adUnitName = "banner-320x50-gam" },
        new AdTypeItem { key = "Banner 320x50 - MAX", adUnitName = "banner-320x50-max" },
        new AdTypeItem { key = "Banner 300x250 - GAM", adUnitName = "banner-300x250-gam" },
        new AdTypeItem { key = "Banner 300x250 - MAX", adUnitName = "banner-300x250-max" },
        new AdTypeItem { key = "Interstitial - GAM", adUnitName = "interstitial-gam" },
        new AdTypeItem { key = "Interstitial - MAX", adUnitName = "interstitial-max" },
        new AdTypeItem { key = "Rewarded - GAM", adUnitName = "rewarded-gam" }, 
        new AdTypeItem { key = "Rewarded - MAX", adUnitName = "rewarded-video-max" },
    };

    private bool isInitialized = false;
    private string currentAdUnit;

    void Start()
    {
        string pubId = "1024407";
        string appId = "703"; // Android default
        
        #if UNITY_IOS
        appId = "702";
        // Update Rewarded ID for iOS
        foreach(var item in adTypes) {
            if(item.adUnitName == "rewarded-gam") item.adUnitName = "rewarded-video-gam";
        }
        #endif

        PlaywireSDK.StartConsoleLogger();
        PlaywireSDK.Test = true;

        // Subscribe to Events
        RegisterCallbacks();

        // Initialize SDK
        PlaywireSDK.InitializeSDK(pubId, appId);
        
        // Show Native Loading State
        UpdateNativeStatus("Initializing SDK...");
    }

    // --- NATIVE INTERFACE ---

    // Called by Native when a list item is clicked
    public void OnNativeSelection(string adUnitName)
    {
        currentAdUnit = adUnitName;
        Debug.Log($"[{LogTag}] Selected: {adUnitName}");

        // Switch to Detail Screen on Native Side
        ShowNativeDetailScreen(adUnitName, "Loading...");

        // Trigger Playwire Logic based on name
        if (adUnitName.Contains("app-open"))
        {
            PlaywireSDK.LoadAppOpenAd(adUnitName);
        }
        else if (adUnitName.Contains("banner"))
        {
            PlaywireSDKBase.AdPosition pos = PlaywireSDKBase.AdPosition.BottomCenter;
            PlaywireSDK.LoadBanner(adUnitName, pos);
        }
        else if (adUnitName.Contains("interstitial"))
        {
            PlaywireSDK.LoadInterstitial(adUnitName);
        }
        else if (adUnitName.Contains("rewarded"))
        {
            PlaywireSDK.LoadRewarded(adUnitName);
        }
    }

    // Called by Native Back Button
    public void OnNativeBack()
    {
        // Cleanup existing ads if needed
        if (!string.IsNullOrEmpty(currentAdUnit))
        {
            if (currentAdUnit.Contains("banner"))
            {
                PlaywireSDK.DestroyBanner(currentAdUnit);
            }
        }
        currentAdUnit = null;
        
        // Re-show list
        ShowNativeList();
    }

    // --- HELPER METHODS ---

    void ShowNativeList()
    {
        string json = JsonUtility.ToJson(new AdTypeListWrapper { items = adTypes });

        #if UNITY_ANDROID && !UNITY_EDITOR
        GetAndroidBridge().CallStatic("showAdTypes", json);
        #elif UNITY_IOS && !UNITY_EDITOR
        _ShowAdTypeList(json);
        #else
        Debug.Log("Mock: Show Native List");
        #endif
    }

    void ShowNativeDetailScreen(string title, string status)
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
        GetAndroidBridge().CallStatic("showDetailScreen", title, status);
        #elif UNITY_IOS && !UNITY_EDITOR
        _ShowDetailScreen(title, status);
        #else
        Debug.Log($"Mock: Show Details for {title} - {status}");
        #endif
    }

    void UpdateNativeStatus(string status)
    {
        Debug.Log($"[{LogTag}] Status Update: {status}");
        #if UNITY_ANDROID && !UNITY_EDITOR
        GetAndroidBridge().CallStatic("updateStatus", status);
        #elif UNITY_IOS && !UNITY_EDITOR
        _UpdateStatus(status);
        #endif
    }

    // --- PLAYWIRE CALLBACKS ---

    void RegisterCallbacks()
    {
        PlaywireSDKCallback.OnSDKInitializedEvent += () => {
            isInitialized = true;
            ShowNativeList(); // SDK Ready -> Show the list
        };

        // --- BANNER ---
        PlaywireSDKCallback.Banner.OnLoadedEvent += (args) => {
            UpdateNativeStatus("Banner Loaded. Showing...");
            PlaywireSDK.ShowBanner(currentAdUnit);
        };
        PlaywireSDKCallback.Banner.OnFailedToLoadEvent += (args) => UpdateNativeStatus("Banner Failed to Load");

        // --- INTERSTITIAL ---
        PlaywireSDKCallback.Interstitial.OnLoadedEvent += (args) => {
            UpdateNativeStatus("Interstitial Loaded. Showing...");
            PlaywireSDK.ShowInterstitial(currentAdUnit);
        };
        PlaywireSDKCallback.Interstitial.OnFailedToLoadEvent += (args) => UpdateNativeStatus("Interstitial Failed to Load");
        PlaywireSDKCallback.Interstitial.OnClosedEvent += (args) => {
            UpdateNativeStatus("Interstitial Closed");
        };

        // --- REWARDED ---
        PlaywireSDKCallback.Rewarded.OnLoadedEvent += (args) => {
            UpdateNativeStatus("Rewarded Loaded. Showing...");
            PlaywireSDK.ShowRewarded(currentAdUnit);
        };
        PlaywireSDKCallback.Rewarded.OnFailedToLoadEvent += (args) => UpdateNativeStatus("Rewarded Failed to Load");
        PlaywireSDKCallback.Rewarded.OnEarnedEvent += (args) => {
            UpdateNativeStatus($"Reward Earned: {args.Amount} {args.Type}");
        };
        PlaywireSDKCallback.Rewarded.OnClosedEvent += (args) => {
            UpdateNativeStatus("Rewarded Ad Closed");
        };
        
        // --- APP OPEN ---
        PlaywireSDKCallback.AppOpenAd.OnLoadedEvent += (args) => {
            UpdateNativeStatus("App Open Loaded. Showing...");
            PlaywireSDK.ShowAppOpenAd(currentAdUnit);
        };
        PlaywireSDKCallback.AppOpenAd.OnFailedToLoadEvent += (args) => UpdateNativeStatus("App Open Failed to Load");
        PlaywireSDKCallback.AppOpenAd.OnClosedEvent += (args) => {
            UpdateNativeStatus("App Open Ad Closed");
        };
    }

    #if UNITY_ANDROID
    private AndroidJavaClass _bridge;
    private AndroidJavaClass GetAndroidBridge()
    {
        if (_bridge == null) _bridge = new AndroidJavaClass("com.unity.nativeui.NativeUiBridge");
        return _bridge;
    }
    #endif
}