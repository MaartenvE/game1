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
        //GameObject target = this.transform.parent.gameObject;
        //GameObject team = target.transform.parent.gameObject;
        //TeamBlockTracker tracker = team.GetComponent<TeamBlockTrackerLoader>().Tracker;
        //if (player.HasPlaceableBlock)
        {
            Vector3 position = this.transform.position + (direction * 0.2f);

            //tracker.PlaceBlock(player, position, Color.green);

            player.Team.Tracker.PlaceBlock(player, position, player.HalfBlock.CalculateUnityColor());
        }
    }

    [RPC]
    void RemoveBlock() { }
}
