using UnityEngine;
using System.Collections;

public class Timer: MonoBehaviour
{
	private float _startTime;
	private float _elapsedTime;

	private GUIStyle _currentStyle = null;
	
	void Awake(){
		_startTime = 900;
	}
	
	void Update () {
		
		if (_startTime > 0)
		{
			_elapsedTime =  _startTime - Time.time ;
		}
	}
	
	void OnTriggerEnter(){
		_startTime = Time.time;
	}
	
	void OnTriggerExit(){
		_startTime = 0;
	}
	
	void OnGUI(){
		string print = "";
		int min = (int) (_elapsedTime / 60);
		int sec = (int) (_elapsedTime % 60);
		
		if (sec < 10) {
			print = "Time = " + min + ":0" + sec;
		} else {
			print = "Time = " + min + ":" + sec;
		}

		InitStyles ();
		GUI.Label(new Rect(Screen.width - 140, 25, 140, 40), print, _currentStyle);
	}

	private void InitStyles()
	{
		if( _currentStyle == null )
		{
			_currentStyle = new GUIStyle( GUI.skin.label );
			//_currentStyle.font = new Font("Arial");
			_currentStyle.fontSize = 22;
			_currentStyle.fontStyle = FontStyle.Bold;
		}
	}


}