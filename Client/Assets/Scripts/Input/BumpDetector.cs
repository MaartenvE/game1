using UnityEngine;

namespace BuildingBlocks.Input
{
    public class BumpDetector : MonoBehaviour
    {
        private Bumpiness bumpiness;

        void Start()
        {
            bumpiness = gameObject.AddComponent<Bumpiness>();
            bumpiness.OnBump += (score) => networkView.RPC("Tap", RPCMode.Server, score);
        }

        void EnableDetector()
        {

        }

        void DisableDetector()
        {

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
