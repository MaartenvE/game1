using UnityEngine;
using System.Collections;
using BuildingBlocks.Team;

namespace BuildingBlocks.GUI
{
    public class TeamProgressOverlay
    {
        private ITeam team;

        public TeamProgressOverlay(ITeam team)
        {
            this.team = team;
        }

        public void OnGUI()
        {
            if (!team.IsMine && team.Target != null)
            {
                findTarget(team.Target);
            }
        }

        private void findTarget(string imageTarget)
        {
            GameObject target = GameObject.Find(imageTarget);
            ImageTargetBehaviour behaviour = target.GetComponent<ImageTargetBehaviour>();
            if (behaviour.CurrentStatus != TrackableBehaviour.Status.NOT_FOUND)
            {
                Vector3 location = Camera.main.WorldToScreenPoint(target.transform.position);
                drawProgress(location.x);
            }
        }

        private void drawProgress(float xpos)
        {
            Rect position = new Rect(
                xpos,
                Screen.height - Screen.width * InGameOverlay.PROGRESSBAR_HEIGHT - Screen.width * InGameOverlay.PROGRESSBAR_PADDING,
                Screen.width * InGameOverlay.PROGRESSBAR_WIDTH,
                Screen.width * InGameOverlay.PROGRESSBAR_HEIGHT);
            ProgressBar.Draw(position, team.Progress);
        }
    }
}
