using UnityEngine;
using System.Collections.Generic;

namespace BuildingBlocks.Input
{
    public class Magnetometer : MonoBehaviour
    {
        /// <summary>
        /// Smoothing factor for the exponential smoothing average.
        /// </summary>
        private const float ALPHA = 0.1f;

        public float Average { get; private set; }
        public float Score
        {
            get
            {
                float magnitude = UnityEngine.Input.compass.rawVector.magnitude;
                Average = (1 - ALPHA) * Average + ALPHA * magnitude;
                return Mathf.Abs((magnitude - Average) / 100);
            }
        }

        void Awake()
        {
            UnityEngine.Input.compass.enabled = true;
        }
    }
}
