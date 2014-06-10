using UnityEngine;

/// <summary>
/// Server loader creates a new server using the NetworkWrapper.
/// </summary>
public class ServerLoader : MonoBehaviour
{
    public int MaxPlayers = 32;
    public int Port = 3825;
    public bool UseNAT = false;

	void Start ()
    {

        Server server = new Server(new NetworkWrapper());
        server.Launch(MaxPlayers, Port, UseNAT);
	}
}
