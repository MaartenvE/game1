using UnityEngine;
using System.Collections;
using System;

public class Timer : MonoBehaviour
{
    public const int TIMELIMIT = 900;

    private double startTime;
    private const float PADDING_TOP = .05f;
    private const float PADDING_RIGHT = .01f;
    private const float WIDTH = .15f;
    private const float HEIGHT = .04f;

    private double endTime;
    private GUIStyle style;

    void OnServerInitialized()
    {
        this.startTime = Network.time;
        Invoke("EndRound", TIMELIMIT);
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        float timeRemaining = (float)(TIMELIMIT - (Network.time - startTime));
        networkView.RPC("SetTime", player, timeRemaining);
        endTime = Network.time + timeRemaining;
    }

    void OnGUI()
    {
        setStyle();

        double timeRemaining = Math.Max(0, endTime - Network.time);

        int minutes = (int)(timeRemaining / 60);
        int seconds = (int)(timeRemaining % 60);

        string text = string.Format("{0:00}:{1:00}", minutes, seconds);

        float width = Screen.width * WIDTH;
        GUI.Label(new Rect(
                Screen.width - width - (Screen.width * PADDING_RIGHT),
                Screen.width * PADDING_TOP,
                width,
                Screen.width * HEIGHT
            ), text, style);
    }

    private void setStyle()
    {
        if (style == null)
        {
            style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleRight;
        }
        style.fontSize = (int)(Screen.width * HEIGHT - 2);
    }

    void EndRound()
    {
        ServerLoader.Server.TimeUp();
    }

    [RPC]
    void SetTime(float timeRemaining) { }
}
