
public interface ITeamManager
{
    int NumberOfTeams { get; }

    void AddPlayer(IPlayer player);
    void RemovePlayer(IPlayer player);
}
