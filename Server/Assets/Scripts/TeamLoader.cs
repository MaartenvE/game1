using UnityEngine;

public class TeamLoader : MonoBehaviour
{
    private static ITeamManager teamManager;
    public static ITeamManager TeamManager
    {
        get
        {
            return teamManager;
        }
    }

    void OnServerInitialized()
    {
        teamManager = new TeamManager(new[] {
            new Team("Team 1", "ImageTarget1"),
            new Team("Team 2", "ImageTarget2")
        });

        instantiateTeamObjects();
    }

    void OnPlayerConnected(NetworkPlayer networkPlayer)
    {
        IPlayer player = new Player(new NetworkPlayerWrapper(networkPlayer));

        if (teamManager == null)
        {
            Debug.LogError("TeamManager is null");
        }

        if (player == null)
        {
            Debug.LogError("Player is null");
        }

        teamManager.AddPlayer(player);
        Debug.Log("Player assigned to " + player.Team.Name);

        GameObject.Find("Player").GetComponent<PlayerInfo>().SendInfo(player);
    }

    void instantiateTeamObjects()
    {
        GameObject prefab = Resources.Load("Team") as GameObject;
        foreach (ITeam team in teamManager.Teams)
        {
            GameObject teamObject = Network.Instantiate(prefab, Vector3.zero, prefab.transform.rotation, 1) as GameObject;
            teamObject.GetComponent<TeamInfo>().SetInfo(team.ID, team.Name, team.ImageTarget);
        }
    }
}
