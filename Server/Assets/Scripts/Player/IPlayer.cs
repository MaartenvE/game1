using UnityEngine;
using BuildingBlocks.Team;
using BuildingBlocks.HalfBlock;
using BuildingBlocks.CubeFinger;

namespace BuildingBlocks.Player
{
    public interface IPlayer : IBuildingBlocksBehaviour
    {
        ITeam Team { get; set; }
        INetworkPlayer NetworkPlayer { get; }
        ICubeFinger CubeFinger { get; }
        HalfBlock.HalfBlock HalfBlock { get; }
        bool HasPlaceableBlock { get; }

        void CombineBlock(IPlayer other);
        void SetPlaceableBlock(HalfBlock.HalfBlock block);
        void GiveNewInventoryBlock();
        void InstantiateCubeFinger();
    }
}
