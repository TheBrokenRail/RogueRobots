using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class Util {
	static public void Build() {
		BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
		buildPlayerOptions.scenes = new[] {"Assets/Scene.unity"};
		buildPlayerOptions.locationPathName = "Build/Win32/RogueRobots.exe";
		buildPlayerOptions.target = BuildTarget.StandaloneWindows;
		buildPlayerOptions.options = BuildOptions.None;
		BuildPipeline.BuildPlayer(buildPlayerOptions);
		buildPlayerOptions.locationPathName = "Build/Win64/RogueRobots.exe";
		buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
		BuildPipeline.BuildPlayer(buildPlayerOptions);
		buildPlayerOptions.locationPathName = "Build/Linux/RogueRobots";
		buildPlayerOptions.target = BuildTarget.StandaloneLinuxUniversal;
		BuildPipeline.BuildPlayer(buildPlayerOptions);
		EditorApplication.Exit
	}
}
