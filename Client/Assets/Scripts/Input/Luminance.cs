using UnityEngine;
using System.Collections.Generic;

namespace BuildingBlocks.Input
{
    /// <summary>
    /// Get the average luminance of the camera image by analyzing pixel data.
    /// </summary>
    public class Luminance : MonoBehaviour
    {
        /// <summary>
        /// Number of pixels to skip after every analyzed pixel. Lower values increase
        /// accuracy, but also the number of operations required.
        /// </summary>
        private const int PIXEL_SKIP = 3;

        /// <summary>
        /// Smoothing factor for the exponential smoothing average.
        /// </summary>
        private const float ALPHA = 0.15f;

        public float Average { get; private set; }
        public float Score
        {
            get
            {
                return checkBump();
            }
        }

        private Image.PIXEL_FORMAT pixelFormat = Image.PIXEL_FORMAT.RGB888;
        private bool registeredFormat = false;

        void Start()
        {
            Average = -1;
        }

        private float checkBump()
        {
            if (!registeredFormat)
            {
                CameraDevice.Instance.SetFrameFormat(pixelFormat, true);
                registeredFormat = true;
            }

            Image image = CameraDevice.Instance.GetCameraImage(pixelFormat);
            if (image != null)
            {
                byte[] pixels = image.Pixels;
                float totalLuminance = 0.0f;

                // 4 bytes per pixel
                int count = 0;
                for (int p = 0; p < pixels.Length; p += 4 + 4 * PIXEL_SKIP)
                {
                    totalLuminance += (pixels[p] * 3 + pixels[p + 1] * 4 + pixels[p + 2]) >> 3;
                    count++;
                }

                totalLuminance /= (count * 255);

                if (Average < 0)
                {
                    Average = totalLuminance;
                }
                else
                {
                    Average = (1 - ALPHA) * Average + ALPHA * totalLuminance;
                }
                return Average - totalLuminance;
            }

            return 0;
        }
    }
}
