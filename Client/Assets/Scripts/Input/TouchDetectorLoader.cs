using UnityEngine;

namespace BuildingBlocks.Input
{
    public class TouchDetectorLoader : MonoBehaviour
    {
        public static ITouchDetector Detector { get; private set; }

        void Start()
        {
            Detector = new TouchDetector(new UnityMouseInput());
        }

        void Update()
        {
            Detector.Update();
        }
    }
}
