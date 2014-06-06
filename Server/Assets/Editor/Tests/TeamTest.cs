using NUnit.Framework;
using Moq;
using System.Linq;

[TestFixture]
class TeamTest
{
    [Test]
    public void CreateTeam()
    {
        string teamName = "team name";
        ITeam team = new Team(teamName);
        Assert.AreEqual(teamName, team.Name);
        Assert.AreEqual(0.0f, team.Progress);
    }

    [Test]
    public void MultipleTeams()
    {
        ITeam team1 = new Team("team1");
        ITeam team2 = new Team("team2");
        Assert.AreNotEqual(team1.ID, team2.ID);
    }

    [Test]
    public void AddPlayer()
    {
        ITeam team = new Team("team");
        IPlayer player = Mock.Of<IPlayer>();

        team.AddPlayer(player);

        Mock.Get(player).VerifySet(p => p.Team = team);
    }

    [Test]
    public void AddPlayerTeamList()
    {
        ITeam team = new Team("team");
        IPlayer player = Mock.Of<IPlayer>();

        team.AddPlayer(player);

        Assert.AreEqual(player, team.Players.First());
    }

    [Test]
    public void AddMultiplePlayers()
    {
        ITeam team = new Team("team");
        IPlayer player1 = Mock.Of<IPlayer>();
        IPlayer player2 = Mock.Of<IPlayer>();

        team.AddPlayer(player1);
        team.AddPlayer(player2);

        Assert.IsTrue(team.Players.Contains(player1));
        Assert.IsTrue(team.Players.Contains(player2));
    }

    [Test]
    public void RemovePlayer()
    {
        ITeam team = new Team("team");
        IPlayer player = Mock.Of<IPlayer>();

        team.AddPlayer(player);
        team.RemovePlayer(player);

        Mock.Get(player).VerifySet(p => p.Team = null);
    }

    [Test]
    public void RemovePlayerTeamList()
    {
        ITeam team = new Team("team");
        IPlayer player = Mock.Of<IPlayer>();

        team.AddPlayer(player);
        team.RemovePlayer(player);

        Assert.IsFalse(team.Players.Contains(player));
    }

    [Test]
    public void RemoveMultiplePlayers()
    {
        ITeam team = new Team("team");
        IPlayer player1 = Mock.Of<IPlayer>();
        IPlayer player2 = Mock.Of<IPlayer>();
        IPlayer player3 = Mock.Of<IPlayer>();

        team.AddPlayer(player1);
        team.AddPlayer(player2);
        team.AddPlayer(player3);

        team.RemovePlayer(player1);
        team.RemovePlayer(player3);

        Assert.IsFalse(team.Players.Contains(player1));
        Assert.IsTrue(team.Players.Contains(player2));
        Assert.IsFalse(team.Players.Contains(player3));
    }
}
