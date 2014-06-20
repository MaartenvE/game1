/*using UnityEngine;
using NUnit.Framework;
using Moq;

namespace BuildingBlocks.Team
{
    [TestFixture]
    public class TeamInfoTest
    {
        private Mock<ITransform> transformMock;
        private Mock<IGameObject> gameObjectMock;
        private Mock<INetworkView> networkViewMock;

        private TeamInfo teamInfo;

        private int id = 3;
        private string name = "name";
        private string imageTarget = "imageTarget";
        private float progress = .13f;

        [SetUp]
        public void Setup()
        {
            transformMock = new Mock<ITransform>();
            networkViewMock = new Mock<INetworkView>();

            gameObjectMock = new Mock<IGameObject>();
            gameObjectMock.SetupGet(g => g.transform).Returns(transformMock.Object);
            gameObjectMock.SetupGet(g => g.networkView).Returns(networkViewMock.Object);

            teamInfo = new TeamInfo(gameObjectMock.Object);
        }

        [Test]
        public void TestNetworkInstantiate()
        {
            ITransform teamTransform = Mock.Of<ITransform>();

            gameObjectMock.Setup(g => g.Find("Teams")).Returns(
                Mock.Of<IGameObject>(g => g.transform == teamTransform)
            );

            teamInfo.OnNetworkInstantiate();

            transformMock.VerifySet(t => t.parent = teamTransform);
        }

        [Test]
        public void TestSetInfo()
        {
            teamInfo.SetInfo(id, name, imageTarget);
            networkViewMock.Verify(n => n.RPC("SetTeamInfo", RPCMode.AllBuffered, id, name, imageTarget));
        }

        [Test]
        public void TestSetProgress()
        {
            teamInfo.SetProgress(progress);
            networkViewMock.Verify(n => n.RPC("SetTeamProgress", RPCMode.AllBuffered, progress));
        }

        [Test]
        public void TestRPC_SetTeamProgress()
        {
            teamInfo.RPC_SetTeamProgress(progress);
            Assert.AreEqual(progress, teamInfo.Progress);
        }

        [Test]
        public void TestRPC_SetTeamInfo()
        {
            Mock<ITransform> targetTransformMock = new Mock<ITransform>();
            gameObjectMock.Setup(g => g.Find(imageTarget)).Returns(Mock.Of<IGameObject>(g =>
                g.transform == targetTransformMock.Object));

            teamInfo.RPC_SetTeamInfo(id, name, imageTarget);

            Assert.AreEqual(id, teamInfo.ID);
            Assert.AreEqual(name, teamInfo.Name);
            Assert.AreEqual(imageTarget, teamInfo.ImageTarget);

            targetTransformMock.VerifySet(t => t.parent = transformMock.Object);
        }


        [Test]
        public void TestIsMine()
        {
            gameObjectMock.Setup(g => g.Find(imageTarget)).Returns(Mock.Of<IGameObject>(g =>
                g.transform == Mock.Of<ITransform>()));

            PlayerInfo.Team = 0;
            teamInfo.RPC_SetTeamInfo(0, "TeamName", imageTarget);

            Assert.IsTrue(teamInfo.IsMine);
        }
    }
}
*/