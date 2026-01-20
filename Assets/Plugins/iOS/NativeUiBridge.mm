#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

extern UIViewController* UnityGetGLViewController();
extern void UnitySendMessage(const char* obj, const char* method, const char* msg);

// --- HELPER CLASSES ---

// Handles clicks on list items to avoid C-function pointers mess
@interface AdItemHandler : NSObject
@property (nonatomic, copy) NSString* adUnitName;
- (void)onTap;
@end
@implementation AdItemHandler
- (void)onTap { UnitySendMessage("ScriptManager", "OnNativeSelection", [self.adUnitName UTF8String]); }
@end

@interface BackHandler : NSObject
+ (void)onTap;
@end
@implementation BackHandler
+ (void)onTap { UnitySendMessage("ScriptManager", "OnNativeBack", ""); }
@end

static UIView *currentOverlay = nil;
static UILabel *statusLabel = nil;
static NSMutableArray *handlers = nil; // Keep references alive

// --- C INTERFACE ---
extern "C" {

    void _ShowAdTypeList(const char* jsonList) {
        if (currentOverlay) [currentOverlay removeFromSuperview];
        handlers = [NSMutableArray array]; // Reset handlers
        
        UIViewController *vc = UnityGetGLViewController();
        currentOverlay = [[UIView alloc] initWithFrame:vc.view.bounds];
        currentOverlay.backgroundColor = [UIColor colorWithWhite:0.95 alpha:1.0]; // Light Gray

        // Parse JSON
        NSString *str = [NSString stringWithUTF8String:jsonList];
        NSData *data = [str dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary *json = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
        NSArray *items = json[@"items"];

        UIScrollView *scroll = [[UIScrollView alloc] initWithFrame:currentOverlay.bounds];
        CGFloat y = 60;

        for (NSDictionary *item in items) {
            NSString *key = item[@"key"];
            NSString *adUnit = item[@"adUnitName"];

            UIButton *btn = [UIButton buttonWithType:UIButtonTypeSystem];
            btn.frame = CGRectMake(0, y, vc.view.bounds.size.width, 50);
            [btn setTitle:key forState:UIControlStateNormal];
            btn.contentHorizontalAlignment = UIControlContentHorizontalAlignmentLeft;
            btn.titleEdgeInsets = UIEdgeInsetsMake(0, 20, 0, 0);
            btn.backgroundColor = [UIColor whiteColor];
            [btn setTitleColor:[UIColor blackColor] forState:UIControlStateNormal];
            
            // Maestro ID
            btn.accessibilityIdentifier = [NSString stringWithFormat:@"item_%@", adUnit];

            AdItemHandler *h = [[AdItemHandler alloc] init];
            h.adUnitName = adUnit;
            [handlers addObject:h];
            [btn addTarget:h action:@selector(onTap) forControlEvents:UIControlEventTouchUpInside];

            [scroll addSubview:btn];
            y += 51; // 1 pixel gap
        }
        
        scroll.contentSize = CGSizeMake(vc.view.bounds.size.width, y + 50);
        [currentOverlay addSubview:scroll];
        [vc.view addSubview:currentOverlay];
    }

    void _ShowDetailScreen(const char* title, const char* status) {
        if (currentOverlay) [currentOverlay removeFromSuperview];
        
        UIViewController *vc = UnityGetGLViewController();
        currentOverlay = [[UIView alloc] initWithFrame:vc.view.bounds];
        currentOverlay.backgroundColor = [UIColor whiteColor];

        // Back Button
        UIButton *back = [UIButton buttonWithType:UIButtonTypeSystem];
        back.frame = CGRectMake(20, 50, 80, 40);
        [back setTitle:@"< Back" forState:UIControlStateNormal];
        back.accessibilityIdentifier = @"btn_back";
        [back addTarget:[BackHandler class] action:@selector(onTap) forControlEvents:UIControlEventTouchUpInside];
        [currentOverlay addSubview:back];

        // Title
        UILabel *lblTitle = [[UILabel alloc] initWithFrame:CGRectMake(0, 100, vc.view.bounds.size.width, 30)];
        lblTitle.text = [NSString stringWithUTF8String:title];
        lblTitle.textAlignment = NSTextAlignmentCenter;
        lblTitle.font = [UIFont boldSystemFontOfSize:18];
        [currentOverlay addSubview:lblTitle];

        // Status
        statusLabel = [[UILabel alloc] initWithFrame:CGRectMake(0, 140, vc.view.bounds.size.width, 30)];
        statusLabel.text = [NSString stringWithFormat:@"Status: %s", status];
        statusLabel.textAlignment = NSTextAlignmentCenter;
        statusLabel.textColor = [UIColor darkGrayColor];
        statusLabel.accessibilityIdentifier = @"lbl_status";
        [currentOverlay addSubview:statusLabel];

        [vc.view addSubview:currentOverlay];
    }

    void _UpdateStatus(const char* status) {
        if (statusLabel) {
            statusLabel.text = [NSString stringWithFormat:@"Status: %s", status];
        }
    }
}