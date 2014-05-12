using UnityEngine;
using System.Collections;

public class Client : MonoBehaviour {

	float time = 0;

	void Start (){
		Debug.Log ("test");
	}
	
	void ConnectToServer() {
		Network.Connect("127.0.0.1", 20000);
	}
	
	void Update ()
	{
		time += Time.deltaTime;
		if (!Network.isClient) {
			ConnectToServer();

		}
	}
	
	[RPC] 
	void PrintText (string text) {
	}
}