using NUnit.Framework;
using Moq;

namespace BuildingBlocks.Team
{
    [TestFixture]
    public class TeamCreatorTest
    {
        private Mock<ITeam> team1Mock;
        private Mock<ITeam> team2Mock;
        private Mock<IGameObject> gameObjectMock;
        private Mock<ITeamInstantiator> instantiatorMock;
        private Mock<IStructureTracker> structureTracker1Mock;
        private Mock<IStructureTracker> structureTracker2Mock;

        private TeamCreator creator;

        [SetUp]
        public void Setup()
        {
            team1Mock = new Mock<ITeam>();
            team2Mock = new Mock<ITeam>();
            gameObjectMock = new Mock<IGameObject>();
            instantiatorMock = new Mock<ITeamInstantiator>();
            structureTracker1Mock = new Mock<IStructureTracker>();
            structureTracker2Mock = new Mock<IStructureTracker>();

            team1Mock.SetupGet(t => t.StructureTracker).Returns(structureTracker1Mock.Object);
            team2Mock.SetupGet(t => t.StructureTracker).Returns(structureTracker2Mock.Object);

            int count = 0;
            instantiatorMock.Setup(i => i.InstantiateTeam()).Returns(() => 
                count++ == 0 ? team1Mock.Object : team2Mock.Object
            );

            creator = new TeamCreator(gameObjectMock.Object, instantiatorMock.Object);
        }

        [Test]
        public void TestInstantiation()
        {
            instantiatorMock.Verify(i => i.InstantiateTeam(), Times.Exactly(2));
        }

        [Test]
        public void TestSetInfo()
        {
            team1Mock.VerifySet(t => t.Name = "Team 1");
            team1Mock.VerifySet(t => t.Target = "ImageTarget1");
            team2Mock.VerifySet(t => t.Name = "Team 2");
            team2Mock.VerifySet(t => t.Target = "ImageTarget2");
        }

        [Test]
        public void TestSendInfo()
        {
            team1Mock.Verify(t => t.SendInfo());
            team2Mock.Verify(t => t.SendInfo());
        }

        [Test]
        public void TestInstantiateGroundBlocks()
        {
            creator.InstantiateGroundBlocks();
            structureTracker1Mock.Verify(s => s.PlaceGroundBlock());
            structureTracker2Mock.Verify(s => s.PlaceGroundBlock());
        }
    }
}
