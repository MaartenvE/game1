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
    public void SetHalfBlockColor(Vector3 color)
    {
        GameObject rotatingBlock = GameObject.Find("RotatingBlock");
        rotatingBlock.renderer.material.color = ColorModel.ConvertToUnityColor(color);
    }

    [RPC]
    public void SetBlockHalf()
    {
        HasFullBlock = false;
        GameObject rotatingBlock = GameObject.Find("RotatingBlock");
        GameObject halfBlock = Resources.Load("HalfBlock") as GameObject;
        rotatingBlock.GetComponent<MeshFilter>().mesh = halfBlock.GetComponent<MeshFilter>().mesh;
    }

    [RPC]
    public void SetBlockFull()
    {
        HasFullBlock = true;
        GameObject rotatingBlock = GameObject.Find("RotatingBlock");
        GameObject block = Resources.Load("GoalCube") as GameObject;
        rotatingBlock.GetComponent<MeshFilter>().mesh = block.GetComponent<MeshFilter>().mesh;
    }

    [RPC]
    public void ThrowAwayBlock(NetworkMessageInfo message)
    {
        INetworkPlayer nPlayer = new NetworkPlayerWrapper(message.sender);
        IPlayer player = Player.GetPlayer(nPlayer);
        player.GiveNewInventoryBlock();
    }
}