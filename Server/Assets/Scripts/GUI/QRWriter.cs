using UnityEngine;
using System.Collections;
using BuildingBlocks.Server;

using ZXing;
using ZXing.QrCode;
using ZXing.Common;

namespace BuildingBlocks.GUI
{
    public class QRWriter : MonoBehaviour
    {
        public float sizePercentage;

        private int size;


        // Use this for initialization
        void Start()
        {
            QRCodeWriter writer = new QRCodeWriter();

            GameObject server = GameObject.Find("Server");
            int port = server.GetComponent<ServerLoader>().Port;
            size = ((int)(sizePercentage * Screen.height));
            BitMatrix qrcode = writer.encode("BuildingBlocksServer=" + Network.player.ipAddress + ":" + port, BarcodeFormat.QR_CODE, size, size);

            GUITexture GUItexture = GetComponent<GUITexture>();
            GUItexture.pixelInset = new Rect( -size / 2, 0, size, size);

            var texture = new Texture2D(size, size);
            for (int w = 0; w < size; w++)
            {
                for (int h = 0; h < size; h++)
                {
                    texture.SetPixel(h, w, getColorFromBinary(qrcode[w, h]));
                }
            }
            texture.Apply();

            GameObject qrMarkerField = GameObject.FindGameObjectWithTag("qrmarkertag");
            qrMarkerField.guiTexture.texture = texture;
        }

        //tells whether to let the pixel be filled (black) or empty (white) according to qr encoding.
        public Color getColorFromBinary(bool isfilled)
        {
            if (isfilled)
            {
                return Color.black;

            }
            else
            {
                return Color.white;
            }
        }
    }
}
