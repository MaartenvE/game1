using UnityEngine;

namespace BuildingBlocks.CubeFinger
{
    public class CubeFinger : BaseCubeFinger
    {
        public CubeFinger(IGameObject gameObject) : base(gameObject)
        {

        }

        public void SetParent(string parent)
        {
            networkView.RPC("SetFingerParent", RPCMode.AllBuffered, parent);
        }

        public void SetPlayer(INetworkPlayer player)
        {
            networkView.RPC("SetPersonalFinger", player);
        }

        public override void OnPlayerConnected(INetworkPlayer player)
        {
            networkView.RPC("SetFingerMode", player, (int) Mode);
            networkView.RPC("ColorFinger", player, ColorModel.ConvertToVector3(Renderer.FingerColor));
        }

        public override void Destroy()
        {
            network.RemoveRPCs(networkView.viewID);
            network.Destroy(networkView.viewID);
        }
    }
}