using UnityEngine;
using System.Collections;

namespace BuildingBlocks.HalfBlock
{
    public interface IHalfBlockColorBehaviour
    {
        AbstractHalfBlockColor CombineColor(AbstractHalfBlockColor first, AbstractHalfBlockColor second);
        void SetMapping();
    }
}
