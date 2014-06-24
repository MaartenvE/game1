using UnityEngine;
using System.Collections;

namespace BuildingBlocks.HalfBlock
{
    public class HalfBlockDecorator : HalfBlock
    {

        public override Color CalculateUnityColor()
        {
            return wrappedObject.CalculateUnityColor();
        }

    }
}