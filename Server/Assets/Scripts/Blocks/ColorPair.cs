using UnityEngine;
using System;

namespace BuildingBlocks.Blocks
{
    class ColorPair : IEquatable<ColorPair>
    {
        Color color1, color2;

        public ColorPair(Color color1, Color color2)
        {
            this.color1 = color1;
            this.color2 = color2;
        }

        public override int GetHashCode()
        {
            return (color1.GetHashCode() + color2.GetHashCode());
        }

        public bool Equals(ColorPair other)
        {
            return this.color1.Equals(other.color1) && this.color2.Equals(other.color2)
                || this.color2.Equals(other.color1) && this.color1.Equals(other.color2);
        }
    }
}