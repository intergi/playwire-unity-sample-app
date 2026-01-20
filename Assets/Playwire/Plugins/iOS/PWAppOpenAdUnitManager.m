//
//  Created by Intergi
//  Copyright © 2022 Intergi. All rights reserved.
//

#import "PWAppOpenAdUnitManager.h"
#import <GoogleMobileAds/GoogleMobileAds.h>
#import <PlaywireMobile/PlaywireMobile-Swift.h>
#import <Playwire-Swift.h>
#import "PWUnityManager.h"
#import "PWConstant.h"

@interface PWAppOpenAdUnitManager()<PWFullScreenAdDelegate>

@property (nonatomic, copy) NSString *adUnit;
@property (nonatomic, strong) PWAppOpenAd *appOpenAd;
@property (nonatomic, assign) BOOL isUsed;
@property (nonatomic, strong) NSDictionary<NSString *,NSString *> * targets;

@end

@implementation PWAppOpenAdUnitManager

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

- (void)loadAppOpenAdWithTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting
{
    self.appOpenAd = nil;
    self.isUsed = NO;
  
    UIViewController * viewController = [PWUnityManager unityViewController];
    self.appOpenAd = [[PWAppOpenAd alloc] initWithAdUnitName:self.adUnit
                                              viewController:viewController
                                                    delegate:self];
    self.appOpenAd.autoReloadOnExpiration = self.autoReloadOnExpiration;
    if (self.targets)
    {
      [self.appOpenAd.targeting add:self.targets];
    } else {
      [self.appOpenAd.targeting clear];
    }

    if (targeting) {
        PWLoadParams* params = [[PWLoadParams new] withTargeting:targeting];
        [self.appOpenAd loadWithParams:params];
    } else {
        [self.appOpenAd load];
    }
}

- (void)showAppOpenAd
{
    if (![self.appOpenAd isLoaded]) {      
      return;
    };

    [self.appOpenAd show];
}

- (BOOL)isAppOpenAdReady 
{
  return [self.appOpenAd isLoaded] && !self.isUsed;
}

- (void)setAppOpenAdReloadOnExpiration:(BOOL)isEnabled
{
    _autoReloadOnExpiration = isEnabled;
    self.appOpenAd.autoReloadOnExpiration = isEnabled;
}

# pragma mark - PWFullScreenAdDelegate

- (void)fullScreenAdDidLoad:(PWFullScreenAd * _Nonnull)ad
{
  if ([ad isLoaded]) 
  {
    NSString* message = [PWUnityMessageBuilder buildWithName:PW_AppOpenAd_Loaded_Event
                                                    adUnitId:self.adUnit];
    [PWUnityManager sendUnityMessage:message];
  }
}

- (void)fullScreenAdDidFailToLoad:(PWFullScreenAd * _Nonnull)ad
{
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_AppOpenAd_FailedToLoad_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdWillPresentFullScreenContent:(PWFullScreenAd * _Nonnull)ad
{
  self.isUsed = YES;
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_AppOpenAd_Opened_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdWillDismissFullScreenContent:(PWFullScreenAd * _Nonnull)ad {}

- (void)fullScreenAdDidDismissFullScreenContent:(PWFullScreenAd * _Nonnull)ad
{
  self.isUsed = YES;
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_AppOpenAd_Closed_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdDidFailToPresentFullScreenContent:(PWFullScreenAd * _Nonnull)ad
{
  self.isUsed = YES;
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_AppOpenAd_FailedToOpen_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdDidRecordImpression:(PWFullScreenAd * _Nonnull)ad
{
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_AppOpenAd_RecordedImpression_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdDidRecordClick:(PWFullScreenAd * _Nonnull)ad 
{
  NSString* message = [PWUnityMessageBuilder buildWithName:PW_AppOpenAd_Clicked_Event
                                                  adUnitId:self.adUnit];
  [PWUnityManager sendUnityMessage:message];
}

- (void)fullScreenAdDidUserEarn:(PWFullScreenAd * _Nonnull)ad
                           type:(NSString * _Nonnull)type
                         amount:(double)amount {}
@end