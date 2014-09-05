using UnityEngine;
using BuildingBlocks.Team;
using BuildingBlocks.Player;

namespace BuildingBlocks.Blocks
{
    public class BlockBehaviour : MonoBehaviour
    {
        protected ITeam team { get; private set; }

        public void SetInfo(int teamId, Vector3 location, Color color)
        {
            networkView.RPC("SetBlockInfo", RPCMode.AllBuffered, teamId, location, new Vector3(color.r, color.g, color.b));
        }

        [RPC]
        void SetBlockInfo(int teamId, Vector3 location, Vector3 color)
        {
            this.team = Team.Team.GetTeam(teamId);
            this.transform.parent = team.gameObject.transform.Transform;
            this.transform.localPosition = location;
            this.renderer.material.color = ColorModel.ConvertToUnityColor(color);
        }

        [RPC]
        void PlaceNewBlock(Vector3 direction, NetworkMessageInfo info)
        {
            IPlayer player = Player.Player.GetPlayer(new NetworkPlayerWrapper(info.sender));

            if (player.Team == this.team)
            {
                Vector3 position = this.transform.localPosition + (direction * transform.localScale.x);
                if (player.Team.StructureTracker.PlaceBlock(position, player.HalfBlock.CalculateUnityColor()))
                {
                    player.GiveNewInventoryBlock();
                }
            }
        }

        [RPC]
        void RemoveBlock() { }
    }
}
