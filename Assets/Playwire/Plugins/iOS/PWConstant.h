//
//  Created by Intergi
//  Copyright © 2021 Intergi. All rights reserved.
//

#ifndef PWConstant_h
#define PWConstant_h

#import <Foundation/Foundation.h>

    /// SDK
    static NSString *const PW_SDK_Initialization_Event = @"PW_OnSDKInitializedEvent";

    /// Banner
    static NSString *const PW_Banner_Loaded_Event = @"PW_OnBannerLoadedEvent";
    static NSString *const PW_Banner_FailedToLoad_Event = @"PW_OnBannerAdFailedToLoadEvent";
    static NSString *const PW_Banner_Opened_Event = @"PW_OnBannerAdOpenedEvent";
    static NSString *const PW_Banner_Closed_Event = @"PW_OnBannerAdClosedEvent";
    static NSString *const PW_Banner_Clicked_Event = @"PW_OnBannerAdClickedEvent";
    static NSString *const PW_Banner_RecordedImpression_Event = @"PW_OnBannerAdRecordedImpressionEvent";

    /// Interstitial
    static NSString *const PW_Interstitial_Loaded_Event = @"PW_OnInterstitialAdLoadedEvent";
    static NSString *const PW_Interstitial_FailedToLoad_Event = @"PW_OnInterstitialAdFailedToLoadEvent";
    static NSString *const PW_Interstitial_Opened_Event = @"PW_OnInterstitialAdOpenedEvent";
    static NSString *const PW_Interstitial_FailedToOpen_Event = @"PW_OnInterstitialAdFailedToOpenEvent";
    static NSString *const PW_Interstitial_Closed_Event = @"PW_OnInterstitialAdClosedEvent";
    static NSString *const PW_Interstitial_RecordedImpression_Event = @"PW_OnInterstitialAdRecordedImpressionEvent";
    static NSString *const PW_Interstitial_Clicked_Event = @"PW_OnInterstitialAdClickedEvent";

    /// Rewarded
    static NSString *const PW_Rewarded_Loaded_Event = @"PW_OnRewardedAdLoadedEvent";
    static NSString *const PW_Rewarded_FailedToLoad_Event = @"PW_OnRewardedAdFailedToLoadEvent";
    static NSString *const PW_Rewarded_Opened_Event = @"PW_OnRewardedAdOpenedEvent";
    static NSString *const PW_Rewarded_FailedToOpen_Event = @"PW_OnRewardedAdFailedToOpenEvent";
    static NSString *const PW_Rewarded_Closed_Event = @"PW_OnRewardedAdClosedEvent";
    static NSString *const PW_Rewarded_RecordedImpression_Event = @"PW_OnRewardedAdRecordedImpressionEvent";
    static NSString *const PW_Rewarded_Earned_Event = @"PW_OnRewardedAdEarnedEvent";
    static NSString *const PW_Rewarded_Clicked_Event = @"PW_OnRewardedAdClickedEvent";

    /// AppOpenAd
    static NSString *const PW_AppOpenAd_Loaded_Event = @"PW_OnAppOpenAdLoadedEvent";
    static NSString *const PW_AppOpenAd_FailedToLoad_Event = @"PW_OnAppOpenAdFailedToLoadEvent";
    static NSString *const PW_AppOpenAd_Opened_Event = @"PW_OnAppOpenAdOpenedEvent";
    static NSString *const PW_AppOpenAd_FailedToOpen_Event = @"PW_OnAppOpenAdFailedToOpenEvent";
    static NSString *const PW_AppOpenAd_Closed_Event = @"PW_OnAppOpenAdClosedEvent";
    static NSString *const PW_AppOpenAd_RecordedImpression_Event = @"PW_OnAppOpenAdRecordedImpressionEvent";
    static NSString *const PW_AppOpenAd_Clicked_Event = @"PW_OnAppOpenAdClickedEvent";

    /// Rewarded
    static NSString *const PW_RewardedInterstitial_Loaded_Event = @"PW_OnRewardedInterstitialAdLoadedEvent";
    static NSString *const PW_RewardedInterstitial_FailedToLoad_Event = @"PW_OnRewardedInterstitialAdFailedToLoadEvent";
    static NSString *const PW_RewardedInterstitial_Opened_Event = @"PW_OnRewardedInterstitialAdOpenedEvent";
    static NSString *const PW_RewardedInterstitial_FailedToOpen_Event = @"PW_OnRewardedInterstitialAdFailedToOpenEvent";
    static NSString *const PW_RewardedInterstitial_Closed_Event = @"PW_OnRewardedInterstitialAdClosedEvent";
    static NSString *const PW_RewardedInterstitial_RecordedImpression_Event = @"PW_OnRewardedInterstitialAdRecordedImpressionEvent";
    static NSString *const PW_RewardedInterstitial_Earned_Event = @"PW_OnRewardedInterstitialAdEarnedEvent";
    static NSString *const PW_RewardedInterstitial_Clicked_Event = @"PW_OnRewardedInterstitialAdClickedEvent";

#endif
