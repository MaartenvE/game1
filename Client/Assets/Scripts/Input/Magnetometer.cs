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

        public float Average;
        public float Score;

        void Awake()
        {
            UnityEngine.Input.compass.enabled = true;
        }

        void Update()
        {
            float magnitude = UnityEngine.Input.compass.rawVector.magnitude;
            Average = (1 - ALPHA) * Average + ALPHA * magnitude;
            Score = Mathf.Abs((magnitude - Average) / 100);
        }
    }
}
