using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace BuildingBlocks.Input
{
    public class Magnetometer : IMagnetometer
    {
        /// <summary>
        /// The minimum change in microteslas to be considered changing.
        /// </summary>
        private const float CHANGING_THRESHOLD = 3.0f;

        /// <summary>
        /// The number of old magnetometer readings to be kept.
        /// </summary>
        private const int MAGNETOMETER_HISTORY_SIZE = 20;

        private IMagnetometerInput input;

        /// <summary>
        /// A list of previous magnetometer readings.
        /// </summary>
        private Queue<float> magnetometerHistory = new Queue<float>();

        public Magnetometer(IMagnetometerInput magnetometerInput)
        {
            this.input = magnetometerInput;
        }

        public Vector3 Magnetisation
        {
            get
            {
                return input.Magnetisation;
            }
        }

        /// <summary>
        /// Update magnetometer readings. Should be called at the end of each frame.
        /// </summary>
        public void Update()
        {
            if (magnetometerHistory.Count == MAGNETOMETER_HISTORY_SIZE)
            {
                magnetometerHistory.Dequeue();
            }
            magnetometerHistory.Enqueue(Magnetisation.magnitude);
        }

        /// <summary>
        /// Checks if a change is detected in the magnetometer readings.
        /// </summary>
        public bool IsChanging()
        {
            if (magnetometerHistory.Count > 0)
            {
                float average = magnetometerHistory.Average();

                float min = magnetometerHistory.Min();
                float max = magnetometerHistory.Max();

                return (min - average <= -CHANGING_THRESHOLD
                        || max - average >= CHANGING_THRESHOLD);
            }
            return false;
        }
    }
}
