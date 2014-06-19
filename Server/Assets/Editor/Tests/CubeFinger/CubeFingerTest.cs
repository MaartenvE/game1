using NUnit.Framework;
using Moq;
using UnityEngine;

namespace BuildingBlocks.CubeFinger
{
    [TestFixture]
    class CubeFingerTest
    {
        private IGameObject gameObject;
        private Mock<IMaterial> materialMock;
        private Mock<IRenderer> rendererMock;
        private Mock<INetwork> networkMock;
        private Mock<INetworkView> networkViewMock;

        private CubeFinger finger;

        [SetUp]
        public void Setup()
        {
            materialMock = new Mock<IMaterial>();
            materialMock.SetupGet(m => m.color).Returns(Color.red);

            rendererMock = new Mock<IRenderer>();
            rendererMock.SetupGet(r => r.material).Returns(materialMock.Object);

            networkMock = new Mock<INetwork>();
            networkViewMock = new Mock<INetworkView>();

            gameObject = Mock.Of<IGameObject>(g =>
                g.renderer == rendererMock.Object && 
                g.network == networkMock.Object &&
                g.networkView == networkViewMock.Object);

            finger = new CubeFinger(gameObject);
        }

        [Test]
        public void TestSetParent()
        {
            string parent = "parent";
            finger.SetParent(parent);
            networkViewMock.Verify(n => n.RPC("SetFingerParent", RPCMode.AllBuffered, parent));
        }

        [Test]
        public void TestSetPlayer()
        {
            INetworkPlayer player = Mock.Of<INetworkPlayer>();
            finger.SetPlayer(player);
            networkViewMock.Verify(n => n.RPC("SetPersonalFinger", player));
        }

        [Test]
        public void TestOnPlayerConnected()
        {
            INetworkPlayer player = Mock.Of<INetworkPlayer>();
            finger.Mode = CubeFingerMode.Delete;
            finger.Renderer.FingerColor = Color.green;
            finger.OnPlayerConnected(player);
            networkViewMock.Verify(n => n.RPC("SetFingerMode", player, (int) CubeFingerMode.Delete));
            networkViewMock.Verify(n => n.RPC("ColorFinger", player, new Vector3(0, 1, 0)));
        }

        [Test]
        public void TestDestroy()
        {
            NetworkViewID viewId = new NetworkViewID();
            networkViewMock.SetupGet(n => n.viewID).Returns(viewId);

            finger.Destroy();
            networkMock.Verify(n => n.RemoveRPCs(viewId));
            networkMock.Verify(n => n.Destroy(viewId));
        }
    }
}