using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace BuildingBlocks.Blocks
{
    public static class ColorModel
    {

        public static Color RED = Color.red;
        public static Color YELLOW = Color.yellow;
        public static Color BLUE = Color.blue;
        public static Color ORANGE = new Color(1, 0.5f, 0);
        public static Color PURPLE = new Color(0.5f, 0, 1);
        public static Color GREEN = Color.green;

        public static Vector3 ConvertToVector3(Color color)
        {
            return new Vector3(color.r, color.g, color.b);
        }

        public static Color ConvertToUnityColor(Vector3 color)
        {
            return new Color(color.x, color.y, color.z);
        }

        public static IEnumerable<Color> Colors
        {
            get
            {
                yield return RED;
                yield return YELLOW;
                yield return BLUE;
                yield return ORANGE;
                yield return PURPLE;
                yield return GREEN;
            }
        }

        public static Color RandomColor()
        {
            int index = Random.Range(0, Colors.Count());
            return Colors.ElementAt(index);
        }
    }
}
