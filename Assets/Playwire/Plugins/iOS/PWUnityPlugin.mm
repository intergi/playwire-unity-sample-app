//
//  Created by Intergi
//  Copyright © 2021 Intergi. All rights reserved.
//

#include "math.h"
#import "PWUnityManager.h"
#import "PWAdPosition.h"
#import <PlaywireMobile/PlaywireMobile-Swift.h>
#import <Playwire-Swift.h>

#define NSSTRING(_CSTRING) ( (_CSTRING != NULL) ? [NSString stringWithCString:_CSTRING encoding:NSStringEncodingConversionAllowLossy] : nil)

@interface PWUnityPluginHelper : NSObject
@end

@implementation PWUnityPluginHelper

+(NSDictionary<NSString *, NSString *>*)decodeCustomTargetsFromString:(NSString *)targeting
{
    if (!targeting) return NULL;
    NSData *data = [targeting dataUsingEncoding:NSUTF8StringEncoding];

    NSError *error;
    NSDictionary *customTargets = [NSJSONSerialization JSONObjectWithData:data
                                                                  options:0
                                                                    error:&error];
    if (error || ![customTargets isKindOfClass:NSDictionary.class]) return NULL;
    return customTargets;
}

+ (PWAdPosition)position:(NSString *)position
{
    if ([position isEqualToString:@"TopLeft"]) {
        return PWAdPositionTopLeft;
    } else if ([position isEqualToString:@"TopCenter"]) {
        return PWAdPositionTopCenter;
    } else if ([position isEqualToString:@"TopRight"]) {
        return PWAdPositionTopRight;
    } else if ([position isEqualToString:@"CenterLeft"]) {
        return PWAdPositionCenterLeft;
    } else if ([position isEqualToString:@"Center"]) {
        return PWAdPositionCenter;
    } else if ([position isEqualToString:@"CenterRight"]) {
        return PWAdPositionCenterRight;
    } else if ([position isEqualToString:@"BottomLeft"]) {
        return PWAdPositionBottomLeft;
    } else if ([position isEqualToString:@"BottomCenter"]) {
        return PWAdPositionBottomCenter;
    } else if ([position isEqualToString:@"BottomRight"]) {
        return PWAdPositionBottomRight;
    } else {
        return PWAdPositionBottomCenter;
    }
}

+ (PWCMPType)cmpType:(NSString *)type
{
    if ([type isEqualToString:@"GoogleUMP"]) {
        return PWCMPTypeGoogleUmp;
    } else if ([type isEqualToString:@"AlreadyLaunched"]) {
        return PWCMPTypeAlreadyLaunched;
    } else if ([type isEqualToString:@"None"]) {
        return PWCMPTypeNone;
    } else {
        return PWCMPTypeGoogleUmp;
    }
}

+ (NSString*)cmpStringFrom:(PWCMPType)type
{
    switch (type) {
    case PWCMPTypeNone:
        return @"None";
        break;
    case PWCMPTypeAlreadyLaunched:
        return @"AlreadyLaunched";
        break;
    case PWCMPTypeGoogleUmp:
        return @"GoogleUMP";
        break;
    }
}

@end

#ifdef __cplusplus
extern "C" {
#endif
    static NSString *const LOGTAG = @"PW_UnityPlugin";
    static PWUnityManager *_unityManager;
    static bool _isUnityManagerInitialized = false;
    static bool _isInitialized = false;

    bool isInitialized()
    {
        return _isInitialized;
    }

    void initializeUnityManagerIfNeeded()
    {
        if (_isUnityManagerInitialized) {
            return;
        }
        _unityManager = [PWUnityManager shared];
        _isUnityManagerInitialized = true;
    }

    extern void onInitialize() {
        initializeUnityManagerIfNeeded();
        _isInitialized = true;
    }

    void _PlaywireStartConsoleLogger()
    {
        initializeUnityManagerIfNeeded();
        [_unityManager startConsoleLogger];
    }

    void _PlaywireSetGlobalTargeting(const char *cCustomTargets)
    {
        NSString *targeting = NSSTRING(cCustomTargets);
        NSDictionary<NSString *, NSString *>* customTargets = [PWUnityPluginHelper decodeCustomTargetsFromString:targeting];
        [_unityManager setGlobalTargeting:customTargets];
    }

    void _PlaywireSetBannerTargeting(const char *cAdUnitId, const char *cCustomTargets)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);
        NSString *targeting = NSSTRING(cCustomTargets);
        NSDictionary<NSString *, NSString *>* customTargets = [PWUnityPluginHelper decodeCustomTargetsFromString:targeting];

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireSetBannerTargeting: SDK is not initialized, banner targeting cannot be set.", LOGTAG);
            return;
        } 
        [_unityManager setBanner:adUnitId withTargeting:customTargets];
    }

    void _PlaywireLoadBanner(const char *cAdUnitId, const char *cPosition, const char *cCustomTargets)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);
        NSString *positionString = NSSTRING(cPosition);
        NSString *targeting = NSSTRING(cCustomTargets);
        NSDictionary<NSString *, NSString *>* customTargets = [PWUnityPluginHelper decodeCustomTargetsFromString:targeting];
        PWAdPosition adPosition = [PWUnityPluginHelper position:positionString];

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireLoadBanner: SDK is not initialized, banner cannot be loaded.", LOGTAG);
            return;
        } 
        [_unityManager loadBanner:adUnitId position:adPosition withTargeting:customTargets];
    }
    
    void _PlaywireShowBanner(const char *cAdUnitId)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireShowBanner: SDK is not initialized, banner is not loaded.", LOGTAG);
            return;
        } 
        [_unityManager showBanner:adUnitId];
    }

    void _PlaywireHideBanner(const char *cAdUnitId)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireHideBanner: SDK is not initialized, banner is not loaded.", LOGTAG);
            return;
        } 
        [_unityManager hideBanner:adUnitId];
    }

    void _PlaywireRefreshBanner(const char *cAdUnitId)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireRefreshBanner: SDK is not initialized, banner cannot be refreshed.", LOGTAG);
            return;
        }
        [_unityManager refreshBanner:adUnitId];
    }

    # pragma mark - Interstitials

    void _PlaywireSetInterstitialTargeting(const char *cAdUnitId, const char *cCustomTargets)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);
        NSString *targeting = NSSTRING(cCustomTargets);
        NSDictionary<NSString *, NSString *>* customTargets = [PWUnityPluginHelper decodeCustomTargetsFromString:targeting];

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireSetInterstitialTargeting: SDK is not initialized, interstitial targeting cannot be set.", LOGTAG);
            return;
        }
        [_unityManager setInterstitial:adUnitId withTargeting:customTargets];
    }

    void _PlaywireLoadInterstitial(const char *cAdUnitId, const char *cCustomTargets)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);
        NSString *targeting = NSSTRING(cCustomTargets);
        NSDictionary<NSString *, NSString *>* customTargets = [PWUnityPluginHelper decodeCustomTargetsFromString:targeting];

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireLoadInterstitial: SDK is not initialized, interstitial is not loaded.", LOGTAG);
            return;
        }
        [_unityManager loadInterstitial:adUnitId withTargeting:customTargets];
    }

    bool _PlaywireIsInterstitialReady(const char *cAdUnitId)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireIsInterstitialReady: SDK is not initialized, interstitial is not loaded.", LOGTAG);
            return false;
        }
       return  [_unityManager isInterstitialReady:adUnitId];
    }
    
    void _PlaywireShowInterstitial(const char *cAdUnitId)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireShowInterstitial: SDK is not initialized, interstitial is not loaded.", LOGTAG);
            return;
        }
        [_unityManager showInterstitial:adUnitId];
    }

    # pragma mark - Rewarded

    void _PlaywireSetRewardedTargeting(const char *cAdUnitId, const char *cCustomTargets)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);
        NSString *targeting = NSSTRING(cCustomTargets);
        NSDictionary<NSString *, NSString *>* customTargets = [PWUnityPluginHelper decodeCustomTargetsFromString:targeting];

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireSetRewardedTargeting: SDK is not initialized, rewarded targeting cannot be set.", LOGTAG);
            return;
        }
        [_unityManager setRewarded:adUnitId withTargeting:customTargets];
    }

    void _PlaywireLoadRewarded(const char *cAdUnitId, const char *cCustomTargets)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);
        NSString *targeting = NSSTRING(cCustomTargets);
        NSDictionary<NSString *, NSString *>* customTargets = [PWUnityPluginHelper decodeCustomTargetsFromString:targeting];

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireLoadRewarded: SDK is not initialized, rewarded is not loaded.", LOGTAG);
            return;
        }
        [_unityManager loadRewarded:adUnitId withTargeting:customTargets];
    }

    bool _PlaywireIsRewardedReady(const char *cAdUnitId)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireIsRewardedReady: SDK is not initialized, rewarded is not loaded.", LOGTAG);
            return false;
        }
       return  [_unityManager isRewardedReady:adUnitId];
    }

    void _PlaywireShowRewarded(const char *cAdUnitId)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireShowRewarded: SDK is not initialized, rewarded is not loaded.", LOGTAG);
            return;
        }
        [_unityManager showRewarded:adUnitId];
    }

    # pragma mark - App Open Ad

    void _PlaywireSetAppOpenAdTargeting(const char *cAdUnitId, const char *cCustomTargets)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);
        NSString *targeting = NSSTRING(cCustomTargets);
        NSDictionary<NSString *, NSString *>* customTargets = [PWUnityPluginHelper decodeCustomTargetsFromString:targeting];

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireSetAppOpenAdTargeting: SDK is not initialized, app open ad targeting cannot be set.", LOGTAG);
            return;
        }
        [_unityManager setAppOpenAd:adUnitId withTargeting:customTargets];
    }

    void _PlaywireLoadAppOpenAd(const char *cAdUnitId, const char *cCustomTargets)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);
        NSString *targeting = NSSTRING(cCustomTargets);
        NSDictionary<NSString *, NSString *>* customTargets = [PWUnityPluginHelper decodeCustomTargetsFromString:targeting];

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireLoadAppOpenAd: SDK is not initialized, app open ad is not loaded.", LOGTAG);
            return;
        }
        [_unityManager loadAppOpenAd:adUnitId withTargeting:customTargets];
    }

    bool _PlaywireIsAppOpenAdReady(const char *cAdUnitId)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireIsAppOpenAdReady: SDK is not initialized, app open ad is not loaded.", LOGTAG);
            return false;
        }
       return  [_unityManager isAppOpenAdReady:adUnitId];
    }
    
    void _PlaywireShowAppOpenAd(const char *cAdUnitId)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireShowAppOpenAd: SDK is not initialized, app open ad is not loaded.", LOGTAG);
            return;
        }
        [_unityManager showAppOpenAd:adUnitId];
    }

    void _PlaywireSetAppOpenAdReloadOnExpiration(const char *cAdUnitId, bool isEnabled)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireSetAppOpenAdReloadOnExpiration: SDK is not initialized.", LOGTAG);
            return;
        }
        [_unityManager setAppOpenAdReloadOnExpiration:adUnitId isEnabled:isEnabled];
    }

    bool _PlaywireGetAppOpenAdReloadOnExpiration(const char *cAdUnitId)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireGetAppOpenAdReloadOnExpiration: SDK is not initialized.", LOGTAG);
            return false;
        }
       return [_unityManager getAppOpenAdReloadOnExpiration:adUnitId];
    }

    # pragma mark - Rewarded Interstitial

    void _PlaywireSetRewardedInterstitialTargeting(const char *cAdUnitId, const char *cCustomTargets)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);
        NSString *targeting = NSSTRING(cCustomTargets);
        NSDictionary<NSString *, NSString *>* customTargets = [PWUnityPluginHelper decodeCustomTargetsFromString:targeting];

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireSetRewardedInterstitialTargeting: SDK is not initialized, rewarded interstitial targeting cannot not be set.", LOGTAG);
            return;
        }
        [_unityManager setRewardedInterstitial:adUnitId withTargeting:customTargets];
    }

    void _PlaywireLoadRewardedInterstitial(const char *cAdUnitId, const char *cCustomTargets)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);
        NSString *targeting = NSSTRING(cCustomTargets);
        NSDictionary<NSString *, NSString *>* customTargets = [PWUnityPluginHelper decodeCustomTargetsFromString:targeting];

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireLoadRewardedInterstitial: SDK is not initialized, rewarded interstitial cannot not loaded.", LOGTAG);
            return;
        }
        [_unityManager loadRewardedInterstitial:adUnitId withTargeting:customTargets];
    }

    bool _PlaywireIsRewardedInterstitialReady(const char *cAdUnitId)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireIsRewardedInterstitialReady: SDK is not initialized, rewarded interstiitial is not loaded.", LOGTAG);
            return false;
        }
       return  [_unityManager isRewardedInterstitialReady:adUnitId];
    }

    void _PlaywireShowRewardedInterstitial(const char *cAdUnitId)
    {
        NSString *adUnitId = NSSTRING(cAdUnitId);

        if (!_isInitialized) {
            NSLog(@"[%@] _PlaywireShowRewardedInterstitial: SDK is not initialized, rewarded interstiitial is not loaded.", LOGTAG);
            return;
        }
        [_unityManager showRewardedInterstitial:adUnitId];
    }

    void _PlaywireSetTestAds(bool isEnabled)
    {
        [_unityManager setTestAds:isEnabled];
    }

    bool _PlaywireGetTestAds()
    {
        return [_unityManager getTestAds];
    }

    void _PlaywireSetCMP(const char *cType)
    {
        NSString *typeString = NSSTRING(cType);
        PWCMPType type = [PWUnityPluginHelper cmpType:typeString];

        [_unityManager setCMP:type];
    }

    char* cStringCopy(const char* string)
    {
        if (string == NULL)
            return NULL;

        char* res = (char*)malloc(strlen(string) + 1);
        strcpy(res, string);

        return res;
    }

    const char* _PlaywireGetCMP()
    {
        PWCMPType type = [_unityManager getCMP];
        NSString *typeString = [PWUnityPluginHelper cmpStringFrom:type]; 
        return cStringCopy([typeString UTF8String]);
    }

#ifdef __cplusplus
}
#endif