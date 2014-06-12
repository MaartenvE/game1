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
        Client.ConnectToServer(QRScanner.IP ?? IP, QRScanner.Port ?? Port);
    }

    [RPC]
    void Win(int teamId)
    {
        Client.RPC_Win(teamId);
    }
}
