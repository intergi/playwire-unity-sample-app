#import "PWAdReward.h"

@interface PWAdReward()

@property (nonatomic, strong) NSString *type;
@property (nonatomic, assign) double amount;

@end

@implementation PWAdReward

- (instancetype)initWithType:(NSString *)type amount:(double)amount;
{
    self = [super init];
    if (self)
    {
        self.type = type;
        self.amount = amount;
    }
    return self;
}

- (NSDictionary<NSString *, NSString *> *)encodeToParameters
{
    return @{
        @"type": self.type,
        @"amount": [NSString stringWithFormat:@"%d", (int)self.amount]
    };
}

@end
