using UnityEngine;

namespace BuildingBlocks.Blocks
{
    public delegate void StructureCompleteHandler();
    public delegate void StructureProgressHandler(float progress);

    public interface IStructureTracker
    {
        event StructureCompleteHandler OnCompletion;
        event StructureProgressHandler OnProgressChange;

        float Progress { get; }
        float RawProgress { get; }

        void PlaceGroundBlock();
        bool PlaceBlock(Vector3 location, Color color);
        void RemoveBlock(IGameObject block);
    }
}
