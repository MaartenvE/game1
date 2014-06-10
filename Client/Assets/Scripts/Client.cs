using UnityEngine;
using System.Collections;

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

    public void RPC_Win(int teamId)
    {
        int myTeam = GameObject.Find("Player").GetComponent<PlayerInfo>().Team;
        if (teamId == myTeam)
        {
            Debug.LogError("You won!");
        }

        else
        {
            Debug.LogError("You lost :(");
        }

        System.Threading.Thread.Sleep(5000);

        Application.LoadLevel("Client");
    }
}
