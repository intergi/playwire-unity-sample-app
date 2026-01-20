//
//  Created by Intergi
//  Copyright © 2021 Intergi. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface PWInterstitialAdUnitManager : NSObject

- (instancetype)initWithAdUnit:(NSString *)adUnit;

- (void)setTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting;
- (void)loadInterstitialWithTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting;
- (void)showInterstitial;
- (BOOL)isInterstitialReady;

@end

NS_ASSUME_NONNULL_END
