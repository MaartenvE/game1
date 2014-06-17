using UnityEngine;
using System.Collections;

namespace BuildingBlocks.CubeFinger
{
    public class CubeFingerLoader : MonoBehaviour
    {
        public BaseCubeFinger Finger;

        void OnNetworkInstantiate(NetworkMessageInfo info)
        {
            Finger = new CubeFinger(new GameObjectWrapper(gameObject));
        }

        void OnPlayerConnected(NetworkPlayer networkPlayer)
        {
            Finger.OnPlayerConnected(new NetworkPlayerWrapper(networkPlayer));
        }

        void Update()
        {
            Finger.Update();
        }

        [RPC]
        void SetPersonalFinger()
        {
            Finger.RPC_SetPersonalFinger();
        }

        [RPC]
        void SetFingerParent(string parent)
        {
            Finger.RPC_SetFingerParent(parent);
        }

        [RPC]
        void SetFingerMode(int mode)
        {
            Finger.RPC_SetFingerMode(mode);
        }

        [RPC]
        void ShowFinger(int show)
        {
            CubeFingerRenderer renderer = Finger.Renderer as CubeFingerRenderer;
            renderer.RPC_ShowFinger(show);
        }

        [RPC]
        void ColorFinger(Vector3 color)
        {
            CubeFingerRenderer renderer = Finger.Renderer as CubeFingerRenderer;
            renderer.RPC_ColorFinger(color);
        }

        [RPC]
        void MoveFinger(NetworkViewID viewId, Vector3 displacement)
        {
            CubeFingerRenderer renderer = Finger.Renderer as CubeFingerRenderer;
            renderer.RPC_MoveFinger(viewId, displacement);
        }
    }
}
