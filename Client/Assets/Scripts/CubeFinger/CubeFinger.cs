using System;
using UnityEngine;
using BuildingBlocks.Team;
using BuildingBlocks.Player;

namespace BuildingBlocks.CubeFinger
{
    public class CubeFinger : BaseCubeFinger
    {
        private CubeFingerPositioner positioner;
        private ClickEventHandler clicker;

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
                if (IsMine && Mode != CubeFingerMode.None)
                {
                    IGameObject pickedObject;
                    Vector3 displacement;
                    bool show = positioner.CalculateDisplacement(out pickedObject, out displacement);

                    if (show)
                    {
                        Vector3 position = pickedObject.transform.localPosition;
                        if (Mode == CubeFingerMode.Build)
                        {
                            position += displacement * gameObject.transform.localScale.x;
                        }

                        Renderer.MoveFinger(pickedObject, displacement);
                        show = !handleClick(pickedObject, displacement);
                    }

                    Renderer.ShowFinger(show);
                }
            }
            Renderer.Update();
        }

        private bool handleClick(IGameObject pickedObject, Vector3 displacement)
        {
            if (clicker.SingleClick())
            {
                if (Mode == CubeFingerMode.Build)
                {
                    placeObject(pickedObject, displacement);
                }

                else if (Mode == CubeFingerMode.Delete)
                {
                    removeObject(pickedObject);
                }

                return true;
            }
            return false;
        }

        private void placeObject(IGameObject pickedObject, Vector3 displacement)
        {
            pickedObject.GetComponent<BlockBehaviour>().Place(displacement);
        }

        private void removeObject(IGameObject pickedObject)
        {
            pickedObject.GetComponent<BlockBehaviour>().Remove();
            Renderer.IsObjectRemoved = true;
        }

        // todo: cleaner attach to player?
        public override void RPC_SetPersonalFinger()
        {
            base.RPC_SetPersonalFinger();
            clicker = GameObject.Find("Client").GetComponent<ClickEventHandler>();
            Player.Player.LocalPlayer.CubeFinger = this;
        }
    }
}
