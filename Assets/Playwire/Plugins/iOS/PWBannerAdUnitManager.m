//
//  Created by Intergi
//  Copyright © 2021 Intergi. All rights reserved.
//

#import "PWBannerAdUnitManager.h"
#import <GoogleMobileAds/GoogleMobileAds.h>
#import <PlaywireMobile/PlaywireMobile-Swift.h>
#import <Playwire-Swift.h>
#import "PWUnityManager.h"
#import "UIView+PWLayout.h"
#import "PWConstant.h"

@interface PWBannerAdUnitManager() <PWViewAdDelegate>

@property (nonatomic, copy) NSString *adUnit;

@property (nonatomic, strong) PWBannerView *bannerView;
@property (nonatomic, strong) NSDictionary<NSString *,NSString *> * targets;

@end

@implementation PWBannerAdUnitManager

- (instancetype)initWithAdUnit:(NSString *)adUnit 
{
    self = [super init];
    if (self)
    {
        self.adUnit = adUnit;
    }
    return self;
}

- (void)setTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting
{
  self.targets = targeting;
}

- (void)loadBannerAtPosition:(PWAdPosition)position withTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting
{
    if (self.bannerView) {
        self.bannerView.hidden = YES;
        [self.bannerView removeFromSuperview];
    }

    self.bannerView = [[PWBannerView alloc] initWithAdUnitName:self.adUnit
                                                      delegate:self];
    
    if (self.targets)
    {
      [self.bannerView.targeting add:self.targets];
    } else {
      [self.bannerView.targeting clear];
    }
    if (targeting) {
        PWLoadParams* params = [[PWLoadParams new] withTargeting:targeting];
        [self.bannerView loadWithParams:params];
    } else {
        [self.bannerView load];
    }

    self.bannerView.hidden = YES;
    [[PWUnityManager unityViewController].view addSubview:self.bannerView
                                               atPosition:position];
}

- (void)showBanner
{
    if (!self.bannerView) return;

    self.bannerView.hidden = NO;
}

- (void)hideBanner
{
    if (!self.bannerView) return;

    self.bannerView.hidden = YES;
}

- (void)destroyBanner
{
    if (!self.bannerView) return;

    [self.bannerView destroy];
}

- (void)refreshBanner
{
    if (!self.bannerView) return;
    [self.bannerView refresh];
}

/// PWViewAdDelegate

- (void)viewAdDidLoad:(PWViewAd * _Nonnull)ad
{
    NSString* message = [PWUnityMessageBuilder buildWithName:PW_Banner_Loaded_Event
                                                    adUnitId:self.adUnit];
    [PWUnityManager sendUnityMessage:message];
}

- (void)viewAdDidFailToLoad:(PWViewAd * _Nonnull)ad
{
    NSString* message = [PWUnityMessageBuilder buildWithName:PW_Banner_FailedToLoad_Event
                                                    adUnitId:self.adUnit];
    [PWUnityManager sendUnityMessage:message];
}

- (void)viewAdWillPresentFullScreenContent:(PWViewAd * _Nonnull)ad {
    NSString* message = [PWUnityMessageBuilder buildWithName:PW_Banner_Opened_Event
                                                    adUnitId:self.adUnit];
    [PWUnityManager sendUnityMessage:message];
}

- (void)viewAdWillDismissFullScreenContent:(PWViewAd * _Nonnull)ad {}

- (void)viewAdDidDismissFullScreenContent:(PWViewAd * _Nonnull)ad {
    NSString* message = [PWUnityMessageBuilder buildWithName:PW_Banner_Closed_Event
                                                    adUnitId:self.adUnit];
    [PWUnityManager sendUnityMessage:message];
}

- (void)viewAdDidRecordClick:(PWViewAd * _Nonnull)ad
{
    NSString* message = [PWUnityMessageBuilder buildWithName:PW_Banner_Clicked_Event
                                                    adUnitId:self.adUnit];
    [PWUnityManager sendUnityMessage:message];
}

- (void)viewAdDidRecordImpression:(PWViewAd * _Nonnull)ad {
    NSString* message = [PWUnityMessageBuilder buildWithName:PW_Banner_RecordedImpression_Event
                                                    adUnitId:self.adUnit];
    [PWUnityManager sendUnityMessage:message];
}

@end
