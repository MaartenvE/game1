using UnityEngine;
using System.Collections.Generic;

namespace BuildingBlocks.GUI
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

            gameObject.AddComponent<QRScanner>();
        }

        public void OnLevelLoaded(IEnumerable<TrackableBehaviour> keptAliveTrackables)
        {
            switch (Application.loadedLevel)
            {
                case 1: // StartScreenScene
                    CameraDevice.Instance.Stop();
                    Destroy(gameObject.GetComponent<QRScanner>());
                    break;
                case 2: // Client
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
