using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour
{
	private GUIStyle _currentStyle = null;
	private GUIStyle _buttonStyle = null;

    void OnGUI()
    {
		InitStyles();
		GUI.contentColor = Color.black;
		GUI.Box( new Rect ( 0, 0, Screen.width, Screen.height), "Building Blocks",_currentStyle);//, Lang.buttonStyle(Screen.height)); 

		GUI.backgroundColor = new Color(0f, 0f, 255f);
		if (GUI.Button(new Rect((Screen.width / 2) - Screen.width * 0.25f, (Screen.height /2) - Screen.height * .1f, Screen.width * 0.5f, Screen.height * 0.1f), "Play The Game" , Lang.buttonStyle(Screen.height, Screen.width)))
        {
            Application.LoadLevel(2);
        }
		//- ((Screen.width * 0.75f) /2))

		if (GUI.Button(new Rect((Screen.width / 2) - Screen.width * 0.25f, (Screen.height /2) + Screen.height * .1f, Screen.width * 0.5f, Screen.height * 0.1f), "Spectate" , Lang.buttonStyle(Screen.height, Screen.width)))
        {
            TeamSelector.IsSpectator = true;
            Application.LoadLevel(2);
        }  

		GUI.Label (new Rect (Screen.width - 80, Screen.height - 20, 80, 20), "Team Cubed");//,Lang.labelStyle(Screen.height));

    }


	private void InitStyles()
	{
		if( _currentStyle == null )
		{
			_currentStyle = new GUIStyle( GUI.skin.box );
			_currentStyle.normal.background = MakeTex( 2, 2, new Color( 255f, 255f, 255f, 0.95f ) );
			//_currentStyle.font = new Font("Arial");
			_currentStyle.fontSize = 32;
			_currentStyle.fontStyle = FontStyle.Bold;
		}

	}


	private Texture2D MakeTex( int width, int height, Color col )
	{
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}
}