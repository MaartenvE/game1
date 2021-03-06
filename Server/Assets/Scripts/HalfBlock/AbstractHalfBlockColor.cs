﻿using System.Collections;
using System;
using UnityEngine;

namespace BuildingBlocks.HalfBlock
{
    public abstract class AbstractHalfBlockColor : IEquatable<AbstractHalfBlockColor>
    {
        private Color _color;
        public bool isSecondaryColor;

        public Color color
        {
            get { return _color; }
            protected set { _color = value; }
        }

        public override int GetHashCode()
        {
            return _color.GetHashCode();
        }

        public bool Equals(AbstractHalfBlockColor other)
        {
            return this.color.Equals(other.color);
        }

        public abstract AbstractHalfBlockColor CombineColor(AbstractHalfBlockColor other);

    }
}
