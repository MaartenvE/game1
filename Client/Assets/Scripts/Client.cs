using UnityEngine;
using System.Collections;
using AssemblyCSharp;


public class Client : MonoBehaviour {

	public string ip = "127.0.0.1";
	public int port = 3825;


	void Start (){
		ConnectToServer ();
	}
	
	void ConnectToServer() {
		Network.Connect(ip, port);

	}
	
	void Update ()
	{
		if (false) {
						Vector3 location = new Vector3 (1, 1, 1);
						networkView.RPC ("PlaceBlock", RPCMode.Server, location);
				}
	}

	[RPC]
	void PlaceBlock(Vector3 location, Vector3 relativeLocation, NetworkViewID NVI){
		//return null;
	}

	[RPC]
	void RemoveBlock(NetworkViewID NVI){

	}


}