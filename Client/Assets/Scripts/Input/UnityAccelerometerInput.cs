using UnityEngine;

namespace BuildingBlocks.Input
{
    public class UnityAccelerometerInput : IAccelerometerInput
    {
        public Vector3 Acceleration
        {
            get
            {
                return UnityEngine.Input.acceleration;
            }
        }
    }
}
