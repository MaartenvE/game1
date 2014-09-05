using UnityEngine;

namespace BuildingBlocks.Input
{
    public interface IAccelerometer
    {
        Vector3 Acceleration { get; }
        void Update();
        bool IsAccelerating();
        bool IsDecelerating();
        bool IsStationary();
        bool IsUpright();
    }
}
