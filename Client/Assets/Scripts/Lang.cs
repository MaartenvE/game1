using UnityEngine;
using System.Collections;

public class Lang  {
	 

	public static GUIStyle ButtonStyle(float height,float width)
	{
		GUIStyle buttonStyle = null;
		buttonStyle  = new GUIStyle (GUI.skin.button);
		float size = Mathf.Min (height, width);

		//Debug.LogError ("Screen height = " + height);
		//buttonStyle.font = new Font("Arial");
		buttonStyle.fontSize = (int)(size * 0.065 );
		//buttonStyle.normal.background = MakeTex( 2, 2, new Color( 255f, 255f, 255f, 0.95f ) );
		//buttonStyle.fontStyle = FontStyle.Bold;

		return buttonStyle;
	}

	public static GUIStyle LabelStyle(float height, float width)
	{
		GUIStyle labelStyle = new GUIStyle(GUI.skin.label);

		float size = Mathf.Min (height, width);

		//labelStyle.font = new Font("Arial");
		labelStyle.fontSize = (int)(size * 0.04f);
		//labelStyle.fontStyle = FontStyle.Bold;

		return labelStyle;
	}

	public static GUIStyle QRStyle(float height, float width)
	{
		GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
		
		float size = Mathf.Min (height, width);
		
		//labelStyle.font = new Font("Arial");
		labelStyle.fontSize = (int)(size * 0.06f);
		//labelStyle.fontStyle = FontStyle.Bold;
		
		return labelStyle;
	}

	public static GUIStyle BoxStyle(float height, float width)
	{
		GUIStyle boxStyle = new GUIStyle( GUI.skin.box );// 	126,170,147
		boxStyle.normal.background = MakeTex( 2, 2, new Color( 126f, 170f, 147f, 0.5f ) );

		float size = Mathf.Min (height, width);
		boxStyle.fontSize = (int)(size * 0.125f);
		boxStyle.fontStyle = FontStyle.Bold;

		return boxStyle;
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
