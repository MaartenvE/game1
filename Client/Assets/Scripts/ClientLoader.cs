using UnityEngine;
using System.Collections;

public class ClientLoader : MonoBehaviour {

    
    

	void Start () {
		string ip = QRScanner.adressText;
		int port = QRScanner.portNum;
        
		INetwork networkWrapper = new NetworkWrapper();
        INetworkView networkViewWrapper = new NetworkViewWrapper();

        NetworkView nativeNetworkView = gameObject.GetComponent<NetworkView>();

        networkViewWrapper.SetNativeNetworkView(nativeNetworkView);

        Client client = this.gameObject.AddComponent<Client>();
        client.network = networkWrapper;
        client.networkView = networkViewWrapper;
        client.ip = ip;
        client.port = port;        
	}
}
