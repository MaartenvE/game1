using UnityEngine;
using BuildingBlocks.CubeFinger;
using BuildingBlocks.Team;

namespace BuildingBlocks.Player
{
    public interface IPlayer : IBuildingBlocksBehaviour
    {
        ITeam Team { get; set; }
        INetworkPlayer NetworkPlayer { get; }
        ICubeFinger CubeFinger { get; }
        HalfBlock HalfBlock { get; }
        bool HasPlaceableBlock { get; }

        void CombineBlock(IPlayer other);
        void GiveNewInventoryBlock();
        void InstantiateCubeFinger();
    }
}
