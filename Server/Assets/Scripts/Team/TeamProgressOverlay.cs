using UnityEngine;
using System.Collections;

namespace BuildingBlocks.Team
{
    public class TeamProgressOverlay
    {
        private ITeam team;

        private const float PROGRESSBAR_HEIGHT = 0.02f;
        private const float PROGRESSBAR_WIDTH = 0.15f;
        private const float PROGRESSBAR_PADDING = 0.2f;

        public TeamProgressOverlay(ITeam team)
        {
            this.team = team;
        }

        public void OnGUI()
        {
            string imageTarget = team.Target;
            if (imageTarget != null)
            {
                GameObject target = GameObject.Find(imageTarget);
                Camera camera = target.GetComponentInChildren<Camera>();
                Vector3 location = camera.WorldToScreenPoint(target.transform.position);

                drawProgressBar(location);
            }
        }

        private void drawProgressBar(Vector3 location)
        {
            float width = Screen.width * PROGRESSBAR_WIDTH;

            Rect position = new Rect(
                location.x - width / 2,
                Screen.height - Screen.height * PROGRESSBAR_PADDING - Screen.width * PROGRESSBAR_HEIGHT,
                width,
                Screen.width * PROGRESSBAR_HEIGHT);
            ProgressBar.Draw(position, team.StructureTracker.Progress);
        }
    }
}
