using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour
{
    public int Team;
    public GameObject CubeFinger;

    private GameObject teamObject;

    public void SendInfo(IPlayer player)
    {
        networkView.RPC("SetPlayerInfo", player.NetworkPlayer.getNetworkPlayer(), player.Team.ID);
    }

    [RPC]
    void SetPlayerInfo(int team)
    {
        Team = team;

        foreach (TeamInfo teamInfo in GameObject.Find("Teams").GetComponentsInChildren<TeamInfo>())
        {
            if (teamInfo.ID == team)
            {
                teamObject = teamInfo.gameObject;
                break;
            }
        }

        if (CubeFinger != null)
        {
            CubeFinger.transform.parent = teamObject.transform;
        }
    }
}