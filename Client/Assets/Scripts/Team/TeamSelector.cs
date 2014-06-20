using UnityEngine;

namespace BuildingBlocks.Team
{
    public class TeamSelector : MonoBehaviour
    {
        public static bool IsSpectator = false;

        void OnConnectedToServer()
        {
            networkView.RPC("SelectTeam", RPCMode.Server, IsSpectator ? 1 : 0);
        }

        [RPC]
        void SelectTeam(int spectator) { }
    }
}
