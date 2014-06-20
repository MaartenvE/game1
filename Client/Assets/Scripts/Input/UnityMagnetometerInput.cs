using UnityEngine;

namespace BuildingBlocks.Input
{
    public class UnityMagnetometerInput : IMagnetometerInput
    {
        public Vector3 Magnetisation
        {
            get
            {
                return UnityEngine.Input.compass.rawVector;
            }
        }
    }
}
