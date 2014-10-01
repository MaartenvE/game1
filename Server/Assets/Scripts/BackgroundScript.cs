using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {

	// Use this for initialization
	void Update () {
		GUITexture background = this.gameObject.GetComponent<GUITexture> ();
		background.pixelInset = new Rect (0, 0, Screen.width, Screen.height);
	}
}
