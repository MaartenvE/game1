using UnityEngine;

namespace BuildingBlocks.GUI
{
    public static class Icon
    {
        public static bool IsPressed(Rect position, Texture2D image, Color color)
        {
            drawShadow(position, image);
            drawImage(position, image, color);
            return drawButton(position);
        }

        private static void drawShadow(Rect position, Texture2D image)
        {
            UnityEngine.GUI.color = Color.gray;
            UnityEngine.GUI.DrawTexture(new Rect(position.xMin - 1, position.yMin - 1, position.width + 2, position.width + 2), image);
        }

        private static void drawImage(Rect position, Texture2D image, Color color)
        {
            UnityEngine.GUI.color = color;
            UnityEngine.GUI.DrawTexture(position, image);
        }

        private static bool drawButton(Rect position)
        {
            return UnityEngine.GUI.Button(position, GUIContent.none, GUIStyle.none);
        }
    }
}
