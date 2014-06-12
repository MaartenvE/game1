using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour
{
    public int Team;
    public CubeFingerBehaviour CubeFinger;

    private GameObject teamObject;

    public void SendInfo(IPlayer player)
    {
        networkView.RPC("SetPlayerInfo", player.NetworkPlayer.NetworkPlayer, player.Team.ID);
    }

    [RPC]
    void SetPlayerInfo(int team)
    {
        Team = team;

        GameObject goalStructure = GameObject.Find("GoalStructure");

        foreach (TeamInfoLoader teamInfoLoader in GameObject.Find("Teams").GetComponentsInChildren<TeamInfoLoader>())
        {
            TeamInfo teamInfo = teamInfoLoader.TeamInfo;
            if (teamInfo.IsMine())
            {
                teamObject = teamInfoLoader.gameObject;
                goalStructure.transform.parent = GameObject.Find(teamInfo.ImageTarget).transform;
                goalStructure.transform.localPosition = Vector3.zero;
                break;
            }

            else
            {
                GameObject goalClone = GameObject.Instantiate(goalStructure) as GameObject;
                goalClone.transform.parent = GameObject.Find(teamInfo.ImageTarget).transform;
                goalClone.transform.localPosition = Vector3.zero;
            }
        }
    }

    [RPC]
    public void SetHalfBlockColor(Vector3 color)
    {

    }

    [RPC]
    public void SetBlockHalf()
    {

    }

    [RPC]
    public void SetBlockFull()
    {

    }

    [RPC]
    public void ThrowAwayBlock(NetworkMessageInfo message)
    {
        INetworkPlayer networkPlayer = new NetworkPlayerWrapper(message.sender);
        IPlayer player = TeamLoader.TeamManager.GetPlayer(networkPlayer);
        player.HalfBlock = null;
        player.GiveInventoryBlock();
    }
}