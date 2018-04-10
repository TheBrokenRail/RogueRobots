#!/bin/sh

PROJECT=RogueRobots

mkdir Build
mkdir Build/Windows32
mkdir Build/Windows64
mkdir Build/OSX
mkdir Build/Linux
mkdir GH-Pages

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

echo "Building $PROJECT for OSX"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile /dev/stdout \
  -projectPath $(pwd) \
  -buildOSXUniversalPlayer "$(pwd)/Build/OSX/$PROJECT.app" \
  -quit

echo "Building $PROJECT for Linux"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile /dev/stdout \
  -projectPath $(pwd) \
  -buildLinuxUniversalPlayer "$(pwd)/Build/Linux/$PROJECT" \
  -quit

echo 'Zipping Builds'
zip -r ./GH-Pages/Windows32.zip ./Build/Windows32/*
zip -r ./GH-Pages/Windows64.zip ./Build/Windows64/*
zip -r ./GH-Pages/OSX.zip ./Build/OSX/*
zip -r ./GH-Pages/Linux.zip ./Build/Linux/*
