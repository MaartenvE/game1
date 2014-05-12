using UnityEngine;
using System.Collections;
using AssemblyCSharp;


public class Server : MonoBehaviour {
	
	public GameObject prefab;
	
	void Start (){
		LaunchServer ();
	}
	
	void LaunchServer() {
		Network.InitializeServer(32, 3825, false);
		
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	[RPC] 
	void PrintText (string text) {
		Debug.Log(text);
	}
	
	
	[RPC]
	BlockError PlaceBlock(Vector3 location){
		Network.Instantiate (prefab, location, prefab.transform.rotation, 1);
		return null;
	}
	
	[RPC]
	BlockError RemoveBlock(Vector3 location){	
		return null;
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