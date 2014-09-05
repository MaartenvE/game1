using UnityEngine;
using NUnit.Framework;
using Moq;
using BuildingBlocks.Player;

namespace BuildingBlocks.Team
{
    [TestFixture]
    public class TeamInfoTest
    {
        private ITransform teamTransform;
        private Mock<ITransform> transformMock;
        private Mock<IGameObject> gameObjectMock;
        private Mock<INetworkView> networkViewMock;
        private Mock<ITransform> targetTransformMock;

        private Team team;

        private int id = 3;
        private string name = "name";
        private string imageTarget = "imageTarget";
        private float progress = .13f;

        [SetUp]
        public void Setup()
        {
            teamTransform = Mock.Of<ITransform>();
            transformMock = new Mock<ITransform>();
            networkViewMock = new Mock<INetworkView>();
            targetTransformMock = new Mock<ITransform>();

            gameObjectMock = new Mock<IGameObject>();
            gameObjectMock.SetupGet(g => g.transform).Returns(transformMock.Object);
            gameObjectMock.SetupGet(g => g.networkView).Returns(networkViewMock.Object);
            gameObjectMock.Setup(g => g.Find("Teams")).Returns(
                Mock.Of<IGameObject>(g => g.transform == teamTransform));

            gameObjectMock.Setup(g => g.Find("GoalStructure")).Returns(
                Mock.Of<IGameObject>(g => g.transform == Mock.Of<ITransform>()
                    && g.Clone() == Mock.Of<IGameObject>(c => c.transform == Mock.Of<ITransform>())));
            gameObjectMock.Setup(g => g.Find(imageTarget)).Returns(
                Mock.Of<IGameObject>(g => g.transform == targetTransformMock.Object));

            team = new Team(gameObjectMock.Object);
        }

        [Test]
        public void TestCreate()
        {
            transformMock.VerifySet(t => t.parent = teamTransform);
        }

        [Test]
        public void TestRPC_SetTeamProgress()
        {
            team.RPC_SetTeamProgress(progress);
            Assert.AreEqual(progress, team.Progress);
        }

        [Test]
        public void TestRPC_SetTeamInfo()
        {
            team.RPC_SetTeamInfo(id, name, imageTarget);

            Assert.AreEqual(id, team.TeamId);
            Assert.AreEqual(name, team.Name);
            Assert.AreEqual(imageTarget, team.Target);

            targetTransformMock.VerifySet(t => t.parent = transformMock.Object);
        }
    }
}
