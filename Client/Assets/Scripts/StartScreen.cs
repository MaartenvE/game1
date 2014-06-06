using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour
{
    void OnGUI()
    {
        if (GUI.Button(new Rect((Screen.width / 2) - 25 , (Screen.height /2) - 80, 100, 50), "Join Game"))
        {
            Application.LoadLevel(2);
        }

       if (GUI.Button(new Rect((Screen.width / 2) -25, (Screen.height / 2) -20, 100, 50), "Spectate"))  
        {
            Debug.Log("Spectate button was clicked");
        }
            
        
    }	
}
