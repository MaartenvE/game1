	using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
	private const float xloc = 0.15f;
	private const float yloc = 0.3f;
	private const float height = 0.5f;
	private const float width= 0.7f;

	private const float buttonHeight = 0.05f;
	private const float buttonWidth = 0.25f;

	public Texture2D RedPhone;
	public Texture2D YellowPhone;
	public Texture2D OrangePhone;
	public Texture2D PhoneSide;
	public Texture2D PhoneOtherSide;

	public Texture2D ArrowLeft;
	public Texture2D ArrowRight;

	private double startTime = Time.time;
	private GUIStyle style;
	

	void OnGUI() {
		Rect windowRect = new Rect(Screen.width*xloc, Screen.height*yloc, Screen.width*width, Screen.height*height);
		windowRect = GUI.Window(0, windowRect, DoMyWindow, "Mixing");
	}
	

	void DoMyWindow(int windowID) {
		
		GUI.skin.window.fontSize = (int)(Screen.height * 0.075f* height);
		int time = (int)timePassed () % 14;
		if (time < 3) {
			drawStart ();
		} else if (time < 6) {
			drawSide();
		} else if (time < 9) {
			drawBump ();
		} else if (time < 12){
			drawFinish ();
		}
		drawButton ();
	}

	void drawButton(){
		float buttonHeightAbsolute = buttonHeight * Screen.height;
		float buttonWidthAbsolute = buttonWidth * Screen.width;
		float buttonx =  Screen.height*height - buttonHeightAbsolute* 1.5f;
		float buttony = (Screen.width*width - buttonWidthAbsolute) * 0.5f;
		
		if (GUI.Button (new Rect (buttony, buttonx, buttonWidthAbsolute, buttonHeightAbsolute), "Close"))
			Destroy (this);
	}

	void drawNumber(int number){
		float x = 0.05f * Screen.width * width; 
		float y = 0;

		float width1 = 0.2f * Screen.width * width;
		float height1 = 0.3f * Screen.height*height;

		GUIStyle numberStyle = new GUIStyle ();
		numberStyle.fontSize = (int)(Screen.height * 0.15f* height);
		numberStyle.normal.textColor = new Color(255,255,255,0.5f);

		GUI.Label(new Rect(x, y, width1, height1),  number + "", numberStyle);
		//"<p style ='opacity: 0.5'> " +

	}

	void drawStart(){

		float width1 = 0.35f * Screen.width * width;
		float width2 = 0.35f * Screen.width * width;
		
		float hight1 = Screen.height*(height - 2 * buttonHeight);
		float hight2 = Screen.height*(height - 2 * buttonHeight);
		
		float x1 = 0.05f * Screen.width * width;
		float x2 = 0.6f * Screen.width * width;
		
		float y1 = 0.1f * Screen.height * height;
		float y2 = 0.1f * Screen.height * height;
		
		GUI.DrawTexture(new Rect(x1, y1, width1, hight1), RedPhone, ScaleMode.ScaleToFit);
		GUI.DrawTexture(new Rect(x2, y2, width2, hight1), YellowPhone, ScaleMode.ScaleToFit);


		drawNumber(1);
	}

	void drawSide(){


		float width1 = 0.1f * Screen.width * width;
		float width2 = 0.3f * Screen.width * width;
		float width3 = 0.3f * Screen.width * width;
		float width4 = 0.1f * Screen.width * width;

		float hight1 = Screen.height*(height - 2.5f * buttonHeight);
		float hight2 = 0.15f * Screen.height*(height - 2.5f * buttonHeight);
		float hight3 = 0.15f * Screen.height*(height - 2.5f * buttonHeight);
		float hight4 = Screen.height*(height - 2.5f * buttonHeight);

		float x1 = 0.1f  * Screen.width * width;
		float x2 = 0.2f * Screen.width * width;
		float x3 = 0.5f  * Screen.width * width;
		float x4 = 0.8f * Screen.width * width;
		
		float y1 = 0.1f * Screen.height * height;
		float y2 = 0.4f * Screen.height * height;

		float y3 = 0.4f * Screen.height * height;
		float y4 = 0.1f * Screen.height * height;
		
		GUI.DrawTexture(new Rect(x1, y1, width1, hight1), PhoneSide, ScaleMode.ScaleToFit);
		GUI.DrawTexture(new Rect(x2, y2, width2, hight2), ArrowRight, ScaleMode.ScaleToFit);
		GUI.DrawTexture(new Rect(x3, y3, width3, hight3), ArrowLeft, ScaleMode.ScaleToFit);
		GUI.DrawTexture(new Rect(x4, y4, width4, hight4), PhoneOtherSide, ScaleMode.ScaleToFit);

		drawNumber(2);

	}


	void drawBump(){

		float width1 = 0.35f * Screen.width * width;
		float width2 = 0.35f * Screen.width * width;
		
		float hight1 = Screen.height*(height - 2.5f * buttonHeight);
		float hight2 = Screen.height*(height - 2.5f * buttonHeight);
		
		float x1 = 0.27f  * Screen.width * width;
		float x2 = 0.37f * Screen.width * width;
		
		float y1 = 0.1f * Screen.height * height;
		float y2 = 0.1f * Screen.height * height;
		
		GUI.DrawTexture(new Rect(x1, y1, width1, hight1), PhoneSide, ScaleMode.ScaleToFit);
		GUI.DrawTexture(new Rect(x2, y2, width2, hight1), PhoneOtherSide, ScaleMode.ScaleToFit);

		
		drawNumber(3);

	}

	void drawFinish(){


		float width1 = 0.35f * Screen.width * width;
		float width2 = 0.35f * Screen.width * width;
		
		float hight1 = Screen.height*(height - 2 * buttonHeight);
		float hight2 = Screen.height*(height - 2 * buttonHeight);
		
		float x1 = 0.05f * Screen.width * width;
		float x2 = 0.6f * Screen.width * width;
		
		float y1 = 0.1f * Screen.height * height;
		float y2 = 0.1f * Screen.height * height;
		
		GUI.DrawTexture(new Rect(x1, y1, width1, hight1), OrangePhone, ScaleMode.ScaleToFit);
		GUI.DrawTexture(new Rect(x2, y2, width2, hight1), OrangePhone, ScaleMode.ScaleToFit);

		drawNumber(4);
	}
	
	double timePassed(){
		return Time.time - startTime;
	}
}