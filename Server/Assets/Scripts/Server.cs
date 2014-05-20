using UnityEngine;
using System.Collections.Generic;
using AssemblyCSharp;


public class Server : MonoBehaviour {

    const float MAX_BUMP_TIME = 2.0f;
	
	public GameObject prefab;
	public int port = 3825;


    LinkedList<Bump> bumpHistory = new LinkedList<Bump>();

	
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
	Block Tap(float networkTime, float compassHeading, Quaternion attitude, NetworkMessageInfo info){
		Debug.Log ("Received Bump Detection");
        Bump bump = new Bump(networkTime, compassHeading, attitude);

        // Iterate through previous bumps
        LinkedListNode<Bump> node = bumpHistory.First;
        while (node != null)
        {
            var next = node.Next;
			var oldBump = node.Value;
			Debug.Log ("Received time: " + networkTime + "; Current time: " + Network.time + "; Relative time: " + info.timestamp);
            if (oldBump.Time < Network.time - MAX_BUMP_TIME)
            {
				Debug.Log ("Removed old bump");
                bumpHistory.Remove(oldBump);
            }
            else
            {
                Debug.Log("Bump detected!");
				bumpHistory.Remove (oldBump);
            }
            node = next;
        }

		bumpHistory.AddFirst(bump);
		return null;
	}
	
	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("Player connected from " + player.ipAddress);
	}
	
	void OnServerInitialized() {
		Debug.Log("Server initialized and ready");
	}
}