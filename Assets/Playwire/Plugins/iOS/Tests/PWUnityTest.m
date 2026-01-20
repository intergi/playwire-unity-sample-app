#import "PWUnityTest.h"
#import "../PWConstant.h"

@implementation PWUnityTest

- (void)testBannerLoadedEventCheck
{
    XCTAssertEqual(PW_Banner_Loaded_Event, @"PW_OnBannerLoadedEvent");
}


// Uncomment code below to check failing test.

// - (void)testBannerLoadedEventCheckFail
// {
//     XCTAssertEqual(PW_Banner_Loaded_Event, @"PW_OnBanner");
// }

@end