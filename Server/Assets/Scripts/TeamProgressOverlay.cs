using UnityEngine;
using System.Collections;

public class TeamInfoOverlay
{
    private TeamInfo teamInfo;

    private const float PROGRESSBAR_HEIGHT = 0.02f;
    private const float PROGRESSBAR_WIDTH = 0.15f;
    private const float PROGRESSBAR_PADDING = 0.2f;

    public TeamInfoOverlay(TeamInfo teamInfo)
    {
        this.teamInfo = teamInfo;
    }

    // todo: Duplication across client/server?
    public void OnGUI()
    {
        string imageTarget = teamInfo.ImageTarget;
        if (imageTarget != null)
        {
            GameObject target = GameObject.Find(imageTarget);
            Camera camera = target.GetComponentInChildren<Camera>();
            Vector3 location = camera.WorldToScreenPoint(target.transform.position);

            float width = Screen.width * PROGRESSBAR_WIDTH;

            Rect position = new Rect(
                location.x - width / 2,
                Screen.height - Screen.height * PROGRESSBAR_PADDING - Screen.width * PROGRESSBAR_HEIGHT,
                width,
                Screen.width * PROGRESSBAR_HEIGHT);
            ProgressBar.Draw(position, teamInfo.Progress);
        }
    }
}