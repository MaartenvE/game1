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
        IPlayer player = TeamLoader.TeamManager.GetPlayer(new NetworkPlayerWrapper(info.sender));
        GameObject target = this.transform.parent.gameObject;
        GameObject team = target.transform.parent.gameObject;
        TeamBlockTrackerLoader tracker = team.GetComponent<TeamBlockTrackerLoader>();

        Vector3 position = this.transform.position + (direction * 0.2f);

        tracker.Tracker.PlaceBlock(player, position);
    }

    [RPC]
    void RemoveBlock() { }
}
