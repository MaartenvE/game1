using UnityEngine;
using System.Collections;

public class ConnectScreen : MonoBehaviour {
    private string ip = "";
    private string port = "";

    private Rect windowRect = new Rect(20, 20, 120, 50);

    void OnGUI()
    {
        ip = GUI.TextField(new Rect(0, 0, 200, 30), ip );
        port = GUI.TextField(new Rect(0, 50, 200, 30), port);


        if (GUI.Button(new Rect(0, 100, 100, 60), "Connect"))
            if (false)
                Application.LoadLevel(1);
            else
                windowRect = GUI.Window(0, windowRect, WindowFunction, "My Window");
    }

    void WindowFunction(int windowID)
    {
        // Draw any Controls inside the window here
    }
}
