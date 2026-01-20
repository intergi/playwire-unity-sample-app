//
//  Created by Intergi
//  Copyright © 2021 Intergi. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "PWAdPosition.h"

NS_ASSUME_NONNULL_BEGIN

@interface UIView (PWLayout)

- (void)addSubview:(UIView *)subview atPosition:(PWAdPosition)position;

@end

NS_ASSUME_NONNULL_END
