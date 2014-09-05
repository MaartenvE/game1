using UnityEngine;

namespace BuildingBlocks.Input
{
    /// <summary>
    /// Load the default bump detector.
    /// </summary>
    public class BumpDetectorLoader : MonoBehaviour
    {
        public static BumpDetector Detector;

        void Start()
        {
            Detector = new BumpDetector(
                new Accelerometer(new UnityAccelerometerInput()),
                new Magnetometer(new UnityMagnetometerInput()));

            Detector.OnBump +=
                (bump) => networkView.RPC("Tap", RPCMode.Server, bump.Force);
        }

        void Update()
        {
            Detector.DetectBump();
        }

        [RPC]
        void Tap(float force) { }

        [RPC]
        void BumpMatch()
        {
            Handheld.Vibrate();
        }
    }
}
