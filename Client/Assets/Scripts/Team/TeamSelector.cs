using UnityEngine;

namespace BuildingBlocks.Team
{
    public class TeamSelector : MonoBehaviour
    {
        public static bool IsSpectator = false;

        void Start()
        {
            GameObject.Find("GuiOverlay").SetActive(!IsSpectator);
            GameObject.Find("RotatingBlockCamera").SetActive(!IsSpectator);
            GameObject.Find("BumpDetector").SetActive(!IsSpectator);
        }

        void OnConnectedToServer()
        {
            networkView.RPC("SelectTeam", RPCMode.Server, IsSpectator ? 1 : 0);
        }

        [RPC]
        void SelectTeam(int spectator) { }
    }
}
