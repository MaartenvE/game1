using UnityEngine;
using System.Collections;

namespace BuildingBlocks.HalfBlock
{
    public class HalfBlockDecorator : HalfBlock
    {

        public virtual Color CalculateUnityColor()
        {
            return wrappedObject.CalculateUnityColor();
        }

    }
}