using UnityEngine;

namespace BuildingBlocks.CubeFinger
{
    /// <summary>
    /// CubeFingerRenderer is responsible for drawing the CubeFinger and hiding underlying blocks
    /// in delete mode.
    /// </summary>
    public class CubeFingerRenderer : BuildingBlocksBehaviour, ICubeFingerRenderer
    {
        private const float CUBEFINGER_ALPHA = 0.6f;

        private ICubeFinger finger;

        /// <summary>
        /// Because the finger's color changes in delete mode, the color of the block to place 
        /// should be remembered so it can be restored.
        /// </summary>
        public Color FingerColor { get; set; }
        private Vector3 previousPosition;
        private IGameObject hiddenBlock;

        private bool isVisible = true;
        private bool forceSendMove = false;
        public bool IsObjectRemoved { get; set; }

        public CubeFingerRenderer(ICubeFinger finger) : base(finger.gameObject)
        {
            this.finger = finger;
            this.finger.OnModeChanged += modeChanged;
        }

        public void Update()
        {
            gameObject.renderer.enabled = !finger.Hide && isVisible;
        }

        /// <summary>
        /// Move the cube finger to a position relative to an existing block.
        /// </summary>
        /// <param name="pickedObject">The block against which to place the finger.</param>
        /// <param name="displacement">A Vector3 with length 1 indicating the direction.</param>
        public void MoveFinger(IGameObject pickedObject, Vector3 displacement)
        {
            Vector3 localPosition = pickedObject.transform.localPosition 
                + displacement * pickedObject.transform.localScale.x;

            if (finger.Mode == CubeFingerMode.Delete)
            {
                hideBlock(pickedObject);
            }

            if (shouldSendMove(localPosition))
            {
                sendMove(pickedObject, displacement);
                setPosition(localPosition);
            }
        }

        private bool shouldSendMove(Vector3 position)
        {
            return finger.Mode != CubeFingerMode.None && (position != previousPosition || forceSendMove && !IsObjectRemoved);
        }

        private void hideBlock(IGameObject pickedObject)
        {
            enableHiddenBlock();
            Color color = pickedObject.renderer.material.color;
            color.a = CUBEFINGER_ALPHA;
            gameObject.renderer.material.color = color;
            pickedObject.renderer.enabled = false;
            hiddenBlock = pickedObject;
        }

        private void enableHiddenBlock()
        {
            if (hiddenBlock != null && hiddenBlock.renderer != null)
            {
                hiddenBlock.renderer.enabled = true;
                hiddenBlock = null;
            }
        }

        private void sendMove(IGameObject pickedObject, Vector3 displacement)
        {
            if (finger.IsMine)
            {
                networkView.RPC("MoveFinger", RPCMode.Server, pickedObject.networkView.viewID, displacement);
            }
            else if (network.isServer)
            {
                networkView.RPC("MoveFinger", RPCMode.Others, pickedObject.networkView.viewID, displacement);
            }

            forceSendMove = false;
            IsObjectRemoved = false;
        }

        private void setPosition(Vector3 localPosition)
        {
            if (localPosition != previousPosition)
            {
                gameObject.transform.localPosition = localPosition;
                previousPosition = localPosition;
            }
        }

        /// <summary>
        /// Show or hide this finger. If this finger is owned by the local player, notify
        /// the server of showing/hiding the finger.
        /// </summary>
        /// <param name="show">true if the finger should be shown, false otherwise.</param>
        public void ShowFinger(bool show)
        {
            if (show != isVisible)
            {
                isVisible = show;
                gameObject.renderer.enabled = !finger.Hide && show;
                enableHiddenBlock();

                sendShow(show);
            }
        }

        private void sendShow(bool show)
        {
            if (finger.IsMine)
            {
                forceSendMove = true;
                networkView.RPC("ShowFinger", RPCMode.Server, show ? 1 : 0);
            }
            else if (network.isServer)
            {
                networkView.RPC("ShowFinger", RPCMode.Others, show ? 1 : 0);
            }
        }

        /// <summary>
        /// Set the color of this finger. Since the finger color is controlled by the server,
        /// no data is sent by the client.
        /// </summary>
        /// <param name="color">The color the finger should get.</param>
        public void SetColor(Color color)
        {
            color.a = CUBEFINGER_ALPHA;
            this.FingerColor = color;
            gameObject.renderer.material.color = color;
            if (network.isServer)
            {
                networkView.RPC("ColorFinger", RPCMode.Others, ColorModel.ConvertToVector3(color));
            }
        }

        /// <summary>
        /// Show or hide the cubefinger depending on the new mode. If the new mode is not delete,
        /// reset the selected block and finger color.
        /// </summary>
        private void modeChanged(object sender, CubeFingerMode mode)
        {
            switch (mode)
            {
                case CubeFingerMode.Delete:
                case CubeFingerMode.Build:
                    ShowFinger(true);
                    break;
                default:
                    ShowFinger(false);
                    break;
            }

            if (mode != CubeFingerMode.Delete)
            {
                enableHiddenBlock();
                SetColor(FingerColor);
            }
        }

        public void RPC_ShowFinger(int show)
        {
            if (!finger.IsMine)
            {
                ShowFinger(show != 0);
            }
        }

        public void RPC_ColorFinger(Vector3 color)
        {
            SetColor(ColorModel.ConvertToUnityColor(color));
        }

        public void RPC_MoveFinger(NetworkViewID viewId, Vector3 displacement)
        {
            if (!finger.IsMine)
            {
                MoveFinger(networkView.Find(viewId).gameObject, displacement);
            }
        }
    }
}