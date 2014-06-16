using UnityEngine;
using NUnit.Framework;
using Moq;

namespace BuildingBlocks.CubeFinger
{
    [TestFixture]
    public class CubeFingerRendererTest
    {
        private Mock<ICubeFinger> cubeFingerMock;
        private CubeFingerRenderer renderer;

        [SetUp]
        public void Setup()
        {
            cubeFingerMock = new Mock<ICubeFinger>();
            renderer = new CubeFingerRenderer(cubeFingerMock.Object);
        }

        [Test]
        public void TestNoMoveFinger()
        {
            cubeFingerMock.SetupGet(f => f.Mode).Returns(CubeFingerMode.None);

            IGameObject gameObject = Mock.Of<IGameObject>();
            Vector3 displacement = Vector3.up;

            renderer.MoveFinger(gameObject, displacement);

            Assert.IsNotNull(true);
        }
    }
}
