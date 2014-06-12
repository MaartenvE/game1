using UnityEngine;
using System.Collections;

using ZXing;
using ZXing.QrCode;
using ZXing.Common;


public class QRWriter : MonoBehaviour {

	public int width;
	public int height;

	// Use this for initialization
	void Start () {
		QRCodeWriter writer = new QRCodeWriter();

        GameObject server = GameObject.Find("Server");
        int port = server.GetComponent<ServerLoader>().Port;
		BitMatrix qrcode = writer.encode("BuildingBlocksServer="+Network.player.ipAddress+":"+port,BarcodeFormat.QR_CODE,width, height);

		var texture = new Texture2D(width, height);
		for(int w = 0; w<width; w++){
			for(int h = 0; h<height; h++){
				texture.SetPixel(h,w, getColorFromBinary(qrcode[w,h]));
			}
		}
		texture.Apply();

		GameObject qrMarkerField = GameObject.FindGameObjectWithTag ("qrmarkertag");
		qrMarkerField.guiTexture.texture = texture;
	}

	//tells whether to let the pixel be filled (black) or empty (white) according to qr encoding.
	public Color getColorFromBinary(bool isfilled){
		if(isfilled){
			return Color.black;

		}
		else {
			return Color.white;
		}
	}
}
