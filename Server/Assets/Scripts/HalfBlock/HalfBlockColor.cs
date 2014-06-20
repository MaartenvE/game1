using System.Collections.Generic;
using System;
using UnityEngine;


namespace BuildingBlocks.HalfBlock
{
    public class HalfBlockColor : AbstractHalfBlockColor, IEquatable<HalfBlockColor>
    {

        private static IHalfBlockColorBehaviour _colorBehaviour = null;

        public HalfBlockColor(Color color)
        {
            this.color = color;
            if (_colorBehaviour == null)
            {
                _colorBehaviour = new SubtractiveHalfBlockColorBehaviour();
                _colorBehaviour.SetMapping();
            }
        }

        public override int GetHashCode()
        {
            return this.color.GetHashCode();
        }

        public bool Equals(HalfBlockColor other)
        {
            return this.color.Equals(other.color);
        }

        public override AbstractHalfBlockColor CombineColor(AbstractHalfBlockColor other)
        {
            return _colorBehaviour.CombineColor(this, other);
        }
    }
}