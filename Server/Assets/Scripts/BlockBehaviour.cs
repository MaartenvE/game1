using UnityEngine;
using BuildingBlocks.Team;

public class BlockBehaviour : GroundBlockBehaviour
{
    [RPC]
    void RemoveBlock()
    {
        team.StructureTracker.RemoveBlock(new GameObjectWrapper(gameObject));

        /*// todo: Create shortcut to get ITeam to which block belongs
        Transform target = transform.parent;
        GameObject teamObject = target.parent.gameObject;

        TeamInfo teamInfo = teamObject.GetComponent<TeamInfoLoader>().TeamInfo;
        ITeam team = TeamLoader.TeamManager.GetTeam(teamInfo.ID);

        team.Tracker.RemoveBlock(transform.localPosition);

        Network.RemoveRPCs(networkView.viewID);
        Network.Destroy(networkView.viewID);*/
    }
}
