/*using NUnit.Framework;
using Moq;
using System.Linq;

[TestFixture]
class TeamTest
{
    private string name = "team name";
    private string target = "image target";

    [Test]
    public void CreateTeam()
    {
        ITeam team = new Team(name, target);
        Assert.AreEqual(name, team.Name);
        Assert.AreEqual(target, team.ImageTarget);
        Assert.AreEqual(0, team.Size);
        Assert.AreEqual(0.0f, team.Progress);
        Assert.AreEqual(0, team.Players.Count());
    }

    [Test]
    public void MultipleTeams()
    {
        ITeam team1 = new Team("team1", target);
        ITeam team2 = new Team("team2", target);
        Assert.AreNotEqual(team1.ID, team2.ID);
    }

    [Test]
    public void AddPlayer()
    {
        ITeam team = new Team(name, target);
        IPlayer player = Mock.Of<IPlayer>();

        team.AddPlayer(player);

        Mock.Get(player).VerifySet(p => p.Team = team);

        Assert.AreEqual(1, team.Size);
    }

    [Test]
    public void AddPlayerTeamList()
    {
        ITeam team = new Team(name, target);
        IPlayer player = Mock.Of<IPlayer>();

        team.AddPlayer(player);

        Assert.AreEqual(player, team.Players.First());
    }

    [Test]
    public void AddMultiplePlayers()
    {
        ITeam team = new Team(name, target);
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
        ITeam team = new Team(name, target);
        IPlayer player = Mock.Of<IPlayer>();

        team.AddPlayer(player);
        team.RemovePlayer(player);

        Mock.Get(player).VerifySet(p => p.Team = null);

        Assert.AreEqual(0, team.Size);
    }

    [Test]
    public void RemovePlayerTeamList()
    {
        ITeam team = new Team(name, target);
        IPlayer player = Mock.Of<IPlayer>();

        team.AddPlayer(player);
        team.RemovePlayer(player);

        Assert.AreEqual(0, team.Size);
        Assert.IsFalse(team.Players.Contains(player));
    }

    [Test]
    public void RemoveMultiplePlayers()
    {
        ITeam team = new Team(name, target);
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
*/