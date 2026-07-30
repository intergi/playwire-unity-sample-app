//
//  Created by Intergi
//  Copyright © 2022 Intergi. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <GoogleMobileAds/GoogleMobileAds.h>
#import <Playwire/Playwire-Swift.h>
#import <UnityFramework/UnityFramework-Swift.h>
#import "PWUnityPlugin.h"
#import "PWUnityManager.h"

#define NSSTRING(_CSTRING) ( (_CSTRING != NULL) ? [NSString stringWithCString:_CSTRING encoding:NSStringEncodingConversionAllowLossy] : nil)

#ifdef __cplusplus
extern "C" {
#endif

void _PlaywireInitializeSDK(const char *cPublisherId, const char *cAppId)
{
    NSString *publisherId = NSSTRING(cPublisherId);
    NSString *appId = NSSTRING(cAppId);

    [PlaywireSDK.shared initializeWithPublisherId:publisherId
                                            appId:appId
                                   viewController:[PWUnityManager unityViewController]
                                completionHandler:^() {
        onInitialize();
        NSString* message = [PWUnityMessageBuilder buildWithName:PW_SDK_Initialization_Event];
        [PWUnityManager sendUnityMessage:message];
    }];
}

void _PlaywireStartSDK(const char *cPublisherId, const char *cAppId)
{
    NSString *publisherId = NSSTRING(cPublisherId);
    NSString *appId = NSSTRING(cAppId);

    [PlaywireSDK.shared startWithPublisherId:publisherId
                                       appId:appId
                              viewController:[PWUnityManager unityViewController]
                                  completion:^(BOOL success, NSError * _Nullable error) {
        if (success) {
            onInitialize();
            NSDictionary<NSString *, NSString *> *parameters = @{ @"success" : @"true", @"error" : @"" };
            NSString *message = [PWUnityMessageBuilder buildWithName:PW_SDK_Start_Event
                                                            adUnitId:@""
                                                          parameters:parameters];
            [PWUnityManager sendUnityMessage:message];
        } else {
            NSString *errorMessage = error != nil ? error.localizedDescription : @"Unknown start error";
            NSDictionary<NSString *, NSString *> *parameters = @{ @"success" : @"false", @"error" : errorMessage };
            NSString *message = [PWUnityMessageBuilder buildWithName:PW_SDK_Start_Event
                                                            adUnitId:@""
                                                          parameters:parameters];
            [PWUnityManager sendUnityMessage:message];
        }
    }];
}

#ifdef __cplusplus
}
#endif
