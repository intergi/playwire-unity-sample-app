//
//  Created by Intergi
//  Copyright © 2021 Intergi. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface PWRewardedAdUnitManager : NSObject

- (instancetype)initWithAdUnit:(NSString *)adUnit;

- (void)setTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting;
- (void)loadRewardedWithTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting;
- (void)showRewarded;
- (BOOL)isRewardedReady;

@end

NS_ASSUME_NONNULL_END
