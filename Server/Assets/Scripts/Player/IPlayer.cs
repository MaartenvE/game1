using UnityEngine;
using BuildingBlocks.Team;
using BuildingBlocks.Blocks;
using BuildingBlocks.CubeFinger;

namespace BuildingBlocks.Player
{
    public interface IPlayer : IBuildingBlocksBehaviour
    {
        ITeam Team { get; set; }
        INetworkPlayer NetworkPlayer { get; }
        ICubeFinger CubeFinger { get; }
        Block Block { get; }

        void SetBlock(Block block);
        void GiveBlock();
        bool CombineBlock(IPlayer other);
        void InstantiateCubeFinger();
    }
}
