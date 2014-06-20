using System;
using UnityEngine;
using BuildingBlocks.Team;
using BuildingBlocks.Player;
using BuildingBlocks.Input;

namespace BuildingBlocks.CubeFinger
{
    public class CubeFinger : BaseCubeFinger
    {
        private CubeFingerPositioner positioner;

        private IGameObject pickedObject;
        private Vector3 displacement;

        public CubeFinger(IGameObject gameObject) : base(gameObject)
        {
            Hide = true;
            positioner = new CubeFingerPositioner(this);
        }

        public override void Update()
        {
            if (ImageTarget != null && ImageTarget.CurrentStatus != TrackableBehaviour.Status.NOT_FOUND)
            {
                Hide = false;
                updateFinger();
            }
            Renderer.Update();
        }

        private void updateFinger()
        {
            if (IsMine && Mode != CubeFingerMode.None)
            {
                bool show = positioner.CalculateDisplacement(out pickedObject, out displacement);

                if (show)
                {
                    Vector3 position = pickedObject.transform.localPosition + displacement * gameObject.transform.localScale.x;
                    Renderer.MoveFinger(pickedObject, displacement);
                }

                Renderer.ShowFinger(show);
            }
        }

        private void placeObject(IGameObject pickedObject, Vector3 displacement)
        {
            if (pickedObject != null)
            {
                pickedObject.GetComponent<BlockBehaviour>().Place(displacement);
            }
        }

        private void removeObject(IGameObject pickedObject)
        {
            if (pickedObject != null)
            {
                pickedObject.GetComponent<BlockBehaviour>().Remove();
                Renderer.IsObjectRemoved = true;
            }
        }

        private void handleTouch()
        {
            switch (Mode)
            {
                case CubeFingerMode.Build:
                    placeObject(pickedObject, displacement);
                    break;
                case CubeFingerMode.Delete:
                    removeObject(pickedObject);
                    break;
            }
        }

        public override void RPC_SetPersonalFinger()
        {
            base.RPC_SetPersonalFinger();
            TouchDetectorLoader.Detector.OnTouch += handleTouch;
            Player.Player.LocalPlayer.CubeFinger = this;
        }
    }
}
