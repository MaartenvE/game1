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

        teamManager.AddPlayer(player);
        Debug.Log("Player assigned to " + player.Team.Name);

        GameObject.Find("Player").GetComponent<PlayerInfo>().SendInfo(player);

        instantiateCubeFinger(player);
    }

    void OnPlayerDisconnected(NetworkPlayer networkPlayer)
    {
        IPlayer player = TeamLoader.TeamManager.GetPlayer(new NetworkPlayerWrapper(networkPlayer));

        if (player == null) Debug.LogError("Player = null");
        else if (player.CubeFinger == null) Debug.LogError("CubeFinger = null");
        else if (player.CubeFinger.networkView == null) Debug.LogError("NetworkView = null");
        else if (player.CubeFinger.networkView.viewID == null) Debug.LogError("ViewID = null");

        Network.RemoveRPCs(player.CubeFinger.networkView.viewID);
        Network.Destroy(player.CubeFinger.networkView.viewID);

        teamManager.RemovePlayer(player);
        Debug.Log("Player " + networkPlayer + " left.");
    }

    void instantiateTeamObjects()
    {
        GameObject prefab = Resources.Load("Team") as GameObject;
        foreach (ITeam team in teamManager.Teams)
        {
            GameObject teamObject = Network.Instantiate(prefab, Vector3.zero, prefab.transform.rotation, 1) as GameObject;
            teamObject.GetComponent<TeamInfoLoader>().TeamInfo.SetInfo(team.ID, team.Name, team.ImageTarget);
        }
    }

    void instantiateCubeFinger(IPlayer player)
    {
        GameObject prefab = Resources.Load("CubeFinger") as GameObject;
        GameObject cubeFinger = Network.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation, 1) as GameObject;
        CubeFingerBehaviour behaviour = cubeFinger.GetComponent<CubeFingerBehaviour>();
        behaviour.SetParent(player.Team.ImageTarget);
        behaviour.SetPlayer(player);

        player.CubeFinger = behaviour;
    }
}
