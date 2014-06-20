using UnityEngine;
using BuildingBlocks.Team;

namespace BuildingBlocks.GUI
{
    public class TeamProgressOverlayLoader : MonoBehaviour
    {
        private TeamProgressOverlay overlay;

        void OnNetworkInstantiate(NetworkMessageInfo info)
        {
            overlay = new TeamProgressOverlay(gameObject.GetComponent<TeamLoader>().Team);
        }

        void OnGUI()
        {
            overlay.OnGUI();
        }
    }
}
