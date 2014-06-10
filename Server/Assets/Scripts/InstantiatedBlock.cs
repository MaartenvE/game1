using UnityEngine;
using System.Collections;

public class InstantiatedBlock : Block {

	NetworkViewID _NetworkViewID;

	//Vector3 _Location;
    private GameObject _Block;

	public InstantiatedBlock(NetworkViewID networkViewID) : base(){
		_NetworkViewID = networkViewID;
        _Block = NetworkView.Find(_NetworkViewID).gameObject;
	}

    public InstantiatedBlock(GameObject block) :base() {
        _Block = block;
    }



	public void SetLocation(Vector3 location) {
		_Block.transform.position = location;
	}

	public Vector3 GetLocation() {
				return _Block.transform.position;
	}


	public NetworkViewID GetNetworkViewID() {
		return _NetworkViewID;
	}

}
