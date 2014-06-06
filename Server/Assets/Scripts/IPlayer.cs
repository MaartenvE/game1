using UnityEngine;
using System.Collections;

public interface IPlayer {
	//private NetworkView _networkView; //this is the networkview on which the player exists.
	
	//private Block _inventoryBlock; //this is the currently allocated block the player has.
	//private Time _startOfPenaltyTime; //this is the time the penalty started.
	//private Time _penaltyLength; //this is how long the penalty lasts.

    ITeam Team { get; set; }
    INetworkPlayer NetworkPlayer { get; }

	void GiveInventoryBlock();

	void DestroyInventoryBlock();




	[RPC]
	void GiveServerAnError(string errorMessage);

	void GivePlayerAnError(string errorMessage);
}
