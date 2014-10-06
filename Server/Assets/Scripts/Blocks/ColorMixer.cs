using UnityEngine;
using System.Collections.Generic;

namespace BuildingBlocks.Blocks
{
    public static class ColorMixer
    {
        static Dictionary<ColorPair, Color> colorMap;

        static ColorMixer()
        {
            colorMap = new Dictionary<ColorPair, Color>(6);

            //addMapping(ColorModel.RED, ColorModel.RED, ColorModel.RED);
            //addMapping(ColorModel.YELLOW, ColorModel.YELLOW, ColorModel.YELLOW);
            //addMapping(ColorModel.BLUE, ColorModel.BLUE, ColorModel.BLUE);

            addMapping(ColorModel.RED, ColorModel.YELLOW, ColorModel.ORANGE);
            addMapping(ColorModel.RED, ColorModel.BLUE, ColorModel.PURPLE);
            addMapping(ColorModel.YELLOW, ColorModel.BLUE, ColorModel.GREEN);
        }

        private static void addMapping(Color color1, Color color2, Color result)
        {
            colorMap.Add(new ColorPair(color1, color2), result);
        }

        public static bool Mix(Color color1, Color color2, out Color result)
        {
            return colorMap.TryGetValue(new ColorPair(color1, color2), out result);
        }
    }
}
