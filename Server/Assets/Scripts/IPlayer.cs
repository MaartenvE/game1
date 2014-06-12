using UnityEngine;

public interface IPlayer
{
    ITeam Team { get; set; }
    INetworkPlayer NetworkPlayer { get; }

    CubeFingerBehaviour CubeFinger { get; set; }

    HalfBlock HalfBlock { get; set; }
    bool HasPlaceableBlock { get; set; }

	void GiveInventoryBlock();

	void DestroyInventoryBlock();


	[RPC]
	void GiveServerAnError(string errorMessage);

	void GivePlayerAnError(string errorMessage);

    void CombineBlock(IPlayer other);

    void GiveNewInventoryBlock();
}
