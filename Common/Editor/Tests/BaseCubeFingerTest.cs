using UnityEngine;
using NUnit.Framework;
using Moq;

namespace BuildingBlocks.CubeFinger
{
    [TestFixture]
    public class BaseCubeFingerTest
    {
        private Mock<IGameObject> gameObjectMock;
        private Mock<ICubeFingerRenderer> rendererMock;
        private BaseCubeFinger finger;

        [SetUp]
        public void Setup()
        {
            gameObjectMock = new Mock<IGameObject>();
            rendererMock = new Mock<ICubeFingerRenderer>();
            finger = new BaseCubeFinger(gameObjectMock.Object, rendererMock.Object);
        }

        [Test]
        public void TestCreateFinger()
        {
            Assert.AreSame(rendererMock.Object, finger.Renderer);
            Assert.AreEqual(CubeFingerMode.Build, finger.Mode);
            Assert.IsFalse(finger.IsMine);
            rendererMock.Verify(r => r.ShowFinger(false));
        }

        [Test]
        public void TestModeChange()
        {
            CubeFingerMode expected = CubeFingerMode.Delete;
            CubeFingerMode actual = CubeFingerMode.None;

            finger.OnModeChanged += (object sender, CubeFingerMode mode) => actual = mode;

            finger.Mode = expected;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestNoModeChange()
        {
            CubeFingerMode value = CubeFingerMode.Delete;
            CubeFingerMode actual = CubeFingerMode.None;

            finger.OnModeChanged += (object sender, CubeFingerMode mode) => actual = mode;
            finger.Mode = value;

            actual = CubeFingerMode.None;

            finger.Mode = value;

            Assert.AreEqual(CubeFingerMode.None, actual);
        }

        [Test]
        public void TestRPC_SetPersonalFinger()
        {
            Assert.IsFalse(finger.IsMine);
            finger.RPC_SetPersonalFinger();
            Assert.IsTrue(finger.IsMine);
        }

        // todo: Test SetFingerParent

        [Test]
        public void TestRPC_SetFingerMode()
        {
            CubeFingerMode mode = CubeFingerMode.Delete;
            finger.RPC_SetFingerMode((int) mode);
            Assert.AreEqual(mode, finger.Mode);
        }
    }
}
