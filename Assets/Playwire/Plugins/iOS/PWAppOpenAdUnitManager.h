//
//  Created by Intergi
//  Copyright © 2022 Intergi. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface PWAppOpenAdUnitManager : NSObject

@property (nonatomic, assign) BOOL autoReloadOnExpiration;

- (instancetype)initWithAdUnit:(NSString *)adUnit;

- (void)setTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting;
- (void)loadAppOpenAdWithTargeting:(NSDictionary<NSString *,NSString *> * _Nullable)targeting;
- (void)showAppOpenAd;
- (BOOL)isAppOpenAdReady;

@end

NS_ASSUME_NONNULL_END
