using UnityEngine;
using System.Collections;

public class Client
{
    private const float TEXT_SIZE = 0.1f;

    private bool? won;
    private INetwork network;

    private GUIStyle style;

    public Client(INetwork network)
    {
        this.network = network;
    }
	
	public NetworkConnectionError ConnectToServer(string ip, int port) 
    {
	    return network.Connect(ip, port);
	}

	public void OnDisconnectedFromServer(NetworkDisconnection info) {
		if (info == NetworkDisconnection.LostConnection)
						Application.LoadLevel (0);
		else
			Debug.Log("Successfully diconnected from the server");
	}

    public void OnGUI()
    {
        if (won != null)
        {
            GameObject crosshair = GameObject.Find("Crosshair");
            if (crosshair != null)
            {
                crosshair.SetActive(false);
            }

            string text = won.GetValueOrDefault()
                ? "Congratulations, your team won!"
                : "Game over";

            setStyle();

            GUI.color = won.GetValueOrDefault() ? Color.green : Color.red;
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text, style);
        }
    }

    private void setStyle()
    {
        if (style == null)
        {
            style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter;
        }

        style.fontSize = (int)(Screen.width * TEXT_SIZE);
    }

    public void RPC_Win(int teamId)
    {
        int myTeam = PlayerInfo.Team;
        this.won = teamId == myTeam;
    }
}
