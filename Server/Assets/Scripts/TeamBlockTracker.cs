using UnityEngine;

public class TeamBlockTracker
{
    private GameObject parent;
    private GameObject prefab;
    private TeamInfo teamInfo;

    public TeamBlockTracker(GameObject parent, GameObject prefab)
    {
        this.parent = parent;
        this.prefab = prefab;
        this.teamInfo = parent.GetComponent<TeamInfo>();

        instantiateBlock(Vector3.zero, Color.red);
    }

    void instantiateBlock(Vector3 location, Color color)
    {
        GameObject block = Network.Instantiate(prefab, location, prefab.transform.rotation, 1) as GameObject;
        block.GetComponent<BlockBehaviour>().SetInfo(teamInfo.ImageTarget, color);
    }

    [RPC]
    void PlaceBlock(Vector3 location, NetworkMessageInfo info)
    {
        IPlayer player = TeamLoader.TeamManager.GetPlayer(new NetworkPlayerWrapper(info.sender));
        if (player.Team.ID == teamInfo.ID)
        {
            instantiateBlock(location, Color.green);
        }
    }
}
