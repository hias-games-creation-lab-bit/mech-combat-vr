using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using UnityEditor;

namespace MechCombatVR.Tests
{
    public sealed class FoundationCreationGuardTests
    {
        private string directory;
        private string[] paths;
        private MethodInfo guard;

        [SetUp]
        public void SetUp()
        {
            directory = Path.Combine("Temp", "FoundationGuard-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(directory);
            paths = new[] { Path.Combine(directory, "Pipeline.asset"), Path.Combine(directory, "Renderer.asset"), Path.Combine(directory, "Scene.unity") };
            // The bootstrap remains in Unity's predefined Editor assembly.
            guard = Assembly.Load("Assembly-CSharp-Editor")
                .GetType("MechCombatVR.Editor.FoundationSetup", true)
                .GetMethod("EnsureTargetsAbsent", BindingFlags.Static | BindingFlags.NonPublic);
            Assert.That(guard, Is.Not.Null);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(directory))
                Directory.Delete(directory, true);
        }

        [TestCase(0, false)]
        [TestCase(1, false)]
        [TestCase(2, false)]
        [TestCase(0, true)]
        [TestCase(1, true)]
        [TestCase(2, true)]
        public void RejectsEachExistingTargetOrOrphanMetaWithoutWriting(int index, bool metaOnly)
        {
            string existing = paths[index] + (metaOnly ? ".meta" : "");
            File.WriteAllText(existing, "preserve existing content");
            var exception = Assert.Throws<TargetInvocationException>(() => guard.Invoke(null, new object[] { paths }));
            Assert.That(exception.InnerException, Is.TypeOf<InvalidOperationException>());
            Assert.That(File.ReadAllText(existing), Is.EqualTo("preserve existing content"));
            Assert.That(Directory.GetFiles(directory).Length, Is.EqualTo(1));
        }

        [Test]
        public void ExistingBuildSceneListIsRejectedAndPreserved()
        {
            var before = EditorBuildSettings.scenes;
            Assert.That(before, Is.Not.Empty, "Run against the existing foundation project.");
            var method = guard.DeclaringType.GetMethod("EnsureBuildSceneListEmpty", BindingFlags.Static | BindingFlags.NonPublic);
            var exception = Assert.Throws<TargetInvocationException>(() => method.Invoke(null, null));
            Assert.That(exception.InnerException, Is.TypeOf<InvalidOperationException>());
            var after = EditorBuildSettings.scenes;
            Assert.That(after.Length, Is.EqualTo(before.Length));
            for (int i = 0; i < before.Length; i++)
            {
                Assert.That(after[i].path, Is.EqualTo(before[i].path));
                Assert.That(after[i].enabled, Is.EqualTo(before[i].enabled));
            }
        }

        [Test]
        public void EmptyTargetsPassWithoutCreatingFiles()
        {
            guard.Invoke(null, new object[] { paths });
            Assert.That(Directory.GetFileSystemEntries(directory), Is.Empty);
        }
    }
}
