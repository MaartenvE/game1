using NUnit.Framework;
using Moq;
using UnityEngine;
using System.Linq;
using BuildingBlocks.Player;

namespace BuildingBlocks.Team
{
    [TestFixture]
    class TeamTest
    {
        private string name = "team name";
        private string target = "image target";

        private Mock<ITransform> transformMock;
        private Mock<IGameObject> gameObjectMock;
        private Mock<INetworkView> networkViewMock;
        private ITransform parentTransform;
        private ITeam team;

        [SetUp]
        public void Setup()
        {
            transformMock = new Mock<ITransform>();
            gameObjectMock = new Mock<IGameObject>();
            networkViewMock = new Mock<INetworkView>();
            parentTransform = Mock.Of<ITransform>();
            gameObjectMock.SetupGet(g => g.transform).Returns(transformMock.Object);
            gameObjectMock.SetupGet(g => g.networkView).Returns(networkViewMock.Object);
            gameObjectMock.Setup(g => g.Find(target)).Returns(Mock.Of<IGameObject>(g =>
                g.transform == parentTransform));
            team = new Team(gameObjectMock.Object);
        }

        [Test]
        public void CreateTeam()
        {
            Assert.IsInstanceOf<StructureTracker>(team.StructureTracker);
            Assert.AreEqual(0, team.Size);
        }

        [Test]
        public void MultipleTeams()
        {
            ITeam team2 = new Team(Mock.Of<IGameObject>());
            Assert.AreNotEqual(team.TeamId, team2.TeamId);
        }

        [Test]
        public void SetTarget()
        {
            team.Target = target;
            Assert.AreEqual(target, team.Target);
            transformMock.VerifySet(g => g.parent = parentTransform);
            transformMock.VerifySet(g => g.localPosition = Vector3.zero);
        }

        [Test]
        public void SendInfo()
        {
            team.Name = name;
            team.Target = target;
            team.SendInfo();
            networkViewMock.Verify(n => n.RPC("SetTeamInfo", RPCMode.OthersBuffered, team.TeamId, name, target));
        }

        [Test]
        public void SendProgressToPlayer()
        {
            INetworkPlayer player = Mock.Of<INetworkPlayer>();
            team.SendProgress(player);
            networkViewMock.Verify(n => n.RPC("SetTeamProgress", player, team.StructureTracker.Progress));
        }

        [Test]
        public void AddPlayer()
        {
            IPlayer player = Mock.Of<IPlayer>();

            team.AddPlayer(player);

            Mock.Get(player).VerifySet(p => p.Team = team);

            Assert.AreEqual(1, team.Size);
        }

        [Test]
        public void AddPlayerTeamList()
        {
            IPlayer player = Mock.Of<IPlayer>();

            team.AddPlayer(player);

            Assert.AreEqual(player, team.Players.First());
        }

        [Test]
        public void AddMultiplePlayers()
        {
            IPlayer player1 = Mock.Of<IPlayer>();
            IPlayer player2 = Mock.Of<IPlayer>();

            team.AddPlayer(player1);
            team.AddPlayer(player2);

            Assert.AreEqual(2, team.Size);
            Assert.IsTrue(team.Players.Contains(player1));
            Assert.IsTrue(team.Players.Contains(player2));
        }

        [Test]
        public void RemovePlayer()
        {
            IPlayer player = Mock.Of<IPlayer>();

            team.AddPlayer(player);
            team.RemovePlayer(player);

            Mock.Get(player).VerifySet(p => p.Team = null);

            Assert.AreEqual(0, team.Size);
        }

        [Test]
        public void RemovePlayerTeamList()
        {
            IPlayer player = Mock.Of<IPlayer>();

            team.AddPlayer(player);
            team.RemovePlayer(player);

            Assert.AreEqual(0, team.Size);
            Assert.IsFalse(team.Players.Contains(player));
        }

        [Test]
        public void RemoveMultiplePlayers()
        {
            IPlayer player1 = Mock.Of<IPlayer>();
            IPlayer player2 = Mock.Of<IPlayer>();
            IPlayer player3 = Mock.Of<IPlayer>();

            team.AddPlayer(player1);
            team.AddPlayer(player2);
            team.AddPlayer(player3);

            team.RemovePlayer(player1);
            team.RemovePlayer(player3);

            Assert.AreEqual(1, team.Size);
            Assert.IsFalse(team.Players.Contains(player1));
            Assert.IsTrue(team.Players.Contains(player2));
            Assert.IsFalse(team.Players.Contains(player3));
        }
    }
}
