using UnityEngine;
using System.Collections.Generic;

namespace BuildingBlocks.Client
{
    public class CameraBehaviourLoader : MonoBehaviour, ILoadLevelEventHandler
    {
        void Start()
        {
            KeepAliveAbstractBehaviour keepAlive = KeepAliveBehaviour.Instance;
            if (keepAlive != null)
            {
                keepAlive.RegisterEventHandler(this);
            }

            Application.LoadLevel(1);
        }

        public void OnLevelLoaded(IEnumerable<TrackableBehaviour> keptAliveTrackables)
        {
            switch (Application.loadedLevel)
            {
                case 1: // QRCodeScene
                    gameObject.AddComponent<QRScanner>();
                    CameraDevice.Instance.Start();
                    break;
                case 2: // StartScreenScene
                    CameraDevice.Instance.Stop();
                    Destroy(gameObject.GetComponent<QRScanner>());
                    transform.Find("Crosshair").gameObject.SetActive(false);
                    break;
                case 3: // Client
                    transform.Find("Crosshair").gameObject.SetActive(true);
                    CameraDevice.Instance.Start();
                    break;
            }
        }

        // UNUSED
        public void OnDuplicateTrackablesDisabled(IEnumerable<TrackableBehaviour> disabledTrackables)
        {

        }
    }
}
