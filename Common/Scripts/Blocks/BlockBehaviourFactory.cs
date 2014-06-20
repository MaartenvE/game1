using UnityEngine;
using System.Collections;

namespace BuildingBlocks.Blocks
{
    public class BlockBehaviourFactory : MonoBehaviour
    {
        public string BlockBehaviourType = "RemovableBlockBehaviour";

        void Awake()
        {
            gameObject.AddComponent(BlockBehaviourType);
        }
    }
}
