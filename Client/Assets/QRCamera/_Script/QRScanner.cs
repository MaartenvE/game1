using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using BuildingBlocks.GUI;

using com.google.zxing.qrcode;

//code from http://ydaira.blogspot.nl/2012/09/how-to-decode-qr-codes-using-unity3d.html
//modified to suit our needs (and to let it work on modern unity/vuforia)
public class QRScanner : MonoBehaviour, ITrackerEventHandler {
	
	private bool isFrameFormatSet;
	
	private Image cameraFeed;
	private string tempText;
	private string qrText;

    public static string IP;
    public static int? Port;
	
	void Start () {
		QCARBehaviour qcarBehaviour = GetComponent<QCARBehaviour>();
		
		if (qcarBehaviour) {
			qcarBehaviour.RegisterTrackerEventHandler(this, false);
		}


		isFrameFormatSet = CameraDevice.Instance.SetFrameFormat(Image.PIXEL_FORMAT.GRAYSCALE, true);
		
		InvokeRepeating("Autofocus", 1f, 2f);
	}
	
	void Autofocus () {
		CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);
	}

	public void OnInitialized() {
        // Empty method required by ITrackerEventHandler
	}
	
	void OnGUI () {
		GUI.Box(new Rect(0, Screen.height - Screen.height*0.05f, Screen.width, Screen.height * 0.1f), qrText, GUIStyles.QRStyle(Screen.height,Screen.width) );
		GUI.Box (new Rect(0, 0, Screen.width, Screen.height * 0.1f), "Scan the QR code to join the game", GUIStyles.QRStyle(Screen.height,Screen.width));

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
	
	public void OnTrackablesUpdated () {
		try {
			if(!isFrameFormatSet) {
				isFrameFormatSet = CameraDevice.Instance.SetFrameFormat(Image.PIXEL_FORMAT.GRAYSCALE, true);
			}
			
			cameraFeed = CameraDevice.Instance.GetCameraImage(Image.PIXEL_FORMAT.GRAYSCALE);
			tempText = new QRCodeReader().decode(cameraFeed.Pixels, cameraFeed.BufferWidth, cameraFeed.BufferHeight).Text;

			//analyze tempText
			//finds the adress from a string as xxx.xxx.xxx.xxx:{y}+
			//string adress = findAndMatch (tempText, "BuildingBlocksServer=([0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3})(?:\\:[0-9]{1,5})");
            string adress = findAndMatch(tempText, "http://is.gd/bloxar#([0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3})(?:\\:[0-9]{1,5})");
			//does the same for port
			//string port = findAndMatch (tempText, "BuildingBlocksServer=(?:[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\:)([0-9]{1,5})");
            string port = findAndMatch(tempText, "http://is.gd/bloxar#(?:[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\:)([0-9]{1,5})");

			//if either is null, the tempText was not of the proper format (false qrcode found)
			if((port != null && port != "")){
				if(adress != null && adress != ""){
					IP = adress;
					Port = int.Parse(port);

					loadGame ();
				}
			}


		}
		catch {
			// Fail detecting QR Code!
		}
		finally {

		}
	}

	public string findAndMatch(string text, string pattern){
		Match match = Regex.Match(text, pattern);

		if(match.Success){
			qrText= "found a matching qr code, joining server";
			return ""+match.Groups[1].Value;
		}
		else{
			qrText = "The scanned qr code is not valid for joining a game";
			return "";
		}
	}


	private void loadGame(){
        QCARBehaviour qcarBehaviour = GetComponent<QCARBehaviour>();

        if (qcarBehaviour)
        {
            qcarBehaviour.UnregisterTrackerEventHandler(this);
        }

		Application.LoadLevel(Application.loadedLevel + 1);
	}

}
