//
//  Created by Intergi
//  Copyright © 2022 Intergi. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <GoogleMobileAds/GoogleMobileAds.h>
#import <PlaywireMobile/PlaywireMobile-Swift.h>
#import <Playwire-Swift.h>
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
#ifdef __cplusplus
}
#endif