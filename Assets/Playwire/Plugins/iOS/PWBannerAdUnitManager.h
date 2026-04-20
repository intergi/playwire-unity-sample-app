//
//  Created by Intergi
//  Copyright © 2021 Intergi. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "PWAdPosition.h"

NS_ASSUME_NONNULL_BEGIN

@interface PWBannerAdUnitManager : NSObject

- (instancetype)initWithAdUnit:(NSString *)adUnit;

- (void)setTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting;
- (void)loadBannerAtPosition:(PWAdPosition)position withTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting;
- (void)showBanner;
- (void)hideBanner;
- (void)destroyBanner;
- (void)refreshBanner;

@end

NS_ASSUME_NONNULL_END
