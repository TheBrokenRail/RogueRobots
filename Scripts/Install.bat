@echo off

set UNITY_HASH=fc1d3344e6ea
set UNITY_VERSION=2017.3.1f1

mkdir UnityDownload
cd UnityDownload

call :install UnitySetup64 Windows64EditorInstaller
call :install UnitySetup-Linux-Support-for-Editor-%UNITY_VERSION% TargetSupportInstaller
call :install UnitySetup-WebGL-Support-for-Editor-%UNITY_VERSION% TargetSupportInstaller
call :install UnitySetup-Mac-Support-for-Editor-%UNITY_VERSION% TargetSupportInstaller
exit /b

:install
echo Downloading %~1.exe
curl --retry 5 -O "https://netstorage.unity3d.com/unity/%UNITY_HASH%/%~2/%~1.exe"
echo Installing %~1.exe
%~1.exe /S
exit /b
