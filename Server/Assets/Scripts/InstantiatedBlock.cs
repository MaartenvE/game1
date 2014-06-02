using UnityEngine;
using System.Collections;

public class InstantiatedBlock : Block {

	NetworkViewID _NetworkViewID;
	Vector3 _Location;


	public InstantiatedBlock(NetworkViewID networkViewID) : base(){
		_NetworkViewID = networkViewID;
	}

	public void SetLocation(Vector3 location) {
		_Location = location;
	}

	public Vector3 GetLocation() {
		return _Location;
	}

	public NetworkViewID GetNetworkViewID() {
		return _NetworkViewID;
	}

}
