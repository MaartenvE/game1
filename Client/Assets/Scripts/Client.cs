using UnityEngine;

public class Client
{
    private INetwork network;

    public Client(INetwork network)
    {
        this.network = network;
    }
	
	public NetworkConnectionError ConnectToServer(string ip, int port) 
    {
	    return network.Connect(ip, port);
	}
}