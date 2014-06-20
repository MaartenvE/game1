using UnityEngine;

namespace BuildingBlocks.Blocks
{
    public interface IBlockConstructor
    {
        float Scale { get; }
        void PlaceGroundBlock(Color color);
        void PlaceBlock(Vector3 location, Color color);
        void RemoveBlock(IGameObject block);
    }
}
