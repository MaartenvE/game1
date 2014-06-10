using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

using com.google.zxing.qrcode;

//code from http://ydaira.blogspot.nl/2012/09/how-to-decode-qr-codes-using-unity3d.html
//modified to suit our needs (and to let it work on modern unity/vuforia)
public class QRScanner : MonoBehaviour, ITrackerEventHandler {
	
	private bool isFrameFormatSet;
	
	private Image cameraFeed;
	private string tempText;
	private string qrText;

	public static string adressText;
	public static int portNum;
	
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

	/*void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}*/

	public void OnInitialized() {
		Debug.Log ("started a new imageAnalyser");
	}
	
	void OnGUI () {
		GUI.Box(new Rect(0, Screen.height - 25, Screen.width, 25), qrText);
		GUI.Box (new Rect(0, 0, Screen.width, 50), "Scan the QR code to join the game");
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
			string adress = findAndMatch (tempText, "BuildingBlocksServer=([0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3})(?:\\:[0-9]{1,5})");
			//does the same for port
			string port = findAndMatch (tempText, "BuildingBlocksServer=(?:[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\:)([0-9]{1,5})");

			//adress = "adress";
			//	port = "port";
			/*if(adress== null || adress==""){
				adress = "adress size is 0 or null";
			}*/

			//if either is null, the tempText was not of the proper format (false qrcode found)
			if((port != null && port != "")){
				if(adress != null && port != ""){
					adressText = adress;
					portNum = int.Parse(port);

					loadGame ();
				}
			}


		}
		catch {
			// Fail detecting QR Code!
		}
		finally {
			if(!string.IsNullOrEmpty(tempText)) {
				//qrText = tempText;
			}
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
		Application.LoadLevel(1);
	}

}
