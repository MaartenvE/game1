
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Color[] primaryColors = { ColorModel.RED, ColorModel.YELLOW, ColorModel.BLUE };
            int index = UnityEngine.Random.Range(0, 3);
            return new HalfBlockColor(primaryColors[index]);
        }
    }
}