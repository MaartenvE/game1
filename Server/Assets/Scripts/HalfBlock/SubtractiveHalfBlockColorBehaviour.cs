using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingBlocks.Blocks;

namespace BuildingBlocks.HalfBlock
{
    public class SubtractiveHalfBlockColorBehaviour : IHalfBlockColorBehaviour
    {
        private Dictionary<ColorPair, AbstractHalfBlockColor>
                    _subtractiveColorMap;

        public SubtractiveHalfBlockColorBehaviour()
        {
            _subtractiveColorMap = new Dictionary<ColorPair,
            AbstractHalfBlockColor>();


        }

        public void SetMapping()
        {
            AddMapping(new ColorPair(CreateColor(ColorModel.RED), CreateColor(ColorModel.RED)), CreateColor(ColorModel.RED));
            AddMapping(new ColorPair(CreateColor(ColorModel.YELLOW), CreateColor(ColorModel.YELLOW)), CreateColor(ColorModel.YELLOW));
            AddMapping(new ColorPair(CreateColor(ColorModel.BLUE), CreateColor(ColorModel.BLUE)), CreateColor(ColorModel.BLUE));

            AddMapping(new ColorPair(CreateColor(ColorModel.RED), CreateColor(ColorModel.BLUE)), CreateColor(ColorModel.PURPLE));
            AddMapping(new ColorPair(CreateColor(ColorModel.RED), CreateColor(ColorModel.YELLOW)), CreateColor(ColorModel.ORANGE));
            AddMapping(new ColorPair(CreateColor(ColorModel.YELLOW), CreateColor(ColorModel.BLUE)), CreateColor(ColorModel.GREEN));
        }

        private HalfBlockColor CreateColor(Color _color)
        {
            return new HalfBlockColor(_color);
        }

        private void AddMapping(ColorPair _pair, AbstractHalfBlockColor _result)
        {
            _subtractiveColorMap.Add(_pair, _result);
        }

        public AbstractHalfBlockColor CombineColor(AbstractHalfBlockColor first, AbstractHalfBlockColor second)
        {
            ColorPair key = new ColorPair(first, second);
            AbstractHalfBlockColor _result;
            _subtractiveColorMap.TryGetValue(key, out _result);
            return _result;
        }

        public static AbstractHalfBlockColor RandomPrimaryColor()
        {
            HalfBlockColor color;
            ArrayList primaryColors = new ArrayList();
            primaryColors.Add(ColorModel.RED);
            primaryColors.Add(ColorModel.YELLOW);
            primaryColors.Add(ColorModel.BLUE);

            float random = UnityEngine.Random.value;
            Color c  = new Color();

            foreach (KeyValuePair<Color, float> entry in StructureReader.colorsMap)
            {
                if (random <= entry.Value)
                {
                    c = entry.Key;
                    break;
                }
            }

            if (!primaryColors.Contains(c))
            {
                c = splitSecondary(c);
                color = new HalfBlockColor(c);
                color.isSecondaryColor = true;
                return color;
            }
            color = new HalfBlockColor(c);
            color.isSecondaryColor = false;
            return color;
        }

        private static Color splitSecondary(Color c)
        {
            if (c.Equals(ColorModel.ORANGE))
            {
                return UnityEngine.Random.value < 0.5 ? ColorModel.RED : ColorModel.YELLOW;
            }

            if (c.Equals(ColorModel.PURPLE))
            {
                return UnityEngine.Random.value < 0.5 ? ColorModel.RED : ColorModel.BLUE;
            }

            if (c.Equals(ColorModel.GREEN))
            {
                return UnityEngine.Random.value < 0.5 ? ColorModel.BLUE : ColorModel.YELLOW;
            }
            return ColorModel.NONE;
        }
    }
}