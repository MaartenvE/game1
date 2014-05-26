using UnityEngine;
using System.Collections;
using AssemblyCSharp;

/// <summary>
/// Server loader creates a new server using the NetworkWrapper and prefab object.
/// </summary>
public class ServerLoader : MonoBehaviour {

	private Server server;
	public GameObject Prefab; 
	public int port = 3825;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		INetwork network = new NetworkWrapper ();
		INetworkView networkView = new NetworkViewWrapper ();

		NetworkView nativeNetworkView = this.GetComponent<NetworkView> ();
		
		networkView.SetNativeNetworkView (nativeNetworkView);
		server = gameObject.AddComponent<Server>();
		server.port = port;
		server.network = network;
		server.networkView = networkView;
		server.LaunchServer ();
	}

}
	