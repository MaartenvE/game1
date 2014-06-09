
public class Server
{
    private INetwork network;

    public Server(INetwork network)
    {
        this.network = network;
    }

	public void Launch(int maxPlayers, int port, bool useNAT) {
		network.InitializeServer(maxPlayers, port, useNAT);
	}
}
