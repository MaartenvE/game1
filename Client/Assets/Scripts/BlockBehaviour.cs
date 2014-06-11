using UnityEngine;
using System;

public class BlockBehaviour : MonoBehaviour
{
    public void Place(Vector3 direction)
    {
        networkView.RPC("PlaceNewBlock", RPCMode.Server, direction);
    }

    public void Remove()
    {
        networkView.RPC("RemoveBlock", RPCMode.Server);
    }

    [RPC]
    void SetBlockInfo(string parent, Vector3 color)
    {
        this.transform.parent = GameObject.Find(parent).transform as Transform;
        this.renderer.material.color = new Color(color.x, color.y, color.z);
    }

    [RPC]
    void PlaceNewBlock(Vector3 direction) { }

    [RPC]
    void RemoveBlock() { }
}
