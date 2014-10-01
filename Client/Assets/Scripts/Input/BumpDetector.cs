using UnityEngine;

namespace BuildingBlocks.Input
{
    public class BumpDetector : MonoBehaviour
    {
        private Bumpiness bumpiness;
        private const int CHECK_RATE = 10;
        private const float CHECK_DELAY = 1f / CHECK_RATE;

        void Start()
        {
            bumpiness = gameObject.AddComponent<Bumpiness>();
            InvokeRepeating("checkBump", CHECK_DELAY, CHECK_DELAY);
        }

        void checkBump()
        {
            float score;
            if (bumpiness.CheckBump(out score))
            {
                networkView.RPC("Tap", RPCMode.Server, score);
                bumpiness.Reset();
            }
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
