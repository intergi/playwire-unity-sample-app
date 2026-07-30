//
//  Created by Intergi
//  Copyright © 2021 Intergi. All rights reserved.
//

#import "PWUnityManager.h"
#import "PWBannerAdUnitManager.h"
#import "PWInterstitialAdUnitManager.h"
#import "PWRewardedAdUnitManager.h"
#import "PWAppOpenAdUnitManager.h"

@interface PWUnityManager()

@property (nonatomic, strong) NSMutableDictionary<NSString *, PWBannerAdUnitManager *> *bannersAdUnitManagers;
@property (nonatomic, strong) NSMutableDictionary<NSString *, PWInterstitialAdUnitManager *> *interstitialsAdUnitManagers;
@property (nonatomic, strong) NSMutableDictionary<NSString *, PWRewardedAdUnitManager *> *rewardedAdUnitManagers;
@property (nonatomic, strong) NSMutableDictionary<NSString *, PWAppOpenAdUnitManager *> *appOpenAdUnitManagers;

@end

@implementation PWUnityManager

+ (instancetype)shared
{
    static dispatch_once_t token;
    static PWUnityManager *shared;
    dispatch_once(&token, ^{
        shared = [[PWUnityManager alloc] init];
    });
    return shared;
}

- (instancetype)init 
{
    self = [super init];
    if (self)
    {
        self.bannersAdUnitManagers = [[NSMutableDictionary alloc] init];
        self.interstitialsAdUnitManagers = [[NSMutableDictionary alloc] init];
        self.rewardedAdUnitManagers = [[NSMutableDictionary alloc] init];
        self.appOpenAdUnitManagers = [[NSMutableDictionary alloc] init];
    }
    return self;
}

+ (UIViewController*)unityViewController
{
    return [[[UIApplication sharedApplication] keyWindow] rootViewController];
}

- (PWBannerAdUnitManager *)bannerAdUnitManager:(NSString *)adUnitId
{
    PWBannerAdUnitManager* manager = [self.bannersAdUnitManagers valueForKey:adUnitId];
    if (!manager) {
        manager = [[PWBannerAdUnitManager alloc] initWithAdUnit:adUnitId];
        self.bannersAdUnitManagers[adUnitId] = manager;
    }
    return manager;
}

- (PWInterstitialAdUnitManager*)interstitialAdUnitManager:(NSString*)adUnitId
{
  PWInterstitialAdUnitManager* manager = [self.interstitialsAdUnitManagers valueForKey:adUnitId];
  if (!manager) {
      manager = [[PWInterstitialAdUnitManager alloc] initWithAdUnit:adUnitId];
      self.interstitialsAdUnitManagers[adUnitId] = manager;
  }
  return manager;
}

- (PWRewardedAdUnitManager*)rewardedAdUnitManager:(NSString*)adUnitId
{
  PWRewardedAdUnitManager* manager = [self.rewardedAdUnitManagers valueForKey:adUnitId];
  if (!manager) {
      manager = [[PWRewardedAdUnitManager alloc] initWithAdUnit:adUnitId];
      self.rewardedAdUnitManagers[adUnitId] = manager;
  }
  return manager;
}

- (PWAppOpenAdUnitManager*)appOpenAdUnitManager:(NSString*)adUnitId
{
  PWAppOpenAdUnitManager* manager = [self.appOpenAdUnitManagers valueForKey:adUnitId];
  if (!manager) {
      manager = [[PWAppOpenAdUnitManager alloc] initWithAdUnit:adUnitId];
      self.appOpenAdUnitManagers[adUnitId] = manager;
  }
  return manager;
}

- (void)setGlobalTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting
{
  [PlaywireSDK.shared.targeting clear];
  [PlaywireSDK.shared.targeting add:targeting];
}

- (void)setBanner:(NSString *)adUnitId
    withTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting
{
    PWBannerAdUnitManager *adUnitManager = [self bannerAdUnitManager:adUnitId];
    [adUnitManager setTargeting:targeting];
}

- (void)loadBanner:(NSString *)adUnitId
          position:(PWAdPosition)position
     withTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting
{
    PWBannerAdUnitManager *adUnitManager = [self bannerAdUnitManager:adUnitId];
    [adUnitManager loadBannerAtPosition:position withTargeting:targeting];
}

- (void)showBanner:(NSString *)adUnitId
{
    PWBannerAdUnitManager *adUnitManager = [self bannerAdUnitManager:adUnitId];
    [adUnitManager showBanner];
}

- (void)hideBanner:(NSString *)adUnitId
{
    PWBannerAdUnitManager *adUnitManager = [self bannerAdUnitManager:adUnitId];
    [adUnitManager hideBanner];
}

- (void)destroyBanner:(NSString *)adUnitId
{
    PWBannerAdUnitManager *adUnitManager = [self bannerAdUnitManager:adUnitId];
    [adUnitManager destroyBanner];
}

- (void)refreshBanner:(NSString *)adUnitId
{
    PWBannerAdUnitManager *adUnitManager = [self bannerAdUnitManager:adUnitId];
    [adUnitManager refreshBanner];
}

+ (void)sendUnityMessage:(NSString *)message
{
    UnitySendMessage("PlaywireSDKCallback", "HandleEvent", message.UTF8String);
}

#pragma mark - Interstitial

- (void)setInterstitial:(NSString *)adUnitId
          withTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting
{
  PWInterstitialAdUnitManager *unitManager = [self interstitialAdUnitManager:adUnitId];
  [unitManager setTargeting:targeting];
}

- (void)loadInterstitial:(NSString *)adUnitId withTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting
{
  PWInterstitialAdUnitManager *unitManager = [self interstitialAdUnitManager:adUnitId];
  [unitManager loadInterstitialWithTargeting:targeting];
}

- (void)showInterstitial:(NSString *)adUnitId
{
  PWInterstitialAdUnitManager *unitManager = [self interstitialAdUnitManager:adUnitId];
  [unitManager showInterstitial];
}

- (BOOL)isInterstitialReady:(NSString *)adUnitId {
  PWInterstitialAdUnitManager *unitManager = [self interstitialAdUnitManager:adUnitId];
  return [unitManager isInterstitialReady];
}

#pragma mark - Rewarded

- (void)setRewarded:(NSString *)adUnitId
      withTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting
{
  PWRewardedAdUnitManager *unitManager = [self rewardedAdUnitManager:adUnitId];
  [unitManager setTargeting:targeting];
}

- (void)loadRewarded:(NSString *)adUnitId withTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting
{
  PWRewardedAdUnitManager *unitManager = [self rewardedAdUnitManager:adUnitId];
  [unitManager loadRewardedWithTargeting:targeting];
}

- (BOOL)isRewardedReady:(NSString *)adUnitId
{
  PWRewardedAdUnitManager *unitManager = [self rewardedAdUnitManager:adUnitId];
  return [unitManager isRewardedReady];
}

- (void)showRewarded:(NSString *)adUnitId
{
  PWRewardedAdUnitManager *unitManager = [self rewardedAdUnitManager:adUnitId];
  [unitManager showRewarded];
}

#pragma mark - App Open Ad

- (void)setAppOpenAd:(NSString *)adUnitId
       withTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting
{
  PWAppOpenAdUnitManager *unitManager = [self appOpenAdUnitManager:adUnitId];
  [unitManager setTargeting:targeting];
}

- (void)loadAppOpenAd:(NSString *)adUnitId withTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting
{
  PWAppOpenAdUnitManager *unitManager = [self appOpenAdUnitManager:adUnitId];
  [unitManager loadAppOpenAdWithTargeting:targeting];
}

- (BOOL)isAppOpenAdReady:(NSString *)adUnitId
{
  PWAppOpenAdUnitManager *unitManager = [self appOpenAdUnitManager:adUnitId];
  return [unitManager isAppOpenAdReady];
}

- (void)showAppOpenAd:(NSString *)adUnitId
{
  PWAppOpenAdUnitManager *unitManager = [self appOpenAdUnitManager:adUnitId];
  [unitManager showAppOpenAd];
}

- (void)setAppOpenAdReloadOnExpiration:(NSString *)adUnitId isEnabled:(BOOL)isEnabled
{
  PWAppOpenAdUnitManager *unitManager = [self appOpenAdUnitManager:adUnitId];
  unitManager.autoReloadOnExpiration = isEnabled;
}

- (BOOL)getAppOpenAdReloadOnExpiration:(NSString *)adUnitId
{
  PWAppOpenAdUnitManager *unitManager = [self appOpenAdUnitManager:adUnitId];
  return unitManager.autoReloadOnExpiration;
}

#pragma mark - Test Ads

- (void)setTestAds:(BOOL)isEnabled
{
  PlaywireSDK.shared.test = isEnabled;
}

- (BOOL)getTestAds
{
  return PlaywireSDK.shared.test;
}

#pragma mark - CMP

- (void)setCMP:(PWCMPType)type
{
  PlaywireSDK.shared.cmp = type;
}

- (PWCMPType)getCMP
{
  return PlaywireSDK.shared.cmp;
}

#pragma mark - Log Level

- (void)setLogLevel:(LogLevel)level
{
  PlaywireSDK.shared.logLevel = level;
}
@end
