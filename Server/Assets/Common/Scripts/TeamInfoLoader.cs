using UnityEngine;

public class TeamInfoLoader : MonoBehaviour
{
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
    }

    void OnNetworkInstantiate(NetworkMessageInfo info)
    {
        teamInfo.OnNetworkInstantiate();
    }

    [RPC]
    void SetTeamInfo(int id, string name, string imageTarget)
    {
        teamInfo.RPC_SetTeamInfo(id, name, imageTarget);
    }
}
