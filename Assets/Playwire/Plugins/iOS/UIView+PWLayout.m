//
//  Created by Intergi
//  Copyright © 2021 Intergi. All rights reserved.
//

#import "UIView+PWLayout.h"

@implementation UIView (PWLayout)

- (void)addSubview:(UIView *)subview atPosition:(PWAdPosition)position {
    subview.translatesAutoresizingMaskIntoConstraints = NO;
    [self addSubview:subview];

    NSMutableArray<NSLayoutConstraint *> *constraints = [[NSMutableArray alloc] init];

    switch (position) {
        case PWAdPositionTopLeft:
            [constraints addObjectsFromArray:@[
                [subview.leadingAnchor constraintEqualToAnchor:self.safeAreaLayoutGuide.leadingAnchor],
                [subview.topAnchor constraintEqualToAnchor:self.safeAreaLayoutGuide.topAnchor]
            ]];
            break;
        case PWAdPositionTopCenter:
            [constraints addObjectsFromArray:@[
                [subview.centerXAnchor constraintEqualToAnchor:self.centerXAnchor],
                [subview.topAnchor constraintEqualToAnchor:self.safeAreaLayoutGuide.topAnchor]
            ]];
            break;
        case PWAdPositionTopRight:
            [constraints addObjectsFromArray:@[
                [subview.trailingAnchor constraintEqualToAnchor:self.safeAreaLayoutGuide.trailingAnchor],
                [subview.topAnchor constraintEqualToAnchor:self.safeAreaLayoutGuide.topAnchor]
            ]];
            break;
        case PWAdPositionCenterLeft:
            [constraints addObjectsFromArray:@[
                [subview.leadingAnchor constraintEqualToAnchor:self.safeAreaLayoutGuide.leadingAnchor],
                [subview.centerYAnchor constraintEqualToAnchor:self.centerYAnchor]
            ]];
            break;
        case PWAdPositionCenter:
            [constraints addObjectsFromArray:@[
                [subview.centerXAnchor constraintEqualToAnchor:self.centerXAnchor],
                [subview.centerYAnchor constraintEqualToAnchor:self.centerYAnchor]
            ]];
            break;
        case PWAdPositionCenterRight:
            [constraints addObjectsFromArray:@[
                [subview.trailingAnchor constraintEqualToAnchor:self.safeAreaLayoutGuide.trailingAnchor],
                [subview.centerYAnchor constraintEqualToAnchor:self.centerYAnchor]
            ]];
            break;
        case PWAdPositionBottomLeft:
            [constraints addObjectsFromArray:@[
                [subview.leadingAnchor constraintEqualToAnchor:self.safeAreaLayoutGuide.leadingAnchor],
                [subview.bottomAnchor constraintEqualToAnchor:self.safeAreaLayoutGuide.bottomAnchor]
            ]];
            break;
        case PWAdPositionBottomCenter:
            [constraints addObjectsFromArray:@[
                [subview.centerXAnchor constraintEqualToAnchor:self.centerXAnchor],
                [subview.bottomAnchor constraintEqualToAnchor:self.safeAreaLayoutGuide.bottomAnchor]
            ]];
            break;
        case PWAdPositionBottomRight:
            [constraints addObjectsFromArray:@[
                [subview.trailingAnchor constraintEqualToAnchor:self.safeAreaLayoutGuide.trailingAnchor],
                [subview.bottomAnchor constraintEqualToAnchor:self.safeAreaLayoutGuide.bottomAnchor]
            ]];
            break;
    }

    [NSLayoutConstraint activateConstraints:constraints];
}

@end
