@echo off

set PROJECT=RogueRobots

mkdir Build
mkdir Build\Windows32
mkdir Build\Windows64
mkdir Build\Linux

echo Activating Unity
"C:\Program Files\Unity\Editor\Unity.exe" ^
  -nographics ^
  -silent-crashes ^
  -logFile %CD%/Unity.log ^
  -username "%USERNAME%" ^
  -password "%PASSWORD%" ^
  -quit
echo Log:
type Unity.log
echo End Log

echo Building %PROJECT% for Windows 32bit
"C:\Program Files\Unity\Editor\Unity.exe" ^
  -batchmode ^
  -nographics ^
  -silent-crashes ^
  -logFile %CD%/Unity.log ^
  -projectPath %CD% ^
  -buildWindowsPlayer "%CD%\Build\Windows32\%PROJECT%.exe" ^
  -quit
echo Log:
type Unity.log
echo End Log

echo Building %PROJECT% for Windows 64bit
"C:\Program Files\Unity\Editor\Unity.exe" ^
  -batchmode ^
  -nographics ^
  -silent-crashes ^
  -logFile %CD%/Unity.log ^
  -projectPath %CD% ^
  -buildWindows64Player "%CD%\Build\Windows64\%PROJECT%.exe" ^
  -quit
echo Log:
type Unity.log
echo End Log

echo Building %PROJECT% for Linux
"C:\Program Files\Unity\Editor\Unity.exe" ^
  -batchmode ^
  -nographics ^
  -silent-crashes ^
  -logFile %CD%/Unity.log ^
  -projectPath %CD% ^
  -buildLinuxUniversalPlayer "%CD%\Build\Linux\%PROJECT%" ^
  -quit
echo Log:
type Unity.log
echo End Log

echo Zipping Builds
7z a -r .\Build\Windows32.zip .\Build\Windows32\
7z a -r .\Build\Windows64.zip .\Build\Windows64\
7z a -r .\Build\Linux.zip .\Build\Linux\
