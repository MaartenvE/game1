using UnityEngine;
using BuildingBlocks.Team;

namespace BuildingBlocks.Blocks
{
    public class RemovableBlockBehaviour : BlockBehaviour
    {
        [RPC]
        void RemoveBlock()
        {
            team.StructureTracker.RemoveBlock(new GameObjectWrapper(gameObject));
        }
    }
}
