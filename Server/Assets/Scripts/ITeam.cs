using System.Collections.Generic;

public interface ITeam
{
    int ID { get; }
    int Size { get; }
    string Name { get; }
    string ImageTarget { get; }

    float Progress { get; set; }

    IEnumerable<IPlayer> Players { get; }

    void AddPlayer(IPlayer player);
    void RemovePlayer(IPlayer player);
}
