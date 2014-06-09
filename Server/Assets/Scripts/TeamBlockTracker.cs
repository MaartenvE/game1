using UnityEngine;

public class TeamBlockTracker
{
    private GameObject prefab;
    private TeamInfo teamInfo;

    public TeamBlockTracker(GameObject parent, GameObject prefab)
    {
        this.prefab = prefab;
        this.teamInfo = parent.GetComponent<TeamInfo>();

        instantiateGroundBlock(Color.red);
    }

    private void instantiateGroundBlock(Color color)
    {
        BlockBehaviourFactory f = prefab.GetComponent<BlockBehaviourFactory>();
        f.BlockBehaviourType = "GroundBlockBehaviour";
        instantiateBlock(Vector3.zero, color);
        f.BlockBehaviourType = "BlockBehaviour";
    }

    private void instantiateBlock(Vector3 location, Color color)
    {
        GameObject block = Network.Instantiate(prefab, location, prefab.transform.rotation, 1) as GameObject;
        block.GetComponent<GroundBlockBehaviour>().SetInfo(teamInfo.ImageTarget, color);
    }

    public void PlaceBlock(IPlayer player, Vector3 location)
    {
        if (player.Team.ID == teamInfo.ID)
        {
            instantiateBlock(location, Color.green);
        }
    }
}
