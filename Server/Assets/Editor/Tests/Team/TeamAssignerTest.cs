using NUnit.Framework;
using Moq;
using BuildingBlocks.Player;

namespace BuildingBlocks.Team
{
    [TestFixture]
    public class TeamAssignerTest
    {
        [Test]
        public void TestSingleTeam()
        {
            ITeamAssigner TeamAssigner = new TeamAssigner(new[] { Mock.Of<ITeam>() });
            Assert.AreEqual(1, TeamAssigner.NumberOfTeams);
        }

        [Test]
        public void TestMultipleTeams()
        {
            ITeamAssigner TeamAssigner = new TeamAssigner(new[] { Mock.Of<ITeam>(), Mock.Of<ITeam>(), Mock.Of<ITeam>() });
            Assert.AreEqual(3, TeamAssigner.NumberOfTeams);
        }

        [Test, ExpectedException("System.ArgumentException")]
        public void TestZeroTeams()
        {
            new TeamAssigner(new ITeam[] { });
        }

        [Test, ExpectedException("System.ArgumentException")]
        public void TestNullTeam()
        {
            new TeamAssigner(new ITeam[] { null, null });
        }

        [Test]
        public void AddPlayerToSingleTeam()
        {
            ITeam team = Mock.Of<ITeam>();
            IPlayer player = Mock.Of<IPlayer>();
            ITeamAssigner TeamAssigner = new TeamAssigner(new[] { team });

            TeamAssigner.AddPlayer(player);

            Mock.Get(team).Verify(t => t.AddPlayer(player));
        }

        [Test]
        public void AddPlayerToEqualTeams()
        {
            int count = 0;

            IPlayer player = Mock.Of<IPlayer>();

            Mock<ITeam> team1Mock = new Mock<ITeam>();
            team1Mock.SetupGet(t => t.Size).Returns(3);
            team1Mock.SetupGet(t => t.Progress).Returns(0.5f);
            team1Mock.Setup(t => t.AddPlayer(player)).Callback(() => count++);

            Mock<ITeam> team2Mock = new Mock<ITeam>();
            team2Mock.SetupGet(t => t.Size).Returns(3);
            team2Mock.SetupGet(t => t.Progress).Returns(0.5f);
            team2Mock.Setup(t => t.AddPlayer(player)).Callback(() => count++);

            ITeamAssigner TeamAssigner = new TeamAssigner(new[] { team1Mock.Object, team2Mock.Object });

            TeamAssigner.AddPlayer(player);

            Assert.AreEqual(1, count);
        }

        [Test]
        public void AddPlayerToUnequalSizedTeams()
        {
            IPlayer player = Mock.Of<IPlayer>();
            ITeam team1 = Mock.Of<ITeam>(t => t.Size == 4);
            ITeam team2 = Mock.Of<ITeam>(t => t.Size == 3);
            ITeam team3 = Mock.Of<ITeam>(t => t.Size == 5);

            ITeamAssigner TeamAssigner = new TeamAssigner(new[] { team1, team2, team3 });

            TeamAssigner.AddPlayer(player);

            Mock.Get(team1).Verify(t => t.AddPlayer(player), Times.Never());
            Mock.Get(team2).Verify(t => t.AddPlayer(player));
            Mock.Get(team3).Verify(t => t.AddPlayer(player), Times.Never());
        }

        [Test]
        public void AddPlayerToUnequalProgressTeams()
        {
            IPlayer player = Mock.Of<IPlayer>();
            ITeam team1 = Mock.Of<ITeam>(t => t.Progress == 0.5f);
            ITeam team2 = Mock.Of<ITeam>(t => t.Progress == 0.4f);
            ITeam team3 = Mock.Of<ITeam>(t => t.Progress == 0.3f);

            ITeamAssigner TeamAssigner = new TeamAssigner(new[] { team1, team2, team3 });

            TeamAssigner.AddPlayer(player);

            Mock.Get(team1).Verify(t => t.AddPlayer(player), Times.Never());
            Mock.Get(team2).Verify(t => t.AddPlayer(player), Times.Never());
            Mock.Get(team3).Verify(t => t.AddPlayer(player));
        }

        [Test]
        public void AddPlayerToUnequalTeams()
        {
            IPlayer player = Mock.Of<IPlayer>();
            ITeam team1 = Mock.Of<ITeam>(t => t.Size == 3 && t.Progress == 0.3f);
            ITeam team2 = Mock.Of<ITeam>(t => t.Size == 3 && t.Progress == 0.8f);
            ITeam team3 = Mock.Of<ITeam>(t => t.Size == 5 && t.Progress == 0.3f);
            ITeam team4 = Mock.Of<ITeam>(t => t.Size == 5 && t.Progress == 0.8f);

            ITeamAssigner TeamAssigner = new TeamAssigner(new[] { team1, team2, team3, team4 });

            TeamAssigner.AddPlayer(player);

            Mock.Get(team1).Verify(t => t.AddPlayer(player));
            Mock.Get(team2).Verify(t => t.AddPlayer(player), Times.Never());
            Mock.Get(team3).Verify(t => t.AddPlayer(player), Times.Never());
            Mock.Get(team4).Verify(t => t.AddPlayer(player), Times.Never());
        }

        [Test]
        public void RemovePlayerFromTeam()
        {
            ITeam team = Mock.Of<ITeam>();
            IPlayer player = Mock.Of<IPlayer>(p => p.Team == team);

            ITeamAssigner TeamAssigner = new TeamAssigner(new[] { team });
            TeamAssigner.RemovePlayer(player);

            Mock.Get(team).Verify(t => t.RemovePlayer(player));
        }

        [Test]
        public void RemovePlayerFromUnexistingTeam()
        {
            ITeam team1 = Mock.Of<ITeam>();
            ITeam team2 = Mock.Of<ITeam>();
            IPlayer player = Mock.Of<IPlayer>(p => p.Team == team1);

            ITeamAssigner TeamAssigner = new TeamAssigner(new[] { team2 });
            TeamAssigner.RemovePlayer(player);

            Mock.Get(team1).Verify(t => t.RemovePlayer(player), Times.Never());
        }

        [Test]
        public void RemovePlayerFromNullTeam()
        {
            ITeam team = Mock.Of<ITeam>();
            IPlayer player = Mock.Of<IPlayer>();

            ITeamAssigner TeamAssigner = new TeamAssigner(new[] { team });
            TeamAssigner.RemovePlayer(player);

            Mock.Get(team).Verify(t => t.RemovePlayer(player), Times.Never());
        }
    }
}
