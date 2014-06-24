using UnityEngine;
using System.Collections.Generic;

namespace BuildingBlocks.GUI
{
    public class CameraBehaviourLoader : MonoBehaviour, ILoadLevelEventHandler
    {
        private GameObject textureBufferCamera;

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
            if (textureBufferCamera == null)
            {
                textureBufferCamera = GameObject.Find("TextureBufferCamera");
            }

            switch (Application.loadedLevel)
            {
                case 1: // StartScreenScene
                    Destroy(gameObject.GetComponent<QRScanner>());
                    textureBufferCamera.SetActive(false);
                    break;
                case 2: // Client
                    textureBufferCamera.SetActive(true);
                    transform.Find("Crosshair").gameObject.SetActive(true);
                    break;
            }
        }

        // UNUSED
        public void OnDuplicateTrackablesDisabled(IEnumerable<TrackableBehaviour> disabledTrackables)
        {

        }
    }
}
