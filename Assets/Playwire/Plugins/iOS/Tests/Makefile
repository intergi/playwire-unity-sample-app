# In order to properly use this Makefile please pass the argument scpecifying the path to the resulting log file
# For example:
# $ make LOG_PATH="iostestslog.text"

FRAMEWORKS=/Applications/Xcode.app/Contents/Developer/Platforms/MacOSX.platform/Developer/Library/Frameworks
TESTCASES=PWUnityTest

%.o: %.m
	clang -F$(FRAMEWORKS) -c $<

check: PWUnityTestRunner
	./PWUnityTestRunner 2>$(LOG_PATH)

PWUnityTestRunner: PWUnityTestRunner.o $(TESTCASES:=.o) 
	cc -o $@ $< -framework Cocoa -F$(FRAMEWORKS) -rpath $(FRAMEWORKS) \
        -framework XCTest $(TESTCASES:=.o)
