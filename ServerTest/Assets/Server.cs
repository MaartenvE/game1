using UnityEngine;
using System.Collections;


public class Server : MonoBehaviour {

	public GameObject prefab;

	void Start (){
		LaunchServer ();
	}

	void OnNetworkInstantiate(NetworkMessageInfo info) {
		Debug.Log("New object instantiated by " + info.sender);
	}

	void LaunchServer() {
		Network.InitializeServer(32, 3825, false);

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			Debug.Log("New block created");
			Vector3 test = new Vector3 ((float)(Random.Range (0, 1000)/10.0), (float)(Random.Range (0, 1000)/10.0), (float)(Random.Range (0, 1000)/10.0));
			Network.Instantiate (prefab, test, prefab.transform.rotation, 1);
		}
	}

	[RPC] 
	void PrintText (string text) {
		Debug.Log(text);
	}

	/*
	[RPC]
	BlockError PlaceBlock(Vector3 location){
	}

	[RPC]
	BlockError RemoveBlock(Vector3 location){	
	}

	[RPC]
	Block RequestBlock(){
	}

	[RPC]
	void ThrowAwayBlock(){
	}

	[RPC] 
	//Decide: magnetic or true heading. Is compas heading already contained in the attitude from gyro?
	Block Tap(double networkTime, float compasHeading, Quaternion attitude){
	}
	*/

	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("Player connected from " + player.ipAddress);

	}
	
	void OnServerInitialized() {
		Debug.Log("Server initialized and ready");
	}
}