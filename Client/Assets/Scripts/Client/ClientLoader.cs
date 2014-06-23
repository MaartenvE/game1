using UnityEngine;

namespace BuildingBlocks.Client
{
    public class ClientLoader : MonoBehaviour
    {
        public string IP;
        public int Port;

        public Client Client { get; private set; }

        void Start()
        {
            UnityEngine.Input.compass.enabled = true;
            Client = new Client(new NetworkWrapper());
            Client.ConnectToServer(QRScanner.IP ?? IP, QRScanner.Port ?? Port);
        }

        void OnGUI()
        {
            Client.OnGUI();
        }

        void Restart()
        {
            Application.LoadLevel("Client");
        }

		public void OnDisconnectedFromServer(NetworkDisconnection info){
			Client.OnDisconnectedFromServer (info);
		}

        [RPC]
        void Win(int teamId)
        {
            Client.RPC_Win(teamId);
            Network.SetSendingEnabled(1, false);
            Invoke("Restart", 5);
        }
    }
}
