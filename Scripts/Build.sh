#!/bin/sh

PROJECT=RogueRobots

mkdir Build
mkdir Build/Windows32
mkdir Build/Windows64
mkdir Build/Linux32
mkdir Build/Linux64

travis_wait 45

echo "Building $PROJECT for Windows 32bit"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile /dev/stdout \
  -projectPath $(pwd) \
  -buildWindowsPlayer "$(pwd)/Build/Windows32/$PROJECT.exe" \
  -quit

echo "Building $PROJECT for Windows 64bit"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile /dev/stdout \
  -projectPath $(pwd) \
  -buildWindows64Player "$(pwd)/Build/Windows64/$PROJECT.exe" \
  -quit

echo "Building $PROJECT for Linux 32bit"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile /dev/stdout \
  -projectPath $(pwd) \
  -buildLinux32Player "$(pwd)/Build/Linux32/$PROJECT" \
  -stackTraceLogType Full \
  -quit

echo "Building $PROJECT for Linux 64bit"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile /dev/stdout \
  -projectPath $(pwd) \
  -buildLinux64Player "$(pwd)/Build/Linux64/$PROJECT" \
  -quit

echo 'Zipping Builds'
zip -r ./Build/Windows32.zip ./Build/Windows32/
zip -r ./Build/Windows64.zip ./Build/Windows64/
zip -r ./Build/Linux32.zip ./Build/Linux32/
zip -r ./Build/Linux64.zip ./Build/Linux64/
