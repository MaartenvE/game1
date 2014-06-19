using UnityEngine;
using System.Collections;
using BuildingBlocks.Team;
using BuildingBlocks.Player;

public class PlayerInfo : MonoBehaviour
{
    public static bool IsSpectator = false;

    public static bool HasFullBlock { get; private set; }

    private static GameObject teamObject;

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
        INetworkPlayer nPlayer = new NetworkPlayerWrapper(message.sender);
        IPlayer player = Player.GetPlayer(nPlayer);
        player.GiveNewInventoryBlock();
    }
}