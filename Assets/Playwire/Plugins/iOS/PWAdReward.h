//
//  Created by Intergi
//  Copyright © 2020 Intergi. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "PWUnityMessageBuilder.h"

NS_ASSUME_NONNULL_BEGIN

@interface PWAdReward : NSObject<ParametersEncodable>

- (instancetype)initWithType:(NSString *)type amount:(double)amount;

@end

NS_ASSUME_NONNULL_END