using System.Collections.Generic;

public interface ITeamManager
{
    int NumberOfTeams { get; }
    IEnumerable<ITeam> Teams { get; }

    void AddPlayer(IPlayer player);
    void RemovePlayer(IPlayer player);

    IPlayer GetPlayer(INetworkPlayer networkPlayer);
}
