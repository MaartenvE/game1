using UnityEngine;
using System.Collections;
using AssemblyCSharp;


public class Client : MonoBehaviour {

	public string ip = "145.94.197.173";
	public int port = 36963;

	bool failed = false;
	bool ready  = false;

	void OnGUI() {
		if (failed) {
			GUI.Label (new Rect(10, 10, 100, 20), "Failed to connect");
		}

		if (ready) {
			if (GUI.Button (new Rect(10, 30, 100, 20), "Click to send bump!")) {
				networkView.RPC ("Tap", RPCMode.Server, (float) Network.time, 0.0f, Quaternion.identity);
			}
		}
	}


	void Start (){
		ConnectToServer ();
	}
	
	void ConnectToServer() {
		Network.Connect(ip, port);

	}

	void OnConnectedToServer() {
		BumpDetectorLoader.Detector.OnBump += 
			(Bump bump) => networkView.RPC ("Tap", RPCMode.Server, (float) Network.time, 0.0f, Quaternion.identity);

		ready = true;
	}

	void OnFailedToConnect() {
		failed = true;
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
	Block Tap(float networkTime, float compassHeading, Quaternion attitude)
	{
		Debug.Log (networkTime);
		return null;
	}

	void RemoveBlock(NetworkViewID NVI){

	}
}