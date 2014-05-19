using UnityEngine;
using System.Collections;
using AssemblyCSharp;


public class Server : MonoBehaviour {
	
	public GameObject prefab;
	public int port = 3825;

	public BlockMatrix blockMatrix = new BlockMatrix();

	
	void Start (){
		LaunchServer ();
	}
	
	void LaunchServer() {
		Network.InitializeServer(32, port, false);
		
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	[RPC] 
	void PrintText (string text) {
		Debug.Log(text);
	}
	
	
	[RPC]
	void PlaceBlock(Vector3 location, Vector3 matrixLocation, NetworkViewID NVI){

		GameObject block = (GameObject)Network.Instantiate (prefab, location, prefab.transform.rotation, 1);

		GameObject sideBlock = NetworkView.Find (NVI).gameObject;

		block.GetComponent<location> ().index = sideBlock.GetComponent<location> ().index + matrixLocation;

		Debug.Log (block.GetComponent<location> ().index);
	}
	
	[RPC]
	void RemoveBlock(NetworkViewID NVI){	
		Network.Destroy (NVI);
	}
	
	[RPC]
	Block RequestBlock(){
		return null;
	}
	
	[RPC]
	void ThrowAwayBlock(){
	}
	
	[RPC] 
	//Decide: magnetic or true heading. Is compas heading already contained in the attitude from gyro?
	Block Tap(double networkTime, float compasHeading, Quaternion attitude){
		return null;
	}
	
	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("Player connected from " + player.ipAddress);
	}
	
	void OnServerInitialized() {
		Debug.Log("Server initialized and ready");
	}
}