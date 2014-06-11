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
        Color?[, ,] goal = new Color?[3, 3, 3];
        goal[1, 0, 1] = Color.red;
        goal[1, 1, 1] = Color.blue;
        goal[1, 2, 1] = Color.blue;
        goal[1, 0, 2] = Color.blue;
        goal[1, 0, 0] = Color.blue;

        teamManager = new TeamManager(new[] {
            new Team("Team 1", "ImageTarget1", goal),
            new Team("Team 2", "ImageTarget2", goal)
        });

        instantiateStructure(new Structure<Color?>(goal));
        instantiateTeamObjects();
    }

    void OnPlayerConnected(NetworkPlayer networkPlayer)
    {
        IPlayer player = new Player(new NetworkPlayerWrapper(networkPlayer));

        teamManager.AddPlayer(player);
        Debug.Log("Player assigned to " + player.Team.Name);

        GameObject.Find("Player").GetComponent<PlayerInfo>().SendInfo(player);
        player.Team.TeamObject.GetComponent<TeamInfoLoader>().TeamInfo.SetProgress(player.Team.Progress);

        instantiateCubeFinger(player);
    }

    void OnPlayerDisconnected(NetworkPlayer networkPlayer)
    {
        IPlayer player = TeamLoader.TeamManager.GetPlayer(new NetworkPlayerWrapper(networkPlayer));

        Network.RemoveRPCs(player.CubeFinger.networkView.viewID);
        Network.Destroy(player.CubeFinger.networkView.viewID);

        teamManager.RemovePlayer(player);
        Debug.Log("Player " + networkPlayer + " left.");
    }

    void instantiateStructure(Structure<Color?> goal)
    {
        GameObject prefab = Resources.Load("GoalCube") as GameObject;
        foreach (Vector3 position in goal.Keys)
        {
            Vector3 location = goal.Denormalize(position, prefab.transform.localScale.x);
            GameObject blockObject = Network.Instantiate(prefab, location, prefab.transform.rotation, 1) as GameObject;
            blockObject.GetComponent<GoalCubeBehaviour>().SetInfo("GoalStructure", goal[position].GetValueOrDefault());
        }
    }

    void instantiateTeamObjects()
    {
        GameObject prefab = Resources.Load("Team") as GameObject;
        foreach (ITeam team in teamManager.Teams)
        {
            GameObject teamObject = Network.Instantiate(prefab, Vector3.zero, prefab.transform.rotation, 1) as GameObject;
            teamObject.GetComponent<TeamInfoLoader>().TeamInfo.SetInfo(team.ID, team.Name, team.ImageTarget);
            team.TeamObject = new GameObjectWrapper(teamObject);
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
