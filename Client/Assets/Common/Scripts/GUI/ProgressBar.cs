using UnityEngine;

namespace BuildingBlocks.GUI
{
    public static class ProgressBar
    {
        private static GUIStyle style;

        public static void Draw(Rect position, float progress)
        {
            Color previousColor = UnityEngine.GUI.color;

            setStyle();

            drawProgressBar(position, progress);
            UnityEngine.GUI.color = previousColor;
        }

        private static void setStyle()
        {
            if (style == null)
            {
                Texture2D texture = new Texture2D(1, 1);
                texture.SetPixel(0, 0, Color.white);
                texture.Apply();

                style = new GUIStyle();
                style.normal.background = texture;
            }
        }

        private static void drawProgressBar(Rect position, float progress)
        {
            UnityEngine.GUI.color = Color.gray;
            UnityEngine.GUI.Box(position, GUIContent.none, style);

            position.x += (1 - progress) * position.width;
            position.width *= progress;
            UnityEngine.GUI.color = Color.Lerp(Color.red, Color.green, progress);
            UnityEngine.GUI.Box(position, GUIContent.none, style);
        }
    }
}
