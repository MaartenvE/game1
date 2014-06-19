using UnityEngine;
using System.Collections;
using BuildingBlocks.Team;

public class TeamInfoOverlay
{
    private TeamInfo teamInfo;

    public TeamInfoOverlay(TeamInfo teamInfo)
    {
        this.teamInfo = teamInfo;
    }

    public void OnGUI()
    {
        if (!teamInfo.IsMine)
        {
            string imageTarget = teamInfo.ImageTarget;
            if (imageTarget != null)
            {
                GameObject target = GameObject.Find(imageTarget);
                ImageTargetBehaviour behaviour = target.GetComponent<ImageTargetBehaviour>();
                if (behaviour.CurrentStatus != TrackableBehaviour.Status.NOT_FOUND)
                {
                    Vector3 location = Camera.main.WorldToScreenPoint(target.transform.position);
                    Rect position = new Rect(
                        location.x,
                        Screen.height - Screen.width * InGameOverlay.PROGRESSBAR_HEIGHT - Screen.width * InGameOverlay.PROGRESSBAR_PADDING,
                        Screen.width * InGameOverlay.PROGRESSBAR_WIDTH,
                        Screen.width * InGameOverlay.PROGRESSBAR_HEIGHT);
                    ProgressBar.Draw(position, teamInfo.Progress);
                }
            }
        }
    }
}
