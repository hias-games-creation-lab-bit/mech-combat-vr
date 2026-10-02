using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace MechCombatVR.Editor
{
    /// <summary>Initial approved M0 project creation only; refuses to overwrite existing assets.</summary>
    public static class FoundationSetup
    {
        public const string PipelinePath = "Assets/Settings/QuestPipeline.asset";
        public const string RendererPath = "Assets/Settings/QuestRenderer.asset";
        public const string ScenePath = "Assets/Scenes/Foundation.unity";

        public static void Create()
        {
            if (Application.unityVersion != "6000.3.25f1")
                throw new InvalidOperationException("ADR-001 Editor mismatch.");
            if (AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(PipelinePath) != null)
                throw new InvalidOperationException("Foundation already exists; use validation, not recreation.");

            System.IO.Directory.CreateDirectory("Assets/Settings");
            System.IO.Directory.CreateDirectory("Assets/Scenes");
            AssetDatabase.Refresh();
            var renderer = ScriptableObject.CreateInstance<UniversalRendererData>();
            renderer.renderingMode = RenderingMode.Forward;
            AssetDatabase.CreateAsset(renderer, RendererPath);
            var pipeline = UniversalRenderPipelineAsset.Create(renderer);
            pipeline.supportsHDR = false;
            pipeline.msaaSampleCount = 4;
            AssetDatabase.CreateAsset(pipeline, PipelinePath);
            GraphicsSettings.defaultRenderPipeline = pipeline;
            for (int i = 0; i < QualitySettings.names.Length; i++)
            {
                QualitySettings.SetQualityLevel(i, false);
                QualitySettings.renderPipeline = pipeline;
            }

            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            var cameraObject = new GameObject("FoundationCamera", typeof(Camera), typeof(AudioListener));
            cameraObject.tag = "MainCamera";
            cameraObject.transform.position = new Vector3(0, 1.6f, -3);
            var camera = cameraObject.GetComponent<Camera>();
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(0.08f, 0.09f, 0.11f);
            camera.allowHDR = false;
            camera.GetUniversalAdditionalCameraData().renderPostProcessing = false;
            EditorSceneManager.SaveScene(scene, ScenePath);
            EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(ScenePath, true) };
            AssetDatabase.SaveAssets();
            Validate();
        }

        public static void Validate()
        {
            if (Application.unityVersion != "6000.3.25f1")
                throw new InvalidOperationException("ADR-001 Editor mismatch.");
            if (GraphicsSettings.defaultRenderPipeline is not UniversalRenderPipelineAsset)
                throw new InvalidOperationException("Default pipeline must be URP.");
            int originalQuality = QualitySettings.GetQualityLevel();
            try
            {
                for (int i = 0; i < QualitySettings.names.Length; i++)
                {
                    QualitySettings.SetQualityLevel(i, false);
                    var pipeline = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
                    if (pipeline == null)
                        throw new InvalidOperationException("A quality configuration has no URP pipeline.");
                    foreach (var data in pipeline.rendererDataList)
                    {
                        if (data is not UniversalRendererData renderer || renderer.renderingMode != RenderingMode.Forward)
                            throw new InvalidOperationException("Every configured renderer must use Forward.");
                    }
                    Debug.Log($"FOUNDATION_CONFIG quality={QualitySettings.names[i]} pipeline={pipeline.name} Forward=true");
                }
            }
            finally
            {
                QualitySettings.SetQualityLevel(originalQuality, false);
            }
            Debug.Log("FOUNDATION_CONFIG PASS Editor=6000.3.25f1");
        }
    }
}
