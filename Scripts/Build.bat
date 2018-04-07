@echo off

set PROJECT=RogueRobots

mkdir Build
mkdir Build\Windows32
mkdir Build\Windows64
mkdir Build\Linux

echo Building %PROJECT% for Windows 32bit
C:\Program Files\Unity\Editor\Unity ^
  -batchmode ^
  -nographics ^
  -silent-crashes ^
  -logFile %CD%/Unity.log ^
  -projectPath %CD% ^
  -buildWindowsPlayer "%CD%\Build\Windows32\%PROJECT%.exe" ^
  -quit

echo Building %PROJECT% for Windows 64bit
C:\Program Files\Unity\Editor\Unity ^
  -batchmode ^
  -nographics ^
  -silent-crashes ^
  -logFile %CD%/Unity.log ^
  -projectPath %CD% ^
  -buildWindows64Player "%CD%\Build\Windows64\%PROJECT%.exe" ^
  -quit

echo Building %PROJECT% for Linux
C:\Program Files\Unity\Editor\Unity ^
  -batchmode ^
  -nographics ^
  -silent-crashes ^
  -logFile %CD%/Unity.log ^
  -projectPath %CD% ^
  -buildLinuxUniversalPlayer "%CD%\Build\Linux\%PROJECT%" ^
  -quit

echo Zipping Builds
zip -r .\Build\Windows32.zip .\Build\Windows32\
zip -r .\Build\Windows64.zip .\Build\Windows64\
zip -r .\Build\Linux.zip .\Build\Linux\
