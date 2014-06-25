using UnityEngine;
using System.Collections;
using BuildingBlocks.Player;
using BuildingBlocks.GUI;

namespace BuildingBlocks.Client
{
	public class Client
    {
        private enum EndStatus
        {
            None,
            Lost,
            Draw,
            Won
        };

        private const float TEXT_SIZE = 0.1f;

		private bool disconnected = false;

        private EndStatus endStatus;

        private INetwork network;

        private GUIStyle style;

		private static float width = Screen.width / 2;
		private static float height = Screen.height / 2;

		private Rect windowRect = new Rect(Screen.width/2 - width/2, Screen.height/2 - height/2, width, height);

        public Client(INetwork network)
        {
            this.network = network;
        }

        public NetworkConnectionError ConnectToServer(string ip, int port)
        {
            return network.Connect(ip, port);
        }

        public void OnDisconnectedFromServer(NetworkDisconnection info)
        {
            if (info == NetworkDisconnection.LostConnection) 
			{
				Debug.Log ("Disconnected unexpectedly from the server");
				disconnected = true;

			} 
			else 
			{
				Debug.Log ("Successfully disconnected from the server");
			}
        }

		void PopUp() 
		{
			UnityEngine.GUI.Box(new Rect( Screen.width/2 - width/2, Screen.height/2 - height/2, width, height), "Disconnected from Server", GUIStyles.QRStyle(Screen.height,Screen.width) );
			if (UnityEngine.GUI.Button (new Rect (Screen.width/2 - width/2, Screen.height/2 - height/4, width, height), "Tap to return to QR scanner", GUIStyles.ButtonStyle(Screen.height-height,Screen.width-width))) 
			{
				Application.LoadLevel("QRCodeScanner");
			}
		}

        public void OnGUI()
        {
			if (disconnected) 
			{
				PopUp ();
			}
     
            if (endStatus != EndStatus.None)
			{
                string text = "";
                Color color = Color.black;
                switch (endStatus)
                {
                    case EndStatus.Lost:
                        text = "Game over";
                        color = Color.red;
                        break;
                    case EndStatus.Draw:
                        text = "It's a draw!";
                        color = Color.yellow;
                        break;
                    case EndStatus.Won:
                        text = "Congratulations, your team won!";
                        color = Color.green;
                        break;
                }
                disableCrosshair();
                drawText(text, color);
            }
        }

        private void disableCrosshair()
        {
            GameObject crosshair = GameObject.Find("Crosshair");
            if (crosshair != null)
            {
                crosshair.SetActive(false);
            }
        }

        private void drawText(string text, Color color)
        {
            setStyle();
            UnityEngine.GUI.color = color;
            UnityEngine.GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text, style);
        }

        private void setStyle()
        {
            if (style == null)
            {
                style = new GUIStyle(UnityEngine.GUI.skin.label);
                style.alignment = TextAnchor.MiddleCenter;
            }

            style.fontSize = (int)(Screen.width * TEXT_SIZE);
        }

        public void RPC_Win(int teamId)
        {
            if (Player.Player.LocalPlayer.Team != null)
            {
                int myTeam = Player.Player.LocalPlayer.Team.TeamId;
                if (teamId == -1)
                {
                    endStatus = EndStatus.Draw;
                }
                else if (teamId == myTeam)
                {
                    endStatus = EndStatus.Won;
                }
                else
                {
                    endStatus = EndStatus.Lost;
                }
            }
        }
    }
}
