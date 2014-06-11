using UnityEngine;
using System.Collections;

public class CubeFingerBehaviour : MonoBehaviour
{
    public IPlayer Player;

    public void OnNetworkInstantiate(NetworkMessageInfo info)
    {
        ShowFinger(0);
    }

    public void SetParent(string parent)
    {
        gameObject.transform.parent = GameObject.Find(parent).transform;
        networkView.RPC("SetCubeFingerParent", RPCMode.OthersBuffered, parent);
    }

    public void SetPlayer(IPlayer player)
    {
        this.Player = player;
        networkView.RPC("SetPersonalCubeFinger", player.NetworkPlayer.NetworkPlayer);
        player.GiveInventoryBlock();
        Debug.LogWarning("FingerColor = " + Player.HalfBlock.CalculateUnityColor());
        ColorFinger(ColorModel.ConvertToVector3(Player.HalfBlock.CalculateUnityColor()));
    }

    [RPC]
    void SetCubeFingerParent(string parent) { }

    [RPC]
    void SetPersonalCubeFinger() { }

    [RPC]
    void MoveFinger(Vector3 location)
    {
        this.transform.localPosition = location;
        networkView.RPC("MoveFinger", RPCMode.Others, location);
    }

    [RPC]
    void ShowFinger(int show)
    {
        this.renderer.enabled = show != 0;
        networkView.RPC("ShowFinger", RPCMode.Others, show);
    }

    [RPC]
    void ColorFinger(Vector3 color)
    {
        Color temp = ColorModel.ConvertToUnityColor(color);
        Debug.LogWarning("Color = " + temp);
        temp.a = 0.6f;
        this.renderer.material.color = temp;
        networkView.RPC("ColorFinger", RPCMode.Others, color);
    }
}
