using UnityEngine;

namespace BuildingBlocks.CubeFinger
{
    class CubeFinger : BaseCubeFinger
    {
        public CubeFinger(IGameObject gameObject) : base(gameObject)
        {

        }

        public void SetParent(string parent)
        {
            networkView.RPC("SetFingerParent", RPCMode.AllBuffered, parent);
        }

        public void SetPlayer(IPlayer player)
        {
            player.CubeFinger = this;
            networkView.RPC("SetPersonalFinger", player.NetworkPlayer);
            player.GiveInventoryBlock();
            Renderer.SetColor(player.HalfBlock.CalculateUnityColor());
        }

        public override void OnPlayerConnected(INetworkPlayer player)
        {
            networkView.RPC("SetFingerMode", player, (int) Mode);
            networkView.RPC("ColorFinger", player, ColorModel.ConvertToVector3(Renderer.FingerColor));
        }

        public override void Update()
        {

        }
    }
}