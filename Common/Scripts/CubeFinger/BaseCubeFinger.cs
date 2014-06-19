using UnityEngine;
using BuildingBlocks.Team;

namespace BuildingBlocks.CubeFinger
{
    public class BaseCubeFinger : TeamBehaviour, ICubeFinger
    {
        public event CubeFingerModeChangedHandler OnModeChanged;

        private CubeFingerMode mode;
        public CubeFingerMode Mode
        {
            get
            {
                return mode;
            }

            set
            {
                if (value != mode)
                {
                    mode = value;
                    if (OnModeChanged != null)
                    {
                        OnModeChanged(this, mode);
                    }
                }
            }
        }

        public bool Hide { get; protected set; }

        public bool IsMine { get; private set; }
        public ICubeFingerRenderer Renderer { get; private set; }

        public BaseCubeFinger(IGameObject gameObject, ICubeFingerRenderer renderer = null) : base(gameObject)
        {
            Renderer = renderer ?? new CubeFingerRenderer(this);
            OnModeChanged += SendModeChanged;
            Mode = CubeFingerMode.Build;
            Renderer.ShowFinger(false);
        }

        public virtual void OnPlayerConnected(INetworkPlayer player) { }
        public virtual void Update() { }
        public virtual void Destroy() { }

        public virtual void RPC_SetPersonalFinger()
        {
            IsMine = true;
        }

        public virtual void RPC_SetFingerParent(string parent)
        {
            gameObject.transform.parent = gameObject.Find(parent).transform;
            gameObject.transform.localRotation = new Quaternion();
            UpdateTeam();
        }

        public virtual void RPC_SetFingerMode(int mode)
        {
            if (!IsMine)
            {
                this.Mode = (CubeFingerMode)mode;
            }
        }

        private void SendModeChanged(object sender, CubeFingerMode mode)
        {
            if (IsMine)
            {
                networkView.RPC("SetFingerMode", RPCMode.Server, (int) mode);
            }

            if (Network.isServer)
            {
                networkView.RPC("SetFingerMode", RPCMode.Others, (int) mode);
            }
        }
    }
}