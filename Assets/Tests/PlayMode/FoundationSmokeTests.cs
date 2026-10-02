using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace MechCombatVR.Tests
{
    public sealed class FoundationSmokeTests
    {
        [UnityTest]
        public IEnumerator FoundationSceneRunsWithUrpAndOneEnabledCamera()
        {
            yield return SceneManager.LoadSceneAsync("Foundation", LoadSceneMode.Single);
            yield return null;
            Assert.That(GraphicsSettings.currentRenderPipeline, Is.InstanceOf<UniversalRenderPipelineAsset>());
            Assert.That(Camera.allCamerasCount, Is.EqualTo(1));
            Assert.That(Camera.allCameras[0].isActiveAndEnabled, Is.True);
            for (int frame = 0; frame < 120; frame++)
                yield return null;
            LogAssert.NoUnexpectedReceived();
        }
    }
}
