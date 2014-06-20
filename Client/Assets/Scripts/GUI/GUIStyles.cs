using UnityEngine;
using System.Collections;

namespace BuildingBlocks.GUI
{
    public static class GUIStyles
    {
        public static GUIStyle ButtonStyle(float height, float width)
        {
            GUIStyle buttonStyle = new GUIStyle(UnityEngine.GUI.skin.button);
            float size = Mathf.Min(height, width);
            buttonStyle.fontSize = (int)(size * 0.065);

            return buttonStyle;
        }

        public static GUIStyle LabelStyle(float height, float width)
        {
            GUIStyle labelStyle = new GUIStyle(UnityEngine.GUI.skin.label);
            float size = Mathf.Min(height, width);
            labelStyle.fontSize = (int)(size * 0.04f);

            return labelStyle;
        }

        public static GUIStyle QRStyle(float height, float width)
        {
            GUIStyle labelStyle = new GUIStyle(UnityEngine.GUI.skin.label);
            float size = Mathf.Min(height, width);
            labelStyle.fontSize = (int)(size * 0.06f);

            return labelStyle;
        }

        public static GUIStyle BoxStyle(float height, float width)
        {
            GUIStyle boxStyle = new GUIStyle(UnityEngine.GUI.skin.box);
            boxStyle.normal.background = MakeTexture(new Color(126f, 170f, 147f, 0.5f));

            float size = Mathf.Min(height, width);
            boxStyle.fontSize = (int)(size * 0.125f);
            boxStyle.fontStyle = FontStyle.Bold;

            return boxStyle;
        }

        public static Texture2D MakeTexture(Color color)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            return texture;
        }
    }
}
