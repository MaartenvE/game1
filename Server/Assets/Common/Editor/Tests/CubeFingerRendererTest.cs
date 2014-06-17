using UnityEngine;
using NUnit.Framework;
using Moq;

namespace BuildingBlocks.CubeFinger
{
    [TestFixture]
    public class CubeFingerRendererTest
    {
        private Mock<IMaterial> materialMock;
        private Mock<IRenderer> rendererMock;
        private Mock<ITransform> transformMock;
        private Mock<IGameObject> gameObjectMock;
        private Mock<ICubeFinger> cubeFingerMock;
        private CubeFingerRenderer renderer;

        [SetUp]
        public void Setup()
        {
            materialMock = new Mock<IMaterial>();
            materialMock.SetupGet(m => m.color).Returns(Color.red);

            rendererMock = new Mock<IRenderer>();
            rendererMock.SetupGet(r => r.material).Returns(materialMock.Object);

            transformMock = new Mock<ITransform>();
            transformMock.SetupGet(t => t.localPosition).Returns(new Vector3(0, 0, .2f));
            transformMock.SetupGet(t => t.localScale).Returns(new Vector3(.2f, .2f, .2f));

            gameObjectMock = new Mock<IGameObject>();
            gameObjectMock.SetupGet(g => g.transform).Returns(transformMock.Object);
            gameObjectMock.SetupGet(g => g.renderer).Returns(rendererMock.Object);

            cubeFingerMock = new Mock<ICubeFinger>();
            cubeFingerMock.SetupGet(c => c.gameObject).Returns(gameObjectMock.Object);
            renderer = new CubeFingerRenderer(cubeFingerMock.Object);
        }

        [Test]
        public void TestNoneMoveFinger()
        {
            cubeFingerMock.SetupGet(f => f.Mode).Returns(CubeFingerMode.None);

            Vector3 displacement = Vector3.up;

            renderer.MoveFinger(gameObjectMock.Object, displacement);

            gameObjectMock.VerifyGet(g => g.renderer, Times.Never);
            transformMock.VerifySet(t => t.localPosition = It.IsAny<Vector3>(), Times.Never);
        }

        [Test]
        public void TestBuildMoveFinger()
        {
            cubeFingerMock.SetupGet(f => f.Mode).Returns(CubeFingerMode.Build);

            Vector3 displacement = Vector3.up;

            renderer.MoveFinger(gameObjectMock.Object, displacement);

            gameObjectMock.VerifyGet(g => g.renderer, Times.Never);
            transformMock.VerifySet(t => t.localPosition = new Vector3(0, .2f, .2f));
        }

        [Test]
        public void TestDeleteMoveFinger()
        {
            cubeFingerMock.SetupGet(f => f.Mode).Returns(CubeFingerMode.Delete);

            Vector3 displacement = Vector3.up;

            renderer.MoveFinger(gameObjectMock.Object, displacement);

            materialMock.VerifySet(m => m.color = new Color(1, 0, 0, .6f));
            rendererMock.VerifySet(r => r.enabled = false);
        }

        [Test]
        public void TestShowFinger()
        {
            renderer.ShowFinger(false);
            rendererMock.VerifySet(r => r.enabled = false);

            renderer.ShowFinger(true);
            renderer.ShowFinger(true);
            rendererMock.VerifySet(r => r.enabled = true, Times.Once());
        }

        [Test]
        public void TestSetColor()
        {
            Color color = Color.blue;

            renderer.SetColor(color);

            Assert.AreEqual(new Color(0, 0, 1, .6f), renderer.FingerColor);
            materialMock.VerifySet(m => m.color = new Color(0, 0, 1, .6f));
        }

        [Test]
        public void TestModeChanged()
        {
            cubeFingerMock.Raise(f => f.OnModeChanged += null, cubeFingerMock.Object, CubeFingerMode.None);

            rendererMock.VerifySet(r => r.enabled = false);
        }
    }
}
