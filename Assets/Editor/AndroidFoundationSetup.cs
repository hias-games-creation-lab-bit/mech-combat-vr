using System;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Rendering;

namespace MechCombatVR.Editor
{
    public static class AndroidFoundationSetup
    {
        public static void Configure()
        {
            PlayerSettings.SetScriptingBackend(NamedBuildTarget.Android, ScriptingImplementation.IL2CPP);
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
            PlayerSettings.SetUseDefaultGraphicsAPIs(BuildTarget.Android, false);
            PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, new[] { GraphicsDeviceType.Vulkan });
            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel32;
            PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel34;
            AssetDatabase.SaveAssets();
            Validate();
        }

        public static void Validate()
        {
            FoundationSetup.Validate();
            Require(EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android, "Active target must be Android.");
            Require(PlayerSettings.GetScriptingBackend(NamedBuildTarget.Android) == ScriptingImplementation.IL2CPP, "IL2CPP required.");
            Require(PlayerSettings.Android.targetArchitectures == AndroidArchitecture.ARM64, "ARM64 only required.");
            Require(!PlayerSettings.GetUseDefaultGraphicsAPIs(BuildTarget.Android), "Automatic graphics selection must be off.");
            var apis = PlayerSettings.GetGraphicsAPIs(BuildTarget.Android);
            Require(apis.Length == 1 && apis[0] == GraphicsDeviceType.Vulkan, "Vulkan only required.");
            Require((int)PlayerSettings.Android.minSdkVersion == 32, "minSdk32 required.");
            Require((int)PlayerSettings.Android.targetSdkVersion == 34, "targetSdk34 required.");
            Debug.Log("ANDROID_FOUNDATION PASS target=Android backend=IL2CPP architecture=ARM64 graphics=Vulkan minSdk=32 targetSdk=34");
        }

        private static void Require(bool condition, string message)
        {
            if (!condition)
                throw new InvalidOperationException(message);
        }
    }
}
