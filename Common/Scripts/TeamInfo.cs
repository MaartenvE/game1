using UnityEngine;
using System.Collections;

public class TeamInfo : MonoBehaviour
{
    public int ID;
    public string Name;
    public string ImageTarget;

    void OnNetworkInstantiate(NetworkMessageInfo info)
    {
        this.transform.parent = GameObject.Find("Teams").transform;
    }

    public void SetInfo(int id, string name, string imageTarget)
    {
        networkView.RPC("SetTeamInfo", RPCMode.AllBuffered, id, name, imageTarget);
    }

    public bool IsMine()
    {
        return GameObject.Find("Player").GetComponent<PlayerInfo>().Team == ID;
    }

    [RPC]
    void SetTeamInfo(int id, string name, string imageTarget)
    {
        ID = id;
        Name = name;
        ImageTarget = imageTarget;

        GameObject target = GameObject.Find(imageTarget) as GameObject;
        target.transform.parent = gameObject.transform;
    }
}
