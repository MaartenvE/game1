﻿using UnityEngine;

public class BlockBehaviour : GroundBlockBehaviour
{
    [RPC]
    void RemoveBlock()
    {
        // todo: Create shortcut to get ITeam to which block belongs
        Transform target = transform.parent;
        GameObject teamObject = target.parent.gameObject;

        TeamInfo teamInfo = teamObject.GetComponent<TeamInfoLoader>().TeamInfo;
        ITeam team = TeamLoader.TeamManager.GetTeam(teamInfo.ID);

        team.Tracker.RemoveBlock(transform.localPosition);

        Network.RemoveRPCs(networkView.viewID);
        Network.Destroy(networkView.viewID);
    }
}