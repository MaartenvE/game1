	using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
	private const float xloc = 0.6f;
	private const float yloc = 0.3f;
	private const float height = 0.25f;
	private const float width= 0.25f;

	private const float buttonHeight = 0.05f;
	private const float buttonWidth = 0.15f;

	public Texture2D RedPhone;
	public Texture2D YellowPhone;
	public Texture2D OrangePhone;
	public Texture2D PhoneSide;
	public Texture2D PhoneOtherSide;

	public Texture2D ArrowLeft;
	public Texture2D ArrowRight;

	private double startTime = Time.time;

	void OnGUI() {


		Rect windowRect = new Rect(Screen.width*xloc, Screen.height*yloc, Screen.width*width, Screen.height*height);
		windowRect = GUI.Window(0, windowRect, DoMyWindow, "Merging");
	}

	void DoMyWindow(int windowID) {
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
		float buttonx =  Screen.height*height - buttonHeightAbsolute;
		float buttony = (Screen.width*width - buttonWidthAbsolute) * 0.5f;
		
		if (GUI.Button (new Rect (buttony, buttonx, buttonWidthAbsolute, buttonHeightAbsolute), "Close"))
			Destroy (this);
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
	}

	void drawSide(){
		float width1 = 0.1f * Screen.width * width;
		float width2 = 0.3f * Screen.width * width;
		float width3 = 0.3f * Screen.width * width;
		float width4 = 0.1f * Screen.width * width;

		float hight1 = Screen.height*(height - 2 * buttonHeight);
		float hight2 = 0.15f * Screen.height*(height - 2 * buttonHeight);
		float hight3 = 0.15f * Screen.height*(height - 2 * buttonHeight);
		float hight4 = Screen.height*(height - 2 * buttonHeight);

		float x1 = 0.1f  * Screen.width * width;
		float x2 = 0.2f * Screen.width * width;
		float x3 = 0.5f  * Screen.width * width;
		float x4 = 0.8f * Screen.width * width;
		
		float y1 = 0.15f * Screen.height * height;
		float y2 = 0.4f * Screen.height * height;

		float y3 = 0.4f * Screen.height * height;
		float y4 = 0.15f * Screen.height * height;
		
		GUI.DrawTexture(new Rect(x1, y1, width1, hight1), PhoneSide, ScaleMode.ScaleToFit);
		GUI.DrawTexture(new Rect(x2, y2, width2, hight2), ArrowRight, ScaleMode.ScaleToFit);
		GUI.DrawTexture(new Rect(x3, y3, width3, hight3), ArrowLeft, ScaleMode.ScaleToFit);
		GUI.DrawTexture(new Rect(x4, y4, width4, hight4), PhoneOtherSide, ScaleMode.ScaleToFit);

	}


	void drawBump(){
		float width1 = 0.35f * Screen.width * width;
		float width2 = 0.35f * Screen.width * width;
		
		float hight1 = Screen.height*(height - 2 * buttonHeight);
		float hight2 = Screen.height*(height - 2 * buttonHeight);
		
		float x1 = 0.3f  * Screen.width * width;
		float x2 = 0.4f * Screen.width * width;
		
		float y1 = 0.15f * Screen.height * height;
		float y2 = 0.15f * Screen.height * height;
		
		GUI.DrawTexture(new Rect(x1, y1, width1, hight1), PhoneSide, ScaleMode.ScaleToFit);
		GUI.DrawTexture(new Rect(x2, y2, width2, hight1), PhoneOtherSide, ScaleMode.ScaleToFit);

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
	}
	
	double timePassed(){
		return Time.time - startTime;
	}
}