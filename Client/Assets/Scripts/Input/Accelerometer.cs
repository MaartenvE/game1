using UnityEngine;

namespace BuildingBlocks.Input
{
    public class Accelerometer : IAccelerometer
    {
        /// <summary>
        /// The threshold above which the negative acceleration must lie.
        /// </summary>
        private const float ACCELERATION_THRESHOLD = -0.1f;

        /// <summary>
        /// The threshold above which the positive acceleration must lie.
        /// </summary>
        private const float DECELERATION_THRESHOLD = 0.1f;

        /// <summary>
        /// The threshold below which the total acceleration on all three axes 
        /// combined should lie to be considered stationary.
        /// </summary>
        private const float STATIONARY_THRESHOLD = 0.1f;

        /// <summary>
        /// The maximum deviation from being exactly upright allowed to be 
        /// considered upright.
        /// </summary>
        private const float UPRIGHT_THRESHOLD = 0.1f;

        private IAccelerometerInput input;

        private float previousAcceleration = 0.0f;
        private float currentAcceleration = 0.0f;

        public Accelerometer(IAccelerometerInput accelerometerInput)
        {
            this.input = accelerometerInput;
        }

        public Vector3 Acceleration
        {
            get
            {
                return input.Acceleration;
            }
        }

        /// <summary>
        /// Update accelerometer readings. Should be called at the end of each frame.
        /// </summary>
        public void Update()
        {
            previousAcceleration = currentAcceleration;
            currentAcceleration = Acceleration.z;
        }

        /// <summary>
        /// Checks if the phone is moved in the direction of its back.
        /// </summary>
        public bool IsAccelerating()
        {
            return Acceleration.z - previousAcceleration < ACCELERATION_THRESHOLD;
        }

        /// <summary>
        /// Checks if the phone is moved in the direction of its front.
        /// </summary>
        public bool IsDecelerating()
        {
            return Acceleration.z - previousAcceleration > DECELERATION_THRESHOLD;
        }

        /// <summary>
        /// Checks if the phone is held still in the air.
        /// </summary>
        public bool IsStationary()
        {
            // Since the phone's accelerometer measures gravitational forces, 
            // 1g should be subtracted from the total acceleration magnitude to get 
            // user acceleration.
            return Mathf.Abs(Acceleration.magnitude - 1) < STATIONARY_THRESHOLD;
        }

        /// <summary>
        /// Checks if the phone is held upright (bottom down).
        /// </summary>
        public bool IsUpright()
        {
            // Since the phone's accelerometer measures gravitational forces, 
            // the force along the y-axis should be about -1g when the phone is 
            // upright.
            return Mathf.Abs(Acceleration.y + 1) < UPRIGHT_THRESHOLD;
        }
    }
}
