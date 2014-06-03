using UnityEngine;
using System.Collections;

public interface IPlayer {
	//private NetworkView _networkView; //this is the networkview on which the player exists.
	
	//private Block _inventoryBlock; //this is the currently allocated block the player has.
	//private Time _startOfPenaltyTime; //this is the time the penalty started.
	//private Time _penaltyLength; //this is how long the penalty lasts.

	void GiveInventoryBlock();

	void DestroyInventoryBlock();

<<<<<<< HEAD
	//[RPC]
	//void UpdateFinger(Vector3 newLocation);
=======
	[RPC]
	void UpdateFinger(Vector3 newLocation);
>>>>>>> 505cab0c1b03405b9bb5caa0a05db20c3762f808

	[RPC]
	void GiveServerAnError(string errorMessage);

	void GivePlayerAnError(string errorMessage);
}
