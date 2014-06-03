using UnityEngine;
using System.Collections;


public class Player : IPlayer{

<<<<<<< HEAD
	private INetworkView _networkView; //this is the networkview on which the player exists.
	private INetwork _Network;
	private INetworkPlayer _NetworkPlayer; //this is the actual networkPlayer

	private IInstantiatedBlock _Finger;
	//private IBlock _InventoryBlock; //this is the currently allocated block the player has.

	//private Time _StartOfPenaltyTime; //this is the time the penalty started.
	//private Time _PenaltyLength; //this is how long the penalty lasts.
	

	public Player(INetworkView networkView, INetwork network, INetworkPlayer networkPlayer){
		_networkView = networkView;
=======
	private INetworkView _NetworkView; //this is the networkview on which the player exists.
	private INetwork _Network;

	private NetworkPlayer _NetworkPlayer; //this is the actual networkPlayer

	private IInstantiatedBlock _Finger;
	private IBlock _InventoryBlock; //this is the currently allocated block the player has.

	private Time _StartOfPenaltyTime; //this is the time the penalty started.
	private Time _PenaltyLength; //this is how long the penalty lasts.
	

	public Player(INetworkView networkView, INetwork network, NetworkPlayer networkPlayer){
		_NetworkView = networkView;
>>>>>>> 505cab0c1b03405b9bb5caa0a05db20c3762f808
		_Network = network;
        _NetworkPlayer = networkPlayer;
        InstantiateFinger();
	}


	private void InstantiateFinger(){
		GameObject prefab = Resources.Load ("TestCube") as GameObject;
<<<<<<< HEAD
		GameObject finger = _Network.Instantiate (prefab, new Vector3(0,0,0), prefab.transform.rotation, 1) as GameObject;
        _Finger = new InstantiatedBlock(finger) as IInstantiatedBlock;
        Vector3 color = new Vector3(0, 0, 0);
		finger.layer = 2;


		//this tells the client that this is the players personal finger. (as oposed to the other fingers)
        _networkView.RPC("InstantiateFinger", RPCMode.OthersBuffered, finger.networkView.viewID, color);
		_networkView.RPC ("InstantiatePersonalFinger", _NetworkPlayer, finger.networkView.viewID);

		//now color the block correctly
		_networkView.RPC ("ColorBlock", RPCMode.AllBuffered, finger.networkView.viewID, color);
		finger.renderer.material.color = new Color(0.8f,0.05f,0.8f,0.08f);
	}



=======
		GameObject finger = GameObject.Instantiate (prefab, new Vector3(0,0,0), prefab.transform.rotation);
	}

>>>>>>> 505cab0c1b03405b9bb5caa0a05db20c3762f808
	public void GiveInventoryBlock(){
		throw new UnityException("not yet implemented");
	}


	public void DestroyInventoryBlock(){
		throw new UnityException("not yet implemented");
	}

	//updates the finger
<<<<<<< HEAD
	/*[RPC]
	public void UpdateFinger(Vector3 newLocation){
		_Finger.SetLocation (newLocation);
	}*/
=======
	[RPC]
	public void UpdateFinger(Vector3 newLocation){
		_Finger.SetLocation (newLocation);
	}
>>>>>>> 505cab0c1b03405b9bb5caa0a05db20c3762f808


    public void givePlayerAColor(Vector3 color){
        _Finger.SetColor(color);
<<<<<<< HEAD
        _networkView.RPC("GivePlayerAColor", _NetworkPlayer, color);
=======
        _NetworkView.RPC("GivePlayerAColor", _NetworkPlayer, color);
>>>>>>> 505cab0c1b03405b9bb5caa0a05db20c3762f808
    }
	

	//incoming error
	[RPC]
	public void GiveServerAnError(string errorMessage){
		Debug.LogError (errorMessage);
	}

	//outgoing error
	public void GivePlayerAnError(string errorMessage){
<<<<<<< HEAD
		_networkView.RPC ("GivePlayerAnError", _NetworkPlayer , errorMessage);
=======
		_NetworkView.RPC ("GivePlayerAnError", _NetworkPlayer , errorMessage);
>>>>>>> 505cab0c1b03405b9bb5caa0a05db20c3762f808
	}
	
}
