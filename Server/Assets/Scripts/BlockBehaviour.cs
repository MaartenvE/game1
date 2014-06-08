using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{
    public void SetInfo(string parent, Color color)
    {
        networkView.RPC("SetBlockInfo", RPCMode.AllBuffered, parent, new Vector3(color.r, color.g, color.b));
    }

    [RPC]
    void SetBlockInfo(string parent, Vector3 color)
    {
        this.renderer.material.color = new Color(color.x, color.y, color.z);
    }

    [RPC]
    void RemoveBlock()
    {
        Network.RemoveRPCs(networkView.viewID);
        Network.Destroy(networkView.viewID);
    }
}
