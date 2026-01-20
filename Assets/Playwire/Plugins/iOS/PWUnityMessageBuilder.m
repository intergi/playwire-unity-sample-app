//
//  Created by Intergi
//  Copyright © 2021 Intergi. All rights reserved.
//

#import "PWUnityMessageBuilder.h"


@interface PWUnityMessageBuilder ()
@property (strong, nonatomic) NSString *name;
@property (strong, nonatomic) NSString *adUnitId;
@property (strong, nonatomic) NSDictionary<NSString *, NSString *> * _Nullable parameters;
@end

@implementation PWUnityMessageBuilder

# pragma mark - Public Interface -

+ (NSString *)buildWithName:(NSString *)name
{
  PWUnityMessageBuilder *builder = [[PWUnityMessageBuilder alloc] initWithName:name
                                                                      adUnitId:nil
                                                                    parameters:nil];
  return [builder buildFormattedMessage];
}

+ (NSString *)buildWithName:(NSString *)name
                   adUnitId:(NSString *)adUnitId
{
  PWUnityMessageBuilder *builder = [[PWUnityMessageBuilder alloc] initWithName:name
                                                                      adUnitId:adUnitId
                                                                    parameters:nil];
  return [builder buildFormattedMessage];
}

+ (NSString *)buildWithName:(NSString *)name
                   adUnitId:(NSString *)adUnitId
                 parameters:(NSDictionary<NSString *, NSString *> *)parameters
{
  PWUnityMessageBuilder *builder = [[PWUnityMessageBuilder alloc] initWithName:name
                                                                      adUnitId:adUnitId
                                                                    parameters:parameters];
  return [builder buildFormattedMessage];

}

+ (NSString *)buildWithName:(NSString *)name
                   adUnitId:(NSString *)adUnitId
                     object:(id<ParametersEncodable>)object
{
  NSDictionary<NSString *, NSString *> *parameters = [object encodeToParameters];
  PWUnityMessageBuilder *builder = [[PWUnityMessageBuilder alloc] initWithName:name
                                                                      adUnitId:adUnitId
                                                                    parameters:parameters];
  return [builder buildFormattedMessage];
}

# pragma mark - Private Interface -

- (instancetype)initWithName:(NSString *)name
                    adUnitId:(NSString *)adUnitId
                  parameters:(NSDictionary<NSString *, NSString *> *)parameters
{
  self = [super init];
  if (self) {
    self.name = name;
    self.adUnitId = adUnitId;
    self.parameters = parameters;
  }
  return self;
}

-(NSString *)buildFormattedMessage
{
  if(self.name.length == 0)
  {
    return @"";
  }
  
  NSMutableDictionary *eventAttributes = [[NSMutableDictionary alloc] init];
  eventAttributes[@"name"] = self.name;
  eventAttributes[@"adUnitId"] = self.adUnitId;
  
  if (self.parameters.count != 0)
  {
    NSString *jsonString = [self encodeDictionaryToJSONString:self.parameters];
    NSData *jsonData = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
    NSString *base64JsonString = [jsonData base64EncodedStringWithOptions:0];
    eventAttributes[@"parameters"] = base64JsonString;
  }
  
  return [self encodeDictionaryToJSONString:eventAttributes];
}

- (NSString*)encodeDictionaryToJSONString:(NSDictionary *)parameters
{
  NSError *error;
  NSData *data = [NSJSONSerialization dataWithJSONObject:parameters
                                                 options:0
                                                   error:&error];
  
  if(error)
  {
    return @"";
  }
  return [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
}

@end
