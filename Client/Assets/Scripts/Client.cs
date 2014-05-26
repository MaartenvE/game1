using UnityEngine;
using System.Collections;
using AssemblyCSharp;


public class Client : MonoBehaviour {

	public string ip = "127.0.0.1";
	public int port = 3825;

	void Start (){
		ConnectToServer (ip, port);
	}
	
	void ConnectToServer(string ip, int port) {
		Network.Connect(ip, port);

	}

    void OnGUI()
    {
        GUI.Box(new Rect(5, 5, Screen.width / 5, Screen.height / 4),"Server information");
        GUI.Label(new Rect(10, 30, Screen.width / 10, 20), "IP: ");
        GUI.Label(new Rect(10, 70, Screen.width / 10, 20), "Port: ");
        ip = GUI.TextField(new Rect(50, 30, Screen.width / 7,20), ip);
        port = int.Parse(GUI.TextField(new Rect(50, 70, Screen.width / 7, 20), ""+port));

        if(GUI.Button(new Rect(20, 110, Screen.width / 8, 20), "Connect")) {
            Network.Disconnect();
            ConnectToServer(ip, port);
        }
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