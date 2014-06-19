using UnityEngine;
using System.Collections;
using BuildingBlocks.Team;
using BuildingBlocks.Player;

public class PlayerInfo : MonoBehaviour
{
    public void SendInfo(IPlayer player, int? teamId)
    {
        networkView.RPC("SetPlayerInfo", player.NetworkPlayer.NetworkPlayer, teamId ?? player.Team.TeamId);
    }

	public void SendInfo(IPlayer player){
		networkView.RPC("SetPlayerInfo", player.NetworkPlayer.NetworkPlayer, player.Team.TeamId);
	}

    [RPC]
    void SetPlayerInfo(int team) { }
    
    [RPC]
    void SetBlockType(int full, Vector3 color) { }

    [RPC]
    public void ThrowAwayBlock(NetworkMessageInfo message)
    {
        IPlayer player = Player.GetPlayer(new NetworkPlayerWrapper(message.sender));
        player.GiveNewInventoryBlock();
    }
}