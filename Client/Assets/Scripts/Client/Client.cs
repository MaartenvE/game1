using UnityEngine;
using System.Collections;
using BuildingBlocks.Player;
using BuildingBlocks.GUI;

namespace BuildingBlocks.Client
{
	public class Client
    {
        private const float TEXT_SIZE = 0.1f;

		private bool disconnected = false;
        private bool? won;
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
            if (info == NetworkDisconnection.LostConnection) {
				Debug.Log ("Disconnected unexpectedly from the server");
				disconnected = true;

			} 
			else {
				Debug.Log ("Successfully disconnected from the server");
			}
        }

		void PopUp() {
			UnityEngine.GUI.Box(new Rect( Screen.width/2 - width/2, Screen.height/2 - height/2, width, height), "Disconnected from Server", GUIStyles.QRStyle(Screen.height,Screen.width) );
			if (UnityEngine.GUI.Button (new Rect (Screen.width/2 - width/2, Screen.height/2 - height/4, width, height), "Tap to return to QR scanner", GUIStyles.ButtonStyle(Screen.height/1.3f,Screen.width/1.3f))) {
				Application.LoadLevel (0);
			}
		}

        public void OnGUI()
        {
			if (disconnected) {
				PopUp ();
			}
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

                UnityEngine.GUI.color = won.GetValueOrDefault() ? Color.green : Color.red;
                UnityEngine.GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text, style);
            }
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
            int myTeam = Player.Player.LocalPlayer.Team.TeamId;
            this.won = teamId == myTeam;
        }
    }
}
