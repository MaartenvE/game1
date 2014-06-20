using UnityEngine;
using System.Collections;
using System;

namespace BuildingBlocks.HalfBlock
{

    public class ColorPair : IEquatable<ColorPair>
    {
        public AbstractHalfBlockColor first;
        public AbstractHalfBlockColor second;

        public ColorPair(AbstractHalfBlockColor first, AbstractHalfBlockColor second)
        {
            this.first = first;
            this.second = second;
        }

        public override int GetHashCode()
        {
            return (first.GetHashCode() + second.GetHashCode());
        }

        public bool Equals(ColorPair other)
        {
            if (this.first.Equals(other.first) && this.second.Equals(other.second))
            {
                return true;
            }
            return this.first.Equals(other.second) && this.second.Equals(other.first);

        }
    }
}