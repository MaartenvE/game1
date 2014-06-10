using UnityEngine;

public class GroundBlockBehaviour : MonoBehaviour
{
    public void SetInfo(string parent, Color color)
    {
        networkView.RPC("SetBlockInfo", RPCMode.AllBuffered, parent, new Vector3(color.r, color.g, color.b));
    }

    [RPC]
    void SetBlockInfo(string parent, Vector3 color)
    {
        this.transform.parent = GameObject.Find(parent).transform;
        this.renderer.material.color = new Color(color.x, color.y, color.z);
    }

    [RPC]
    void PlaceNewBlock(Vector3 direction, NetworkMessageInfo info)
    {
        Vector3 position = this.transform.position + (direction * transform.localScale.x);

        IPlayer player = TeamLoader.TeamManager.GetPlayer(new NetworkPlayerWrapper(info.sender));
        player.Team.Tracker.PlaceBlock(player, position, Color.green);
    }

    [RPC]
    void RemoveBlock() { }
}
