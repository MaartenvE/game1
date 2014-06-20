using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Moq;

namespace BuildingBlocks.Server
{
    [TestFixture]
    public class ServerTest
    {
        private Server server;
        private Mock<INetwork> network;
        private Mock<INetworkView> networkView;

        [SetUp]
        public void SetupServer()
        {
            network = new Mock<INetwork>();
            networkView = new Mock<INetworkView>();
            server = new Server(network.Object, networkView.Object);
        }

        [Test]
        public void TestLaunch()
        {
            int maxPlayers = 32;
            int port = 12345;
            bool useNAT = false;
            server.Launch(maxPlayers, port, useNAT);
            network.Verify(n => n.InitializeServer(maxPlayers, port, useNAT));
        }
    }
}
