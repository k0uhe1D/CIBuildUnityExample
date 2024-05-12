using UnityEditor;
using UnityEngine;

public static class BuildExecutor
{

    [MenuItem("Build/Build App")]
    public static void Build()
    {
        var buildTarget = EditorUserBuildSettings.activeBuildTarget;
        var scenes = GetScenes();
        var path = GetPath(buildTarget);
        
        BuildPipeline.BuildPlayer(scenes, path, buildTarget, BuildOptions.Development);
    }

    private static string[] GetScenes()
    {
        return new[]
        {
            "Assets/Scenes/SampleScene.unity",
        };
    }

    private static string GetPath(BuildTarget target)
    {
        switch (target)
        {
            case BuildTarget.StandaloneWindows:
                return "Builds/Windows/MyGame.exe";
            case BuildTarget.StandaloneWindows64:
                return "Builds/Windows64/MyGame.exe";
            case BuildTarget.StandaloneOSX:
                return "Builds/OSX/MyGame.app";
            case BuildTarget.Android:
                return "Builds/Android/MyGame.apk";
            case BuildTarget.iOS:
                return "Builds/iOS/MyGame";
            default:
                Debug.LogError("Unsupported platform");
                EditorApplication.Exit(-1);
                return null;
        }
    }

}