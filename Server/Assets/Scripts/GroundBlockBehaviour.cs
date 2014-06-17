using UnityEngine;

public class GroundBlockBehaviour : MonoBehaviour
{
    public void SetInfo(string parent, Vector3 location, Color color)
    {
        networkView.RPC("SetBlockInfo", RPCMode.AllBuffered, parent, location, new Vector3(color.r, color.g, color.b));
    }

    [RPC]
    void SetBlockInfo(string parent, Vector3 location, Vector3 color)
    {
        this.transform.parent = GameObject.Find(parent).transform;
        this.transform.localPosition = location;
        this.renderer.material.color = new Color(color.x, color.y, color.z);
    }

    [RPC]
    void PlaceNewBlock(Vector3 direction, NetworkMessageInfo info)
    {
        IPlayer player = TeamLoader.TeamManager.GetPlayer(new NetworkPlayerWrapper(info.sender));

        // todo: remove
        //if (player.HasPlaceableBlock)
        {
            Vector3 position = this.transform.localPosition + (direction * transform.localScale.x);
            player.Team.Tracker.PlaceBlock(player, position, player.HalfBlock.CalculateUnityColor());

            player.GiveNewInventoryBlock();
        }
    }

    [RPC]
    void RemoveBlock() { }
}
