using UnityEngine;
using System.Collections;

public class CubeFingerBehaviour : MonoBehaviour
{
    public IPlayer Player;

    private Color color;

    private Vector3 bufferedColor;
    private int deleteMode;

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

    void OnPlayerConnected(NetworkPlayer networkPlayer)
    {
        networkView.RPC("SetFingerDeleteMode", networkPlayer, this.deleteMode);
        networkView.RPC("ColorFinger", networkPlayer, this.bufferedColor);
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

    // todo: SetFingerDeleteMode should somehow be buffered
    [RPC]
    void SetFingerDeleteMode(int delete)
    {
        this.renderer.material.color = (delete == 0) ? new Color(1, 0, 0, 0.6f) : this.color;
        this.deleteMode = delete;
        networkView.RPC("SetFingerDeleteMode", RPCMode.Others, this.deleteMode);
    }

    // todo: ColorFinger should somehow be buffered
    [RPC]
    void ColorFinger(Vector3 color)
    {
        Color unityColor = ColorModel.ConvertToUnityColor(color);
        unityColor.a = 0.6f;
        this.renderer.material.color = unityColor;
        this.color = unityColor;
        this.bufferedColor = color;
        networkView.RPC("ColorFinger", RPCMode.Others, this.bufferedColor);
    }
}
