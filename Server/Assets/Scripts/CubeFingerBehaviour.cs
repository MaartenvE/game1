using UnityEngine;
using System.Collections;

public enum CubeFingerMode
{
    None,
    Delete,
    Build
};

public class CubeFingerBehaviour : MonoBehaviour
{
    public IPlayer Player;

    private Color color;

    private Vector3 bufferedColor;
    private CubeFingerMode mode;

    public void OnNetworkInstantiate(NetworkMessageInfo info)
    {
        ShowFinger(0);
    }

    public void SetParent(string parent)
    {
        gameObject.transform.parent = GameObject.Find(parent).transform;
        networkView.RPC("SetFingerParent", RPCMode.OthersBuffered, parent);
    }

    public void SetPlayer(IPlayer player)
    {
        this.Player = player;
        networkView.RPC("SetPersonalFinger", player.NetworkPlayer.NetworkPlayer);
        player.GiveInventoryBlock();
        ColorFinger(ColorModel.ConvertToVector3(Player.HalfBlock.CalculateUnityColor()));
    }

    void OnPlayerConnected(NetworkPlayer networkPlayer)
    {
        networkView.RPC("SetFingerMode", networkPlayer, (int) mode);
        networkView.RPC("ColorFinger", networkPlayer, this.bufferedColor);
    }

    [RPC]
    void SetFingerParent(string parent) { }

    [RPC]
    void SetPersonalFinger() { }

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
    void SetFingerMode(int mode)
    {
        //this.renderer.material.color = (delete == 0) ? new Color(1, 0, 0, 0.6f) : this.color;
        this.mode = (CubeFingerMode)mode;
        networkView.RPC("SetFingerMode", RPCMode.Others, mode);
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

    public void UpdateColor(Vector3 color)
    {
        ColorFinger(color);
    }
}
