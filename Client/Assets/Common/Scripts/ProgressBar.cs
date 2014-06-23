using UnityEngine;
using System.Collections;

public class ProgressBar
{
    private static GUIStyle style;

    public static void Draw(Rect position, float progress)
    {
        Color previousColor = GUI.color;

        if (style == null)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, Color.white);
            texture.Apply();

            style = new GUIStyle();
            style.normal.background = texture;
        }

        drawProgressBar(position, progress);
        GUI.color = previousColor;
    }

    private static void drawProgressBar(Rect position, float progress)
    {
        GUI.color = Color.gray;
        GUI.Box(position, GUIContent.none, style);

        position.x += (1 - progress) * position.width;
        position.width *= progress;
        GUI.color = Color.Lerp(Color.red, Color.green, progress);
        GUI.Box(position, GUIContent.none, style);
    }
}
