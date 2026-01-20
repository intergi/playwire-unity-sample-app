// the approach was taken from the thread: https://stackoverflow.com/a/41527537/6245536
// credits to @stanislaw-pankevich and @norman-gray

#import "PWUnityTest.h"
#import <XCTest/XCTestObservationCenter.h>

// Define my Observation object -- I only have to do this in one place
@interface PWUnityTestRunnerObserver : NSObject<XCTestObservation>
@property (assign, nonatomic) NSUInteger testsFailed;
@property (assign, nonatomic) NSUInteger testsCalled;
@end

@implementation PWUnityTestRunnerObserver

- (instancetype)init {
    self = [super init];
    self.testsFailed = 0;
    return self;
}

// We can add various other functions here, to be informed about
// various events: see XCTestObservation at
// https://developer.apple.com/reference/xctest?language=objc
- (void)testSuiteWillStart:(XCTestSuite *)testSuite {
    NSString *log = [NSString stringWithFormat:@"suite %@...", [testSuite name]];
    [self printLog:log];
    self.testsCalled = 0;
}

- (void)testSuiteDidFinish:(XCTestSuite *)testSuite {
    NSString *log = [NSString stringWithFormat:@"...suite %@ (%tu tests)", [testSuite name], self.testsCalled];
    [self printLog:log];
}

- (void)testCaseWillStart:(XCTestSuite *)testCase {
    NSString *log = [NSString stringWithFormat:@"  test case: %@", [testCase name]];
    [self printLog:log];
    self.testsCalled++;
}

- (void)testCase:(XCTestCase *)testCase didFailWithDescription:(NSString *)description inFile:(NSString *)filePath atLine:(NSUInteger)lineNumber {
    NSString *log = [NSString stringWithFormat:@"  FAILED: %@, %@ (%@:%tu)", testCase, description, filePath, lineNumber];
    [self printLog:log];
    self.testsFailed++;
}

- (void)printLog:(NSString *)log {
    fprintf(stdout, "%s\n", [log UTF8String]);
}

@end

int main(int argc, char** argv) {
    XCTestObservationCenter *center = [XCTestObservationCenter sharedTestObservationCenter];
    PWUnityTestRunnerObserver *observer = [PWUnityTestRunnerObserver new];
    [center addTestObserver:observer];

    Class classes[] = { [PWUnityTest class], };  // add other classes here
    int nclasses = sizeof(classes)/sizeof(classes[0]);

    for (int i=0; i<nclasses; i++) {
        XCTestSuite *suite = [XCTestSuite testSuiteForTestCaseClass:classes[i]];
        [suite runTest];
    }

    int rval = 0;
    if (observer.testsFailed > 0) {
        NSString *log = [NSString stringWithFormat:@"runtests: %tu failures", observer.testsFailed];
        [observer printLog:log];
        rval = 1;
    }

    return rval;
}
