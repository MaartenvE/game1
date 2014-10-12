using UnityEngine;
using System.Collections;

namespace BuildingBlocks.Blocks
{
    public static class ColorModel
    {

        public static Color RED = Color.red;
        public static Color YELLOW = Color.yellow;
        public static Color BLUE = Color.blue;
        public static Color ORANGE = new Color(1, 0.5f, 0);
        public static Color PURPLE = new Color(0.8f, 0, 1);
        public static Color GREEN = new Color(0, 0.8f, 0);
        public static Color NONE = new Color(0f, 0f, 0f);

        public static char REDCHAR = 'r';
        public static char YELLOWCHAR = 'y';
        public static char BLUECHAR = 'b';
        public static char ORANGECHAR = 'o';
        public static char PURPLECHAR = 'p';
        public static char GREENCHAR = 'g';
        public static char NONECHAR = '0';

        //this is done here, so that if colors would be added, they are easily added here asswell
        public static Color matchColor(char c)
        {
            if (c == REDCHAR)
            {
                return RED;
            }
            else if (c == YELLOWCHAR)
            {
                return YELLOW;
            }
            else if (c == BLUECHAR)
            {
                return BLUE;
            }
            else if (c == ORANGECHAR)
            {
                return ORANGE;
            }
            else if (c == PURPLECHAR)
            {
                return PURPLE;
            }
            else if (c == GREENCHAR)
            {
                return GREEN;
            }
            else if (c == NONECHAR)
            {
                return NONE;
            }
            else
            {
                throw new System.FormatException("undocumented color, either register it in ColorModel or double check puzzleformat");
            }
        }

        public static Color RandomPrimaryColor()
        {
            int r = Random.Range(1, 4);
            switch (r)
            {
                case 1:
                    return RED;
                case 2:
                    return YELLOW;
                default:
                    return BLUE;
            }
        }

        public static bool IsPrimaryColor(Color color)
        {
            return color == RED || color == YELLOW || color == BLUE;
        }

        public static Vector3 ConvertToVector3(Color color)
        {
            return new Vector3(color.r, color.g, color.b);
        }

        public static Color ConvertToUnityColor(Vector3 color)
        {
            return new Color(color.x, color.y, color.z);
        }
    }
}
