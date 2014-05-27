using UnityEngine;
using System.Collections;
using AssemblyCSharp;


public class Client : MonoBehaviour {

	public string ip = "127.0.0.1";
	public int port = 3825;

    private INetwork _network;
    private INetworkView _networkView;

    public INetwork network
    {
        get { return _network; }
        set { _network = value; }
    }

    public INetworkView networkView
    {
        get { return _networkView; }
        set { _networkView = value; }
    }

	public void Start (){
		ConnectToServer (ip, port);
	}
	
	public NetworkConnectionError ConnectToServer(string ip, int port) 
    {
	    return _network.Connect(ip, port);
	}

    public void OnGUI()
    {
        GUI.Box(new Rect(5, 5, Screen.width / 5, Screen.height / 4),"Server information");
        GUI.Label(new Rect(10, 30, Screen.width / 10, 20), "IP: ");
        GUI.Label(new Rect(10, 70, Screen.width / 10, 20), "Port: ");
        ip = GUI.TextField(new Rect(50, 30, Screen.width / 7,20), ip);
        port = int.Parse(GUI.TextField(new Rect(50, 70, Screen.width / 7, 20), ""+port));

        if(GUI.Button(new Rect(20, 110, Screen.width / 8, 20), "Connect")) {
                _network.Disconnect();
                DestroyAllBlocks();
                ConnectToServer(ip, port);
         
        }
    }

    public void DestroyAllBlocks()
    {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("block")){
            Destroy(g);
        }
    }

	[RPC]
	public void PlaceBlock(Vector3 location, Vector3 relativeLocation, NetworkViewID NVI){
	}

	[RPC]
	public void RemoveBlock(NetworkViewID NVI){
	}

    [RPC]
    public void ColorBlock(NetworkViewID NVI, Vector3 color)
    {
        GameObject block = _networkView.Find(NVI).gameObject();
        block.renderer.material.color = new Color(color.x, color.y, color.z);
    }


}