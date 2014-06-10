using UnityEngine;

public class ClientLoader : MonoBehaviour
{
    public string IP;
    public int Port;

    public Client Client { get; private set; }

    void Start()
    {
        Input.compass.enabled = true;
        Client = new Client(new NetworkWrapper());
        Client.ConnectToServer(IP, Port);
    }

    [RPC]
    void Win(int teamId)
    {
        Client.RPC_Win(teamId);
    }
}
