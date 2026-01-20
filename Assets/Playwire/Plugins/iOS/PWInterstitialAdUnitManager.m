//
//  Created by Intergi
//  Copyright © 2021 Intergi. All rights reserved.
//

#import "PWInterstitialAdUnitManager.h"
#import <GoogleMobileAds/GoogleMobileAds.h>
#import <PlaywireMobile/PlaywireMobile-Swift.h>
#import <Playwire-Swift.h>
#import "PWUnityManager.h"
#import "PWConstant.h"


@interface PWInterstitialAdUnitManager()<PWFullScreenAdDelegate>

@property (nonatomic, copy) NSString *adUnit;
@property (nonatomic, strong) PWInterstitial *interstitial;
@property (nonatomic, assign) BOOL isUsed;
@property (nonatomic, strong) NSDictionary<NSString *,NSString *> * targets;

@end

@implementation PWInterstitialAdUnitManager

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

- (void)loadInterstitialWithTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting
{
    self.interstitial = nil;
    self.isUsed = NO;
  
    UIViewController * viewController = [PWUnityManager unityViewController];
    self.interstitial = [[PWInterstitial alloc] initWithAdUnitName:self.adUnit
                                                    viewController:viewController
                                                          delegate:self];
    
    if (self.targets)
    {
      [self.interstitial.targeting add:self.targets];
    } else {
      [self.interstitial.targeting clear];
    }
    if (targeting) {
        PWLoadParams* params = [[PWLoadParams new] withTargeting:targeting];
        [self.interstitial loadWithParams:params];
    } else {
        [self.interstitial load];
    }
}

- (void)showInterstitial
{
    if (![self.interstitial isLoaded]) {      
      return;
    };

    [self.interstitial show];
}

- (BOOL)isInterstitialReady {
  return [self.interstitial isLoaded] && !self.isUsed;
}

# pragma mark - PWFullScreenAdDelegate

- (void)fullScreenAdDidLoad:(PWFullScreenAd * _Nonnull)ad
{
  if ([ad isLoaded]) {
    NSString* message = [PWUnityMessageBuilder buildWithName:PW_Interstitial_Loaded_Event
                                                    adUnitId:self.adUnit];
    [PWUnityManager sendUnityMessage:message];
  }
}

- (void)fullScreenAdDidFailToLoad:(PWFullScreenAd * _Nonnull)ad
{
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_Interstitial_FailedToLoad_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdWillPresentFullScreenContent:(PWFullScreenAd * _Nonnull)ad
{
  self.isUsed = YES;
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_Interstitial_Opened_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdWillDismissFullScreenContent:(PWFullScreenAd * _Nonnull)ad {}

- (void)fullScreenAdDidDismissFullScreenContent:(PWFullScreenAd * _Nonnull)ad
{
  self.isUsed = YES;
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_Interstitial_Closed_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdDidFailToPresentFullScreenContent:(PWFullScreenAd * _Nonnull)ad
{
  self.isUsed = YES;
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_Interstitial_FailedToOpen_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdDidRecordImpression:(PWFullScreenAd * _Nonnull)ad
{
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_Interstitial_RecordedImpression_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdDidRecordClick:(PWFullScreenAd * _Nonnull)ad
{
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_Interstitial_Clicked_Event
                                                adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdDidUserEarn:(PWFullScreenAd * _Nonnull)ad
                           type:(NSString * _Nonnull)type
                         amount:(double)amount {}
@end