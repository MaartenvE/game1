using UnityEngine;
using System.Collections;

public class Client
{
    private bool? won;
    private INetwork network;

    public Client(INetwork network)
    {
        this.network = network;
    }
	
	public NetworkConnectionError ConnectToServer(string ip, int port) 
    {
	    return network.Connect(ip, port);
	}

    public void OnGUI()
    {
        if (won != null)
        {
            string text = won.GetValueOrDefault()
                ? "Congratulations, your team won!"
                : "Game over";

            var style = GUI.skin.GetStyle("Label");
            style.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text);
        }
    }

    public void RPC_Win(int teamId)
    {
        int myTeam = GameObject.Find("Player").GetComponent<PlayerInfo>().Team;
        this.won = teamId == myTeam;

        System.Threading.Thread.Sleep(5000);

        Application.LoadLevel("Client");
    }
}
