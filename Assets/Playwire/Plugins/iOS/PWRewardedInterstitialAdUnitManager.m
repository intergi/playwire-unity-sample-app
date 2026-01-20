//
//  Created by Intergi
//  Copyright © 2022 Intergi. All rights reserved.
//

#import "PWRewardedInterstitialAdUnitManager.h"
#import <GoogleMobileAds/GoogleMobileAds.h>
#import <PlaywireMobile/PlaywireMobile-Swift.h>
#import <Playwire-Swift.h>
#import "PWUnityManager.h"
#import "PWConstant.h"
#import "PWAdReward.h"

@interface PWRewardedInterstitialAdUnitManager ()<PWFullScreenAdDelegate>

@property (nonatomic, copy) NSString *adUnit;
@property (nonatomic, strong) PWRewardedInterstitial *rewardedInterstitialAd;
@property (nonatomic, assign) BOOL isUsed;
@property (nonatomic, strong) NSDictionary<NSString *,NSString *> * targets;

@end

@implementation PWRewardedInterstitialAdUnitManager

- (instancetype)initWithAdUnit:(NSString *)adUnit
{
    self = [super init];
    if (self)
    {
        self.adUnit = adUnit;
        self.isUsed = NO;
    }
    return self;
}

- (void)setTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting
{
  self.targets = targeting;
}

- (void)loadRewardedInterstitialWithTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting
{
    self.rewardedInterstitialAd = nil;
    self.isUsed = NO;
  
    UIViewController * viewController = [PWUnityManager unityViewController];
    self.rewardedInterstitialAd = [[PWRewardedInterstitial alloc] initWithAdUnitName:self.adUnit
                                                                      viewController:viewController
                                                                            delegate:self];
    
    if (self.targets)
    {
      [self.rewardedInterstitialAd.targeting add:self.targets];
    } else {
      [self.rewardedInterstitialAd.targeting clear];
    }
    if (targeting) {
        PWLoadParams* params = [[PWLoadParams new] withTargeting:targeting];
        [self.rewardedInterstitialAd loadWithParams:params];
    } else {
        [self.rewardedInterstitialAd load];
    }
}

- (void)showRewardedInterstitial
{
  if (![self.rewardedInterstitialAd isLoaded]) {
    return;
  };

  [self.rewardedInterstitialAd show];
}

- (BOOL)isRewardedInterstitialReady {
  return [self.rewardedInterstitialAd isLoaded] && !self.isUsed;
}

# pragma mark - PWFullScreenAdDelegate

- (void)fullScreenAdDidLoad:(PWFullScreenAd * _Nonnull)ad 
{
  if ([ad isLoaded]) {
    NSString* message = [PWUnityMessageBuilder buildWithName:PW_RewardedInterstitial_Loaded_Event
                                                    adUnitId:self.adUnit];
    [PWUnityManager sendUnityMessage:message];
  }
}

- (void)fullScreenAdDidFailToLoad:(PWFullScreenAd * _Nonnull)ad
{
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_RewardedInterstitial_FailedToLoad_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdWillPresentFullScreenContent:(PWFullScreenAd * _Nonnull)ad
{
  self.isUsed = YES;
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_RewardedInterstitial_Opened_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdWillDismissFullScreenContent:(PWFullScreenAd * _Nonnull)ad {}

- (void)fullScreenAdDidDismissFullScreenContent:(PWFullScreenAd * _Nonnull)ad
{
  self.isUsed = YES;
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_RewardedInterstitial_Closed_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdDidFailToPresentFullScreenContent:(PWFullScreenAd * _Nonnull)ad
{
  self.isUsed = YES;
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_RewardedInterstitial_FailedToOpen_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdDidRecordImpression:(PWFullScreenAd * _Nonnull)ad
{
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_RewardedInterstitial_RecordedImpression_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdDidUserEarn:(PWFullScreenAd * _Nonnull)ad
                           type:(NSString * _Nonnull)type
                         amount:(double)amount
{
    PWAdReward *reward = [[PWAdReward alloc] initWithType:type amount:amount];
    NSString* message = [PWUnityMessageBuilder buildWithName:PW_RewardedInterstitial_Earned_Event
                                                    adUnitId:self.adUnit
                                                      object:reward];
    [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdDidRecordClick:(PWFullScreenAd * _Nonnull)ad
{
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_RewardedInterstitial_Clicked_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

@end