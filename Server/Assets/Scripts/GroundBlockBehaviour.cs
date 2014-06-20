using UnityEngine;
using BuildingBlocks.Team;
using BuildingBlocks.Player;
using BuildingBlocks;

public class GroundBlockBehaviour : MonoBehaviour
{
    protected ITeam team { get; private set; }

    public void SetInfo(int teamId, Vector3 location, Color color)
    {
        networkView.RPC("SetBlockInfo", RPCMode.AllBuffered, teamId, location, new Vector3(color.r, color.g, color.b));
    }

    [RPC]
    void SetBlockInfo(int teamId, Vector3 location, Vector3 color)
    {
        this.team = Team.GetTeam(teamId);
        this.transform.parent = team.gameObject.transform.Transform;
        this.transform.localPosition = location;
        this.renderer.material.color = ColorModel.ConvertToUnityColor(color);


        //this.transform.parent = GameObject.Find(parent).transform;
        //this.transform.localPosition = location;
        //this.renderer.material.color = new Color(color.x, color.y, color.z);
    }

    [RPC]
    void PlaceNewBlock(Vector3 direction, NetworkMessageInfo info)
    {
        IPlayer player = Player.GetPlayer(new NetworkPlayerWrapper(info.sender));

        if (player.Team == this.team)
        {
            Vector3 position = this.transform.localPosition + (direction * transform.localScale.x);
            player.Team.StructureTracker.PlaceBlock(position, player.HalfBlock.CalculateUnityColor());
            player.GiveNewInventoryBlock();
        }

        //IPlayer player = TeamLoader.TeamManager.GetPlayer(new NetworkPlayerWrapper(info.sender));

        // todo: remove
        //if (player.HasPlaceableBlock)
        //{
        //    Vector3 position = this.transform.localPosition + (direction * transform.localScale.x);
        //    player.Team.Tracker.PlaceBlock(player, position, player.HalfBlock.CalculateUnityColor());
        //
        //    player.GiveNewInventoryBlock();
        //}
    }

    [RPC]
    void RemoveBlock() { }
}
