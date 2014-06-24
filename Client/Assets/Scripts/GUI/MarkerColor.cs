using UnityEngine;
using BuildingBlocks.Team;
using BuildingBlocks.Player;

namespace BuildingBlocks.GUI
{
    public class MarkerColor : MonoBehaviour
    {
        void Start()
        {
            Player.Player.LocalPlayer.OnTeamChange += changeTeam;
            if (Player.Player.LocalPlayer.Team != null)
            {
                changeTeam(Player.Player.LocalPlayer.Team);
            }
            else
            {
                colorTarget(false);
            }
        }

        private void changeTeam(ITeam team)
        {
            bool isMine = team.Target == transform.parent.name;
            colorTarget(isMine);
        }

        private void colorTarget(bool isMine)
        {
            Color color = isMine ? Color.green : Color.red;
            foreach (Transform markerpart in transform)
            {
                markerpart.renderer.material.color = color;
            }
        }
    }
}
