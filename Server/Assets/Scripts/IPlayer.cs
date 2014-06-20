using UnityEngine;
using BuildingBlocks.CubeFinger;
using BuildingBlocks.HalfBlock;

public interface IPlayer
{
    ITeam Team { get; set; }
    INetworkPlayer NetworkPlayer { get; }

    ICubeFinger CubeFinger { get; set; }

    HalfBlock HalfBlock { get; set; }
    bool HasPlaceableBlock { get; set; }

	void GiveInventoryBlock();

	void DestroyInventoryBlock();

    void CombineBlock(IPlayer other);

    void GiveNewInventoryBlock();
}
