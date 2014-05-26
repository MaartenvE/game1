using UnityEngine;
using AssemblyCSharp;


public class Client : MonoBehaviour {

	public string ip = "145.94.197.173";
	public int port = 36963;

	bool failed = false;
	bool ready  = false;

	void OnGUI() {
		if (failed) {
			GUI.Label (new Rect(10, 10, 500, 20), "Failed to connect");
		} else {
			GUI.Label (new Rect(10, 10, 500, 20), "Magnetometer: " + Input.compass.rawVector.magnitude);
		}
	}


	void Start (){
		Input.compass.enabled = true;
		ConnectToServer ();
	}
	
	void ConnectToServer() {
		Network.Connect(ip, port);

	}

	void OnConnectedToServer() {
		BumpDetectorLoader.Detector.OnBump +=
			(bump) => Handheld.Vibrate();
		BumpDetectorLoader.Detector.OnBump += 
			(Bump bump) => networkView.RPC ("Tap", RPCMode.Server, bump.Force);

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
	Block Tap(float force)
	{
		return null;
	}

	void RemoveBlock(NetworkViewID NVI){

	}
}