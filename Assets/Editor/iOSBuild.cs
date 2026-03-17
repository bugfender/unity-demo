using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class iOSBuild
{
    public static void Build()
    {
        BuildForTarget(BuildTarget.iOS, "Builds/iOS");
    }

    public static void BuildAndroid()
    {
        // Include ARM64 so the APK runs on 64-bit-only devices
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;
        BuildForTarget(BuildTarget.Android, "Builds/Android");
    }

    private static void BuildForTarget(BuildTarget target, string outputPath)
    {
        string[] scenes = new[]
        {
            "Assets/Scenes/Main.unity",
            "Assets/Scenes/Game.unity"
        };

        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = outputPath,
            target = target,
            options = BuildOptions.None
        };

        BuildReport report = BuildPipeline.BuildPlayer(options);
        if (report.summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"{target} build succeeded: {report.summary.totalSize} bytes");
        }
        else
        {
            Debug.LogError($"{target} build failed: {report.summary.result}");
            EditorApplication.Exit(report.summary.result == BuildResult.Failed ? 1 : 2);
        }
    }
}
