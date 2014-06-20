using UnityEngine;

namespace BuildingBlocks.Server
{
    public class ServerLoader : MonoBehaviour
    {
        public int MaxPlayers = 32;
        public int Port = 3825;
        public bool UseNAT = false;

        public static Server Server { get; private set; }

        void Start()
        {
            Server = new Server(new NetworkWrapper(), new NetworkViewWrapper(networkView));
            Server.Launch(MaxPlayers, Port, UseNAT);
        }

        [RPC]
        void Win(int teamId) { }

        [RPC]
        void SetTime(int secondsLeft) { }
    }
}
