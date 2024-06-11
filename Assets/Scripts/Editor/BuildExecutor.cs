using UnityEditor;
using UnityEngine;

public static class BuildExecutor
{
    [MenuItem("Build/Build App")]
    public static void Build()
    {
        Debug.Log("Starting Build Process");

        var buildTarget = EditorUserBuildSettings.activeBuildTarget;
        Debug.Log("Current Build Target: " + buildTarget);

        var scenes = GetScenes();
        Debug.Log("Scenes to Build: " + string.Join(", ", scenes));

        var path = GetPath(buildTarget);
        Debug.Log("Build Path: " + path);

        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError("Invalid build path.");
            return;
        }

        var report = BuildPipeline.BuildPlayer(scenes, path, buildTarget, BuildOptions.Development);
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.Log("Build Succeeded: " + report.summary.totalSize + " bytes");
        }
        else
        {
            Debug.LogError("Build Failed");
        }
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