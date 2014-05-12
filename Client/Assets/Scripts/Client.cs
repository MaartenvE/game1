using UnityEngine;
using System.Collections;
using AssemblyCSharp;


public class Client : MonoBehaviour {
	
	void Start (){
		ConnectToServer ();
	}
	
	void ConnectToServer() {
		Network.Connect("127.0.0.1", 3825);

	}
	
	void Update ()
	{
		if (false) {
						Vector3 location = new Vector3 (1, 1, 1);
						networkView.RPC ("PlaceBlock", RPCMode.Server, location);
				}
	}

	[RPC]
	BlockError PlaceBlock(Vector3 location){
		return null;
	}
}