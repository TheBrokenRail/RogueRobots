#!/bin/sh

UNITY_HASH=fc1d3344e6ea
UNITY_VERSION=2017.3.1f1

mkdir UnityDownload
cd UnityDownload

install () {
  echo 'Downloading '"$1"'.pkg'
  curl --retry 5 -O "https://netstorage.unity3d.com/unity/$UNITY_HASH/$2/$1.pkg"
  if [ $? -ne 0 ]
  then
    echo "Download failed"
    exit $?
  fi
  echo 'Installing'"$1"'.pkg'
  sudo installer -dumplog -package "$1.pkg" -target /
}

install Unity MacEditorInstaller
install UnitySetup-Linux-Support-for-Editor-$UNITY_VERSION MacEditorTargetInstaller
install UnitySetup-WebGL-Support-for-Editor-$UNITY_VERSION MacEditorTargetInstaller
install UnitySetup-Windows-Support-for-Editor-$UNITY_VERSION MacEditorTargetInstaller
