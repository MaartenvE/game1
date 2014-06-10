using System.Collections.Generic;

public interface ITeam
{
    int ID { get; }
    int Size { get; }
    string Name { get; }
    string ImageTarget { get; }

    BlockTracker Tracker { get; }

    float Progress { get; }

    IEnumerable<IPlayer> Players { get; }

    void AddPlayer(IPlayer player);
    void RemovePlayer(IPlayer player);
}
