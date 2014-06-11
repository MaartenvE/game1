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

        foreach (TeamInfoLoader teamInfo in GameObject.Find("Teams").GetComponentsInChildren<TeamInfoLoader>())
        {
            if (teamInfo.TeamInfo.IsMine())
            {
                teamObject = teamInfo.gameObject;
                break;
            }
        }
    }
}