//
//  Created by Intergi
//  Copyright © 2022 Intergi. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface PWRewardedInterstitialAdUnitManager : NSObject

- (instancetype)initWithAdUnit:(NSString *)adUnit;

- (void)setTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting;
- (void)loadRewardedInterstitialWithTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting;
- (void)showRewardedInterstitial;
- (BOOL)isRewardedInterstitialReady;

@end

NS_ASSUME_NONNULL_END