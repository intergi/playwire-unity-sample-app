//
//  Created by Intergi
//  Copyright © 2021 Intergi. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@protocol ParametersEncodable <NSObject>

-(NSDictionary<NSString *, NSString *> *)encodeToParameters;
@end

@interface PWUnityMessageBuilder: NSObject

- (instancetype)init NS_UNAVAILABLE;

+ (NSString *)buildWithName:(NSString *)name;

+ (NSString *)buildWithName:(NSString *)name
                   adUnitId:(NSString *)adUnitId;

+ (NSString *)buildWithName:(NSString *)name
                   adUnitId:(NSString *)adUnitId
                 parameters:(NSDictionary<NSString *, NSString *> *)parameters;

+ (NSString *)buildWithName:(NSString *)name
                   adUnitId:(NSString *)adUnitId
                     object:(id<ParametersEncodable>)object;

@end

NS_ASSUME_NONNULL_END
