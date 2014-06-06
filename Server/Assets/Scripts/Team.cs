using System.Collections.Generic;

public class Team : ITeam
{
    private static int nextId = 1;

    private int id;
    public int ID
    {
        get
        {
            return id;
        }
    }

    private int size = 0;
    public int Size
    {
        get
        {
            return size;
        }
    }

    private string name;
    public string Name
    {
        get
        {
            return name;
        }
    }

    public float Progress { get; set; }

    private LinkedList<IPlayer> players;
    public IEnumerable<IPlayer> Players
    {
        get
        {
            return players;
        }
    }

    public Team(string name)
    {
        this.id = nextId++;
        this.name = name;
        this.Progress = 0.0f;
        this.players = new LinkedList<IPlayer>();
    }

    public void AddPlayer(IPlayer player)
    {
        player.Team = this;
        players.AddLast(player);
    }

    public void RemovePlayer(IPlayer player)
    {
        player.Team = null;
        players.Remove(player);
    }
}

