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
		Debug.Log("*** Started Windows 32bit Build! ***");
		BuildPipeline.BuildPlayer(buildPlayerOptions);
		Debug.Log("*** Finished Windows 32bit Build! ***");
		buildPlayerOptions.locationPathName = "Build/Win64/RogueRobots.exe";
		buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
		Debug.Log("*** Started Windows 64bit Build! ***");
		BuildPipeline.BuildPlayer(buildPlayerOptions);
		Debug.Log("*** Finished Windows 64bit Build! ***");
		buildPlayerOptions.locationPathName = "Build/Linux/RogueRobots";
		buildPlayerOptions.target = BuildTarget.StandaloneLinuxUniversal;
		Debug.Log("*** Started Linux Build! ***");
		BuildPipeline.BuildPlayer(buildPlayerOptions);
		Debug.Log("*** Finished Linux Build! ***");
	}
}
