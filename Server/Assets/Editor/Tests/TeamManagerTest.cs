using NUnit.Framework;
using Moq;

[TestFixture]
public class TeamManagerTest
{
    [Test]
    public void TestSingleTeam()
    {
        ITeamManager teamManager = new TeamManager(new[] { Mock.Of<ITeam>() });
        Assert.AreEqual(1, teamManager.NumberOfTeams);
    }

    [Test]
    public void TestMultipleTeams()
    {
        ITeamManager teamManager = new TeamManager(new[] { Mock.Of<ITeam>(), Mock.Of<ITeam>(), Mock.Of<ITeam>() });
        Assert.AreEqual(3, teamManager.NumberOfTeams);
    }

    [Test, ExpectedException("System.ArgumentException")]
    public void TestZeroTeams()
    {
        new TeamManager(new ITeam[] {});
    }

    [Test, ExpectedException("System.ArgumentException")]
    public void TestNullTeam()
    {
        new TeamManager(new ITeam[] { null, null });
    }

    [Test]
    public void AddPlayerToSingleTeam()
    {
        ITeam team = Mock.Of<ITeam>();
        IPlayer player = Mock.Of<IPlayer>();
        ITeamManager teamManager = new TeamManager(new[] { team });

        teamManager.AddPlayer(player);

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

        ITeamManager teamManager = new TeamManager(new[] { team1Mock.Object, team2Mock.Object });

        teamManager.AddPlayer(player);

        Assert.AreEqual(1, count);
    }

    [Test]
    public void AddPlayerToUnequalSizedTeams()
    {
        IPlayer player = Mock.Of<IPlayer>();
        ITeam team1 = Mock.Of<ITeam>(t => t.Size == 4);
        ITeam team2 = Mock.Of<ITeam>(t => t.Size == 3);
        ITeam team3 = Mock.Of<ITeam>(t => t.Size == 5);

        ITeamManager teamManager = new TeamManager(new[] { team1, team2, team3 });

        teamManager.AddPlayer(player);

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

        ITeamManager teamManager = new TeamManager(new[] { team1, team2, team3 });

        teamManager.AddPlayer(player);

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

        ITeamManager teamManager = new TeamManager(new[] { team1, team2, team3, team4 });

        teamManager.AddPlayer(player);

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

        ITeamManager teamManager = new TeamManager(new[] { team });
        teamManager.RemovePlayer(player);

        Mock.Get(team).Verify(t => t.RemovePlayer(player));
    }

    [Test]
    public void RemovePlayerFromUnexistingTeam()
    {
        ITeam team1 = Mock.Of<ITeam>();
        ITeam team2 = Mock.Of<ITeam>();
        IPlayer player = Mock.Of<IPlayer>(p => p.Team == team1);

        ITeamManager teamManager = new TeamManager(new[] { team2 });
        teamManager.RemovePlayer(player);

        Mock.Get(team1).Verify(t => t.RemovePlayer(player), Times.Never());
    }

    [Test]
    public void RemovePlayerFromNullTeam()
    {
        ITeam team = Mock.Of<ITeam>();
        IPlayer player = Mock.Of<IPlayer>();

        ITeamManager teamManager = new TeamManager(new[] { team });
        teamManager.RemovePlayer(player);

        Mock.Get(team).Verify(t => t.RemovePlayer(player), Times.Never());
    }
}
