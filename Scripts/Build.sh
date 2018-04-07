#!/bin/sh

project="RogueRobots"

echo "Building $PROJECT for Windows 32bit"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/Unity.log \
  -projectPath $(pwd) \
  -buildWindowsPlayer "$(pwd)/Build/Windows32/$PROJECT.exe" \
  -quit

echo "Building $PROJECT for Windows 64bit"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/Unity.log \
  -projectPath $(pwd) \
  -buildWindows64Player "$(pwd)/Build/Windows64/$PROJECT.exe" \
  -quit

echo "Building $PROJECT for Linux"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/Unity.log \
  -projectPath $(pwd) \
  -buildLinuxUniversalPlayer "$(pwd)/Build/Linux/$PROJECT" \
  -quit

echo 'Zipping Builds'
zip -r ./Build/Linux.zip ./Build/Linux/
zip -r ./Build/Windows32.zip ./Build/Windows32/
zip -r ./Build/Windows64.zip ./Build/Windows64/
