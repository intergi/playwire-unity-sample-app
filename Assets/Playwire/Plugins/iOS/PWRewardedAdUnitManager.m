//
//  Created by Intergi
//  Copyright © 2021 Intergi. All rights reserved.
//

#import "PWRewardedAdUnitManager.h"
#import <GoogleMobileAds/GoogleMobileAds.h>
#import <PlaywireMobile/PlaywireMobile-Swift.h>
#import <Playwire-Swift.h>
#import "PWUnityManager.h"
#import "PWConstant.h"
#import "PWAdReward.h"

@interface PWRewardedAdUnitManager ()<PWFullScreenAdDelegate>

@property (nonatomic, copy) NSString *adUnit;
@property (nonatomic, strong) PWRewarded *rewardedAd;
@property (nonatomic, assign) BOOL isUsed;
@property (nonatomic, strong) NSDictionary<NSString *,NSString *> * targets;

@end

@implementation PWRewardedAdUnitManager

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

- (void)loadRewardedWithTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting
{
    self.rewardedAd = nil;
    self.isUsed = NO;
  
    UIViewController * viewController = [PWUnityManager unityViewController];
    self.rewardedAd = [[PWRewarded alloc] initWithAdUnitName:self.adUnit
                                              viewController:viewController
                                                    delegate:self];

    if (self.targets)
    {
      [self.rewardedAd.targeting add:self.targets];
    } else {
      [self.rewardedAd.targeting clear];
    }
    if (targeting) {
        PWLoadParams* params = [[PWLoadParams new] withTargeting:targeting];
        [self.rewardedAd loadWithParams:params];
    } else {
        [self.rewardedAd load];
    }
}

- (void)showRewarded
{
  if (![self.rewardedAd isLoaded]) {
    return;
  };

  [self.rewardedAd show];
}

- (BOOL)isRewardedReady {
  return [self.rewardedAd isLoaded] && !self.isUsed;
}

# pragma mark - PWFullScreenAdDelegate

- (void)fullScreenAdDidLoad:(PWFullScreenAd * _Nonnull)ad
{
  if ([ad isLoaded]) {
    NSString* message = [PWUnityMessageBuilder buildWithName:PW_Rewarded_Loaded_Event
                                                    adUnitId:self.adUnit];
    [PWUnityManager sendUnityMessage:message];
  }
}

- (void)fullScreenAdDidFailToLoad:(PWFullScreenAd * _Nonnull)ad
{
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_Rewarded_FailedToLoad_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdWillPresentFullScreenContent:(PWFullScreenAd * _Nonnull)ad
{
  self.isUsed = YES;
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_Rewarded_Opened_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdWillDismissFullScreenContent:(PWFullScreenAd * _Nonnull)ad {}

- (void)fullScreenAdDidDismissFullScreenContent:(PWFullScreenAd * _Nonnull)ad
{
  self.isUsed = YES;
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_Rewarded_Closed_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdDidFailToPresentFullScreenContent:(PWFullScreenAd * _Nonnull)ad
{
  self.isUsed = YES;
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_Rewarded_FailedToOpen_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdDidRecordImpression:(PWFullScreenAd * _Nonnull)ad
{
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_Rewarded_RecordedImpression_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdDidUserEarn:(PWFullScreenAd * _Nonnull)ad
                           type:(NSString * _Nonnull)type
                         amount:(double)amount
{
    PWAdReward *reward = [[PWAdReward alloc] initWithType:type amount:amount];
    NSString* message = [PWUnityMessageBuilder buildWithName:PW_Rewarded_Earned_Event
                                                    adUnitId:self.adUnit
                                                      object:reward];
    [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdDidRecordClick:(PWFullScreenAd * _Nonnull)ad 
{
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_Rewarded_Clicked_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

@end
