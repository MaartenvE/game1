using UnityEngine;
using BuildingBlocks.CubeFinger;

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
		Color?[,,] goal = StructureReader.loadRandomLevelFromMaps ();

        teamManager = new TeamManager(new[] {
            new Team("Team 1", "ImageTarget1", goal),
            new Team("Team 2", "ImageTarget2", goal)
        });

        instantiateStructure(new Structure<Color?>(goal));
        instantiateTeamObjects();
    }

    [RPC]
    void SelectTeam(int spectate, NetworkMessageInfo info)
    {
        IPlayer player = new Player(new NetworkPlayerWrapper(info.sender));
        if (spectate == 0)
        {
            teamManager.AddPlayer(player);
            Debug.Log("Player assigned to " + player.Team.Name);

            GameObject.Find("Player").GetComponent<PlayerInfo>().SendInfo(player);
            player.Team.TeamObject.GetComponent<TeamInfoLoader>().TeamInfo.SetProgress(player.Team.Progress);

            instantiateCubeFinger(player);
        }
        else
        {
            GameObject.Find("Player").GetComponent<PlayerInfo>().SendInfo(player, 0);
            Debug.Log("Player assigned to Spectator");
        }
    }

    void OnPlayerDisconnected(NetworkPlayer networkPlayer)
    {
        IPlayer player = TeamLoader.TeamManager.GetPlayer(new NetworkPlayerWrapper(networkPlayer));

        if (player != null)
        {
            Network.RemoveRPCs(player.CubeFinger.networkView.viewID);
            Network.Destroy(player.CubeFinger.networkView.viewID);

            teamManager.RemovePlayer(player);
        }

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

        CubeFinger finger = cubeFinger.GetComponent<CubeFingerLoader>().Finger as CubeFinger;
        finger.SetParent(player.Team.ImageTarget);
        finger.SetPlayer(player);
    }
}
