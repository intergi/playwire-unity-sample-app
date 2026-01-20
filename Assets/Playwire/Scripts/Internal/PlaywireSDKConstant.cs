namespace PlaywireSDKConstant
{
    namespace Event {

        internal static class Banner
        {
            internal const string Loaded = "PW_OnBannerLoadedEvent";
            internal const string FailedToLoad = "PW_OnBannerAdFailedToLoadEvent";
            internal const string Opened = "PW_OnBannerAdOpenedEvent";
            internal const string Closed = "PW_OnBannerAdClosedEvent";
            internal const string Clicked = "PW_OnBannerAdClickedEvent";
            internal const string RecordedImpression = "PW_OnBannerAdRecordedImpressionEvent";
        }
        internal static class Interstitial
        {
            internal const string Loaded = "PW_OnInterstitialAdLoadedEvent";
            internal const string FailedToLoad = "PW_OnInterstitialAdFailedToLoadEvent";
            internal const string Opened = "PW_OnInterstitialAdOpenedEvent";
            internal const string FailedToOpen = "PW_OnInterstitialAdFailedToOpenEvent";
            internal const string Closed = "PW_OnInterstitialAdClosedEvent";
            internal const string RecordedImpression = "PW_OnInterstitialAdRecordedImpressionEvent";
            internal const string Clicked = "PW_OnInterstitialAdClickedEvent";
        }
        internal static class Rewarded
        {
            internal const string Loaded = "PW_OnRewardedAdLoadedEvent";
            internal const string FailedToLoad = "PW_OnRewardedAdFailedToLoadEvent";
            internal const string Opened = "PW_OnRewardedAdOpenedEvent";
            internal const string FailedToOpen = "PW_OnRewardedAdFailedToOpenEvent";
            internal const string Closed = "PW_OnRewardedAdClosedEvent";
            internal const string RecordedImpression = "PW_OnRewardedAdRecordedImpressionEvent";
            internal const string Earned = "PW_OnRewardedAdEarnedEvent";
            internal const string Clicked = "PW_OnRewardedAdClickedEvent";
        }

        internal static class AppOpenAd
        {
            internal const string Loaded = "PW_OnAppOpenAdLoadedEvent";
            internal const string FailedToLoad = "PW_OnAppOpenAdFailedToLoadEvent";
            internal const string Opened = "PW_OnAppOpenAdOpenedEvent";
            internal const string FailedToOpen = "PW_OnAppOpenAdFailedToOpenEvent";
            internal const string Closed = "PW_OnAppOpenAdClosedEvent";
            internal const string RecordedImpression = "PW_OnAppOpenAdRecordedImpressionEvent";
            internal const string Clicked = "PW_OnAppOpenAdClickedEvent";
        }

        internal static class RewardedInterstitial
        {
            internal const string Loaded = "PW_OnRewardedInterstitialAdLoadedEvent";
            internal const string FailedToLoad = "PW_OnRewardedInterstitialAdFailedToLoadEvent";
            internal const string Opened = "PW_OnRewardedInterstitialAdOpenedEvent";
            internal const string FailedToOpen = "PW_OnRewardedInterstitialAdFailedToOpenEvent";
            internal const string Closed = "PW_OnRewardedInterstitialAdClosedEvent";
            internal const string RecordedImpression = "PW_OnRewardedInterstitialAdRecordedImpressionEvent";
            internal const string Earned = "PW_OnRewardedInterstitialAdEarnedEvent";
            internal const string Clicked = "PW_OnRewardedInterstitialAdClickedEvent";
        }
        internal static class SDK
        {
            internal const string Initialization = "PW_OnSDKInitializedEvent";
        }
    }
}