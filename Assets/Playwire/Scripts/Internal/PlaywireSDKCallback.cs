using System;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using PlaywireSDKConstant;
using System.Text;

/// <summary>
/// The Playwire SDK callbacks handler.
/// </summary>
public class PlaywireSDKCallback : MonoBehaviour
{
    private const string LogTag = "PW_PlaywireSDKCallback";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static PlaywireSDKCallback Instance { get; private set; }

    # region SDK
    private static Action _onSDKInitializedEvent;

    /// <summary>
    /// It's fired when the Playwire SDK has finished initialization.
    /// </summary>
    public static event Action OnSDKInitializedEvent
    {
        add
        {
            _onSDKInitializedEvent += value;
        }
        remove
        {
            _onSDKInitializedEvent -= value;
        }
    }

    #endregion SDK

    # region Banner

    private static Action<PlaywireSDKEventArgs> _onBannerLoadedEvent;
    private static Action<PlaywireSDKEventArgs> _onBannerFailedToLoadEvent;
    private static Action<PlaywireSDKEventArgs> _onBannerOpenedEvent;
    private static Action<PlaywireSDKEventArgs> _onBannerClosedEvent;
    private static Action<PlaywireSDKEventArgs> _onBannerClickedEvent;
    private static Action<PlaywireSDKEventArgs> _onBannerRecordedImpressionEvent;

    public static class Banner {
        /// <summary>
        /// It's fired when the banner ad successfully loaded content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnLoadedEvent
        {
            add
            {
                _onBannerLoadedEvent += value;
            }
            remove
            {
                _onBannerLoadedEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when the banner ad failed to load content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnFailedToLoadEvent
        {
            add
            {
                _onBannerFailedToLoadEvent += value;
            }
            remove
            {
                _onBannerFailedToLoadEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when the banner ad presented content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnOpenedEvent
        {
            add
            {
                _onBannerOpenedEvent += value;
            }
            remove
            {
                _onBannerOpenedEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when the banner ad dismissed content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnClosedEvent
        {
            add
            {
                _onBannerClosedEvent += value;
            }
            remove
            {
                _onBannerClosedEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when a click has been recorded for the banner ad.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnClickedEvent
        {
            add
            {
                _onBannerClickedEvent += value;
            }
            remove
            {
                _onBannerClickedEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when an impression has been recorded for the banner ad.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnRecordedImpressionEvent
        {
            add
            {
                _onBannerRecordedImpressionEvent += value;
            }
            remove
            {
                _onBannerRecordedImpressionEvent -= value;
            }
        }
    }

    #endregion Banner

    #region Interstitials

    private static Action<PlaywireSDKEventArgs> _onInterstitialLoadedEvent;
    private static Action<PlaywireSDKEventArgs> _onInterstitialFailedToLoadEvent;
    private static Action<PlaywireSDKEventArgs> _onInterstitialOpenedEvent;
    private static Action<PlaywireSDKEventArgs> _onInterstitialFailedToOpenEvent;
    private static Action<PlaywireSDKEventArgs> _onInterstitialClosedEvent;
    private static Action<PlaywireSDKEventArgs> _onInterstitialRecordedImpressionEvent;
    private static Action<PlaywireSDKEventArgs> _onInterstitialClickedEvent;

    public static class Interstitial {

        /// <summary>
        /// It's fired when the interstitial ad successfully loaded full screen content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnLoadedEvent
        {
            add
            {
                _onInterstitialLoadedEvent += value;
            }
            remove
            {
                _onInterstitialLoadedEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when the interstitial ad failed to load full screen content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnFailedToLoadEvent
        {
            add
            {
                _onInterstitialFailedToLoadEvent += value;
            }
            remove
            {
                _onInterstitialFailedToLoadEvent -= value;
            }
        }
        /// <summary>
        /// It's fired when the interstitial ad presented full screen content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnOpenedEvent
        {
            add
            {
                _onInterstitialOpenedEvent += value;
            }
            remove
            {
                _onInterstitialOpenedEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when the interstitial ad failed to present full screen content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnFailedToOpenEvent
        {
            add
            {
                _onInterstitialFailedToOpenEvent += value;
            }
            remove
            {
                _onInterstitialFailedToOpenEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when an interstitial ad dismissed full screen content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnClosedEvent
        {
            add
            {
                _onInterstitialClosedEvent += value;
            }
            remove
            {
                _onInterstitialClosedEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when an impression has been recorded for the interstitial ad.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnRecordedImpressionEvent
        {
            add
            {
                _onInterstitialRecordedImpressionEvent += value;
            }
            remove
            {
                _onInterstitialRecordedImpressionEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when a click has been recorded for the interstitial ad.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnClickedEvent
        {
            add
            {
                _onInterstitialClickedEvent += value;
            }
            remove
            {
                _onInterstitialClickedEvent -= value;
            }
        }
    }

    #endregion Interstitials

    #region Rewarded

    private static Action<PlaywireSDKEventArgs> _onRewardedLoadedEvent;
    private static Action<PlaywireSDKEventArgs> _onRewardedFailedToLoadEvent;
    private static Action<PlaywireSDKEventArgs> _onRewardedOpenedEvent;
    private static Action<PlaywireSDKEventArgs> _onRewardedFailedToOpenEvent;
    private static Action<PlaywireSDKEventArgs> _onRewardedClosedEvent;
    private static Action<PlaywireSDKEventArgs> _onRewardedRecordedImpressionEvent;
    private static Action<PlaywireSDKAdRewardEventArgs> _onRewardedEarnedEvent;
    private static Action<PlaywireSDKEventArgs> _onRewardedClickedEvent;

    public static class Rewarded {
        /// <summary>
        /// It's fired when the rewarded ad successfully loaded full screen content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnLoadedEvent
        {
            add
            {
                _onRewardedLoadedEvent += value;
            }
            remove
            {
                _onRewardedLoadedEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when the rewarded ad failed to load full screen content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnFailedToLoadEvent
        {
            add
            {
                _onRewardedFailedToLoadEvent += value;
            }
            remove
            {
                _onRewardedFailedToLoadEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when the rewarded ad presented full screen content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnOpenedEvent
        {
            add
            {
                _onRewardedOpenedEvent += value;
            }
            remove
            {
                _onRewardedOpenedEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when the rewarded ad failed to present full screen content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnFailedToOpenEvent
        {
            add
            {
                _onRewardedFailedToOpenEvent += value;
            }
            remove
            {
                _onRewardedFailedToOpenEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when an rewarded ad dismissed full screen content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnClosedEvent
        {
            add
            {
                _onRewardedClosedEvent += value;
            }
            remove
            {
                _onRewardedClosedEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when an impression has been recorded for the rewarded ad.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnRecordedImpressionEvent
        {
            add
            {
                _onRewardedRecordedImpressionEvent += value;
            }
            remove
            {
                _onRewardedRecordedImpressionEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when a reward has been earned.
        /// </summary>
        public static event Action<PlaywireSDKAdRewardEventArgs> OnEarnedEvent
        {
            add
            {
                _onRewardedEarnedEvent += value;
            }
            remove
            {
                _onRewardedEarnedEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when a click has been recorded for the rewarded ad.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnClickedEvent
        {
            add
            {
                _onRewardedClickedEvent += value;
            }
            remove
            {
                _onRewardedClickedEvent -= value;
            }
        }

    }

    #endregion Rewarded

    #region AppOpenAd

    private static Action<PlaywireSDKEventArgs> _onAppOpenAdLoadedEvent;
    private static Action<PlaywireSDKEventArgs> _onAppOpenAdFailedToLoadEvent;
    private static Action<PlaywireSDKEventArgs> _onAppOpenAdOpenedEvent;
    private static Action<PlaywireSDKEventArgs> _onAppOpenAdFailedToOpenEvent;
    private static Action<PlaywireSDKEventArgs> _onAppOpenAdClosedEvent;
    private static Action<PlaywireSDKEventArgs> _onAppOpenAdRecordedImpressionEvent;
    private static Action<PlaywireSDKEventArgs> _onAppOpenAdClickedEvent;

    public static class AppOpenAd {

        /// <summary>
        /// It's fired when the app open ad successfully loaded full screen content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnLoadedEvent
        {
            add
            {
                _onAppOpenAdLoadedEvent += value;
            }
            remove
            {
                _onAppOpenAdLoadedEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when the app open ad failed to load full screen content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnFailedToLoadEvent
        {
            add
            {
                _onAppOpenAdFailedToLoadEvent += value;
            }
            remove
            {
                _onAppOpenAdFailedToLoadEvent -= value;
            }
        }
        /// <summary>
        /// It's fired when the app open ad presented full screen content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnOpenedEvent
        {
            add
            {
                _onAppOpenAdOpenedEvent += value;
            }
            remove
            {
                _onAppOpenAdOpenedEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when the app open ad failed to present full screen content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnFailedToOpenEvent
        {
            add
            {
                _onAppOpenAdFailedToOpenEvent += value;
            }
            remove
            {
                _onAppOpenAdFailedToOpenEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when the app open ad dismissed full screen content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnClosedEvent
        {
            add
            {
                _onAppOpenAdClosedEvent += value;
            }
            remove
            {
                _onAppOpenAdClosedEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when an impression has been recorded for the app open ad.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnRecordedImpressionEvent
        {
            add
            {
                _onAppOpenAdRecordedImpressionEvent += value;
            }
            remove
            {
                _onAppOpenAdRecordedImpressionEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when a click has been recorded for the app open ad.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnClickedEvent
        {
            add
            {
                _onAppOpenAdClickedEvent += value;
            }
            remove
            {
                _onAppOpenAdClickedEvent -= value;
            }
        }
    }

    #endregion AppOpenAd

    #region Rewarded Interstitial

    private static Action<PlaywireSDKEventArgs> _onRewardedInterstitialLoadedEvent;
    private static Action<PlaywireSDKEventArgs> _onRewardedInterstitialFailedToLoadEvent;
    private static Action<PlaywireSDKEventArgs> _onRewardedInterstitialOpenedEvent;
    private static Action<PlaywireSDKEventArgs> _onRewardedInterstitialFailedToOpenEvent;
    private static Action<PlaywireSDKEventArgs> _onRewardedInterstitialClosedEvent;
    private static Action<PlaywireSDKEventArgs> _onRewardedInterstitialRecordedImpressionEvent;
    private static Action<PlaywireSDKAdRewardEventArgs> _onRewardedInterstitialEarnedEvent;
    private static Action<PlaywireSDKEventArgs> _onRewardedInterstitialClickedEvent;

    public static class RewardedInterstitial {
        /// <summary>
        /// It's fired when the rewarded interstitial ad successfully loaded full screen content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnLoadedEvent
        {
            add
            {
                _onRewardedInterstitialLoadedEvent += value;
            }
            remove
            {
                _onRewardedInterstitialLoadedEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when the rewarded interstitial ad failed to load full screen content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnFailedToLoadEvent
        {
            add
            {
                _onRewardedInterstitialFailedToLoadEvent += value;
            }
            remove
            {
                _onRewardedInterstitialFailedToLoadEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when the rewarded interstitial ad presented full screen content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnOpenedEvent
        {
            add
            {
                _onRewardedInterstitialOpenedEvent += value;
            }
            remove
            {
                _onRewardedInterstitialOpenedEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when the rewarded interstitial ad failed to present full screen content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnFailedToOpenEvent
        {
            add
            {
                _onRewardedInterstitialFailedToOpenEvent += value;
            }
            remove
            {
                _onRewardedInterstitialFailedToOpenEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when an rewarded interstitial ad dismissed full screen content.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnClosedEvent
        {
            add
            {
                _onRewardedInterstitialClosedEvent += value;
            }
            remove
            {
                _onRewardedInterstitialClosedEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when an impression has been recorded for the rewarded interstitial ad.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnRecordedImpressionEvent
        {
            add
            {
                _onRewardedInterstitialRecordedImpressionEvent += value;
            }
            remove
            {
                _onRewardedInterstitialRecordedImpressionEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when a reward interstitial has been earned.
        /// </summary>
        public static event Action<PlaywireSDKAdRewardEventArgs> OnEarnedEvent
        {
            add
            {
                _onRewardedInterstitialEarnedEvent += value;
            }
            remove
            {
                _onRewardedInterstitialEarnedEvent -= value;
            }
        }

        /// <summary>
        /// It's fired when a click has been recorded for the rewarded interstitial ad.
        /// </summary>
        public static event Action<PlaywireSDKEventArgs> OnClickedEvent
        {
            add
            {
                _onRewardedInterstitialClickedEvent += value;
            }
            remove
            {
                _onRewardedInterstitialClickedEvent -= value;
            }
        }

    }

    #endregion Rewarded

    private void HandleEvent(string totalMessage)
    {
        proccessMessage(totalMessage);
    }

    private static void InvokeAction(Action action)
    {
        if (action != null)
        {
            action();
        }
    }

    private static void InvokeEvent(Action<PlaywireSDKEventArgs> action, string adUnitId)
    {
        if (action != null)
        {
            action.Invoke(new PlaywireSDKEventArgs(adUnitId));
        }
    }

    private static void invokeAdRewardEarnedEvent(Action<PlaywireSDKAdRewardEventArgs> action, PlaywireSDKEventMessage message)
    {
        if (string.IsNullOrEmpty(message.parameters)) 
        {
            return;
        }

        var bytes = System.Convert.FromBase64String(message.parameters);
        string parameters = Encoding.UTF8.GetString(bytes);

        PlaywireSDKAdReward adReward = PlaywireSDKAdReward.CreateFromJSON(parameters);
        if (adReward == null || action == null) 
        {
            return;
        }
        action.Invoke(new PlaywireSDKAdRewardEventArgs(message.adUnitId, adReward.type, adReward.amount));
    }

    private static void proccessMessage(string str)
    {
        PlaywireSDKEventMessage message = PlaywireSDKEventMessage.CreateFromJSON(str);
        if (string.IsNullOrEmpty(message.name)) 
        {
            return;
        }

        switch (message.name) {
            case PlaywireSDKConstant.Event.SDK.Initialization:
                InvokeAction(_onSDKInitializedEvent);
                break;
            case PlaywireSDKConstant.Event.Banner.Loaded:
                InvokeEvent(_onBannerLoadedEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.Banner.FailedToLoad:
                InvokeEvent(_onBannerFailedToLoadEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.Banner.Opened:
                InvokeEvent(_onBannerOpenedEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.Banner.Closed:
                InvokeEvent(_onBannerClosedEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.Banner.Clicked:
                InvokeEvent(_onBannerClickedEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.Banner.RecordedImpression:
                InvokeEvent(_onBannerRecordedImpressionEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.Interstitial.Loaded:
                InvokeEvent(_onInterstitialLoadedEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.Interstitial.FailedToLoad:
                InvokeEvent(_onInterstitialFailedToLoadEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.Interstitial.Opened:
                InvokeEvent(_onInterstitialOpenedEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.Interstitial.FailedToOpen:
                InvokeEvent(_onInterstitialFailedToOpenEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.Interstitial.Closed:
                InvokeEvent(_onInterstitialClosedEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.Interstitial.RecordedImpression:
                 InvokeEvent(_onInterstitialRecordedImpressionEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.Interstitial.Clicked:
                InvokeEvent(_onInterstitialClickedEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.Rewarded.Loaded:
                InvokeEvent(_onRewardedLoadedEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.Rewarded.FailedToLoad:
                InvokeEvent(_onRewardedFailedToLoadEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.Rewarded.Opened:
                InvokeEvent(_onRewardedOpenedEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.Rewarded.FailedToOpen:
                InvokeEvent(_onRewardedFailedToOpenEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.Rewarded.Closed:
                InvokeEvent(_onRewardedClosedEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.Rewarded.RecordedImpression:
                InvokeEvent(_onRewardedRecordedImpressionEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.Rewarded.Earned:
                invokeAdRewardEarnedEvent(_onRewardedEarnedEvent, message);
                break;
            case PlaywireSDKConstant.Event.Rewarded.Clicked:
                InvokeEvent(_onRewardedClickedEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.AppOpenAd.Loaded:
                InvokeEvent(_onAppOpenAdLoadedEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.AppOpenAd.FailedToLoad:
                InvokeEvent(_onAppOpenAdFailedToLoadEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.AppOpenAd.Opened:
                InvokeEvent(_onAppOpenAdOpenedEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.AppOpenAd.FailedToOpen:
                InvokeEvent(_onAppOpenAdFailedToOpenEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.AppOpenAd.Closed:
                InvokeEvent(_onAppOpenAdClosedEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.AppOpenAd.RecordedImpression:
                 InvokeEvent(_onAppOpenAdRecordedImpressionEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.AppOpenAd.Clicked:
                InvokeEvent(_onAppOpenAdClickedEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.RewardedInterstitial.Loaded:
                InvokeEvent(_onRewardedInterstitialLoadedEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.RewardedInterstitial.FailedToLoad:
                InvokeEvent(_onRewardedInterstitialFailedToLoadEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.RewardedInterstitial.Opened:
                InvokeEvent(_onRewardedInterstitialOpenedEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.RewardedInterstitial.FailedToOpen:
                InvokeEvent(_onRewardedInterstitialFailedToOpenEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.RewardedInterstitial.Closed:
                InvokeEvent(_onRewardedInterstitialClosedEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.RewardedInterstitial.RecordedImpression:
                InvokeEvent(_onRewardedInterstitialRecordedImpressionEvent, message.adUnitId);
                break;
            case PlaywireSDKConstant.Event.RewardedInterstitial.Earned:
                invokeAdRewardEarnedEvent(_onRewardedInterstitialEarnedEvent, message);
                break;
            case PlaywireSDKConstant.Event.RewardedInterstitial.Clicked:
                InvokeEvent(_onRewardedInterstitialClickedEvent, message.adUnitId);
                break;

            default:
                break;
        }
    }
}