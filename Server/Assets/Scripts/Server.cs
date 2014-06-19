using UnityEngine;

public class Server
{
    private INetwork network;
    private INetworkView networkView;

    public Server(INetwork network, INetworkView networkView)
    {
        this.network = network;
        this.networkView = networkView;
    }

	public void Launch(int maxPlayers, int port, bool useNAT) {
		network.InitializeServer(maxPlayers, port, useNAT);
    }

    public void Win(int team)
    {
        networkView.RPC("Win", RPCMode.OthersBuffered, team);
        network.SetSendingEnabled(1, false);
        network.isMessageQueueRunning = false;
    }

    public void TimeUp()
    {
        Win(0);
    }
}
