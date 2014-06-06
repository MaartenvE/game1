using UnityEngine;
using System.Collections;

public class ConnectScreen : MonoBehaviour {
    private string _ip = "127.0.0.1";
    private string _port = "3825";

    private Rect windowRect = new Rect(20, 20, 120, 50);

    void OnGUI()
    {
        _ip = GUI.TextField(new Rect(0, 0, 200, 30), _ip );
        _port = GUI.TextField(new Rect(0, 50, 200, 30), _port);


        if (GUI.Button(new Rect(0, 100, 100, 60), "Connect"))
            if (true){
                Application.LoadLevel(1);
                client.port = int.Parse(_port);
                client.ip = _ip;
            } 
		//still doesn't create a pop up
            else
                windowRect = GUI.Window(0, windowRect, WindowFunction, "My Window");
    }

    void WindowFunction(int windowID)
    {
        // Draw any Controls inside the window here
    }
}
