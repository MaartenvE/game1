using UnityEngine;
using System.Collections;

public class Lang  {
	 

	public static GUIStyle buttonStyle(float height,float width)
	{
		GUIStyle buttonStyle = null;
		buttonStyle  = new GUIStyle (GUI.skin.button);
		float size = 0;

		size = Mathf.Min (height, width);


		Debug.LogError ("Screen height = " + height);
		//buttonStyle.font = new Font("Arial");
		buttonStyle.fontSize = (int)(size * 0.05 );
		//buttonStyle.normal.background = MakeTex( 2, 2, new Color( 255f, 255f, 255f, 0.95f ) );
		//buttonStyle.fontStyle = FontStyle.Bold;

		return buttonStyle;
	}

	public GUIStyle timeStyle()
	{
		GUIStyle timeStyle = new GUIStyle(GUI.skin.label);

		timeStyle.alignment = TextAnchor.MiddleRight;
		timeStyle.font = new Font("Arial");
		timeStyle.fontSize = 15;
		timeStyle.fontStyle = FontStyle.Bold;
		
		return timeStyle;
	}

	public static GUIStyle labelStyle(float height)
	{
		GUIStyle labelStyle = new GUIStyle(GUI.skin.label);

		labelStyle.font = new Font("Arial");
		labelStyle.fontSize = 128;
		labelStyle.fontStyle = FontStyle.Bold;

		return labelStyle;
	}

	private static Texture2D MakeTex( int width, int height, Color col )
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
