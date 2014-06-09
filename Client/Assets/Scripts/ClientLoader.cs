using UnityEngine;

public class ClientLoader : MonoBehaviour
{
    public string IP;
    public int Port;

	void Start ()
    {
        Input.compass.enabled = true;
        Client client = new Client(new NetworkWrapper());
        client.ConnectToServer(IP, Port);
	}
}
