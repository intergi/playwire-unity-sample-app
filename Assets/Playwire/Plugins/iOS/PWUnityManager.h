//
//  Created by Intergi
//  Copyright © 2021 Intergi. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <GoogleMobileAds/GoogleMobileAds.h>
#import <PlaywireMobile/PlaywireMobile-Swift.h>
#import <Playwire-Swift.h>
#import "PWAdPosition.h"
#import "PWUnityMessageBuilder.h"
#import "PWConstant.h"

NS_ASSUME_NONNULL_BEGIN

@interface PWUnityManager : NSObject

+ (instancetype)shared;
- (instancetype)init NS_UNAVAILABLE;

- (void)setGlobalTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting;

- (void)startConsoleLogger;
- (void)loadBanner:(NSString *)adUnitId
          position:(PWAdPosition)position
     withTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting;
- (void)setBanner:(NSString *)adUnitId
    withTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting;
- (void)showBanner:(NSString *)adUnitId;
- (void)hideBanner:(NSString *)adUnitId;
- (void)refreshBanner:(NSString *)adUnitId;

- (void)setInterstitial:(NSString *)adUnitId
          withTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting;
- (void)loadInterstitial:(NSString *)adUnitId withTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting;
- (BOOL)isInterstitialReady:(NSString *)adUnitId;
- (void)showInterstitial:(NSString *)adUnitId;

- (void)setRewarded:(NSString *)adUnitId
      withTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting;
- (void)loadRewarded:(NSString *)adUnitId withTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting;
- (BOOL)isRewardedReady:(NSString *)adUnitId;
- (void)showRewarded:(NSString *)adUnitId;

- (void)setAppOpenAd:(NSString *)adUnitId
       withTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting;
- (void)loadAppOpenAd:(NSString *)adUnitId withTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting;
- (BOOL)isAppOpenAdReady:(NSString *)adUnitId;
- (void)showAppOpenAd:(NSString *)adUnitId;
- (void)setAppOpenAdReloadOnExpiration:(NSString *)adUnitId isEnabled:(BOOL)isEnabled;
- (BOOL)getAppOpenAdReloadOnExpiration:(NSString *)adUnitId;

- (void)setTestAds:(BOOL)isEnabled;
- (BOOL)getTestAds;

- (void)setCMP:(PWCMPType)type;
- (PWCMPType)getCMP;

- (void)setRewardedInterstitial:(NSString *)adUnitId
                  withTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting;
- (void)loadRewardedInterstitial:(NSString *)adUnitId withTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting;
- (BOOL)isRewardedInterstitialReady:(NSString *)adUnitId;
- (void)showRewardedInterstitial:(NSString *)adUnitId;

+ (UIViewController*)unityViewController;
+ (void)sendUnityMessage:(NSString *)message;

@end

NS_ASSUME_NONNULL_END
