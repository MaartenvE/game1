using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public const int TIMELIMIT = 30;

    private double startTime;

    void OnServerInitialized()
    {
        this.startTime = Network.time;
        Invoke("EndRound", TIMELIMIT);
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        networkView.RPC("SetTime", player, (float) (TIMELIMIT - (Network.time - startTime)));
    }

    void EndRound()
    {
        ServerLoader.Server.TimeUp();
    }

    [RPC]
    void SetTime(float timeRemaining) { }
}
