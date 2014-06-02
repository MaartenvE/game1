using UnityEngine;
using System.Collections;


public class Player : IPlayer{

	private INetworkView _NetworkView; //this is the networkview on which the player exists.
	private INetwork _Network;

	private NetworkPlayer _NetworkPlayer; //this is the actual networkPlayer

	private IInstantiatedBlock _Finger;
	private IBlock _InventoryBlock; //this is the currently allocated block the player has.

	private Time _StartOfPenaltyTime; //this is the time the penalty started.
	private Time _PenaltyLength; //this is how long the penalty lasts.
	

	public Player(INetworkView networkView, INetwork network, NetworkPlayer networkPlayer){
		_NetworkView = networkView;
		_Network = network;
        _NetworkPlayer = networkPlayer;
        InstantiateFinger();
	}


	private void InstantiateFinger(){
		GameObject prefab = Resources.Load ("TestCube") as GameObject;
		GameObject finger = GameObject.Instantiate (prefab, new Vector3(0,0,0), prefab.transform.rotation);
	}

	public void GiveInventoryBlock(){
		throw new UnityException("not yet implemented");
	}


	public void DestroyInventoryBlock(){
		throw new UnityException("not yet implemented");
	}

	//updates the finger
	[RPC]
	public void UpdateFinger(Vector3 newLocation){
		_Finger.SetLocation (newLocation);
	}


    public void givePlayerAColor(Vector3 color){
        _Finger.SetColor(color);
        _NetworkView.RPC("GivePlayerAColor", _NetworkPlayer, color);
    }
	

	//incoming error
	[RPC]
	public void GiveServerAnError(string errorMessage){
		Debug.LogError (errorMessage);
	}

	//outgoing error
	public void GivePlayerAnError(string errorMessage){
		_NetworkView.RPC ("GivePlayerAnError", _NetworkPlayer , errorMessage);
	}
	
}
