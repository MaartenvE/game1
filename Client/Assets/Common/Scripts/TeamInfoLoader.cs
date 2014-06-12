using UnityEngine;

public class TeamInfoLoader : MonoBehaviour
{
    private TeamInfoOverlay overlay;

    private TeamInfo teamInfo;
    public TeamInfo TeamInfo
    {
        get
        {
            return teamInfo;
        }
    }

    void Awake()
    {
        teamInfo = new TeamInfo(new GameObjectWrapper(gameObject));
        overlay = new TeamInfoOverlay(teamInfo);
    }

    void OnGUI()
    {
        overlay.OnGUI();
    }

    void OnNetworkInstantiate(NetworkMessageInfo info)
    {
        teamInfo.OnNetworkInstantiate();
    }

    [RPC]
    void SetTeamProgress(float progress)
    {
        teamInfo.RPC_SetTeamProgress(progress);
    }

    [RPC]
    void SetTeamInfo(int id, string name, string imageTarget)
    {
        teamInfo.RPC_SetTeamInfo(id, name, imageTarget);
    }
}
