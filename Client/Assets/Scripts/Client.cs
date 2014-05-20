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

	[RPC]
	void PlaceBlock(Vector3 location, Vector3 relativeLocation, NetworkViewID NVI){
		//return null;
	}

	[RPC]
	void RemoveBlock(NetworkViewID NVI){

	}

    [RPC]
    public void ColorBlock(NetworkViewID NVI, float r, float g, float b)
    {
        GameObject block = NetworkView.Find(NVI).gameObject;
        block.renderer.material.color = new Color(r, g, b);
    }


}