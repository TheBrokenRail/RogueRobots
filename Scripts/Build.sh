#!/bin/sh

PROJECT=RogueRobots

mkdir Build
mkdir Build/Windows32
mkdir Build/Windows64
mkdir Build/OSX

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

echo "Building $PROJECT for OSX 64bit"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/Unity.log \
  -projectPath $(pwd) \
  -buildOSX64Player "$(pwd)/Build/OSX/$PROJECT.app" \
  -stackTraceLogType Full \
  -quit

echo 'Zipping Builds'
mkdir GH-Pages
zip -r ./GH-Pages/Windows32.zip ./Build/Windows32/
zip -r ./GH-Pages/Windows64.zip ./Build/Windows64/
zip -r ./GH-Pages/OSX.zip ./Build/OSX/
