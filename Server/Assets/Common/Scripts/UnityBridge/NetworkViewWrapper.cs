using UnityEngine;

namespace BuildingBlocks
{
    public class NetworkViewWrapper : INetworkView
    {
        private NetworkView wrappedObject;

        public IGameObject gameObject
        {
            get
            {
                return new GameObjectWrapper(wrappedObject.gameObject);
            }
        }

        public NetworkViewID viewID
        {
            get
            {
                return wrappedObject.viewID;
            }
        }

        public NetworkViewWrapper(NetworkView networkView)
        {
            wrappedObject = networkView;
        }

        public void RPC(string name, RPCMode mode, params object[] args)
        {
            wrappedObject.RPC(name, mode, args);
        }

        public void RPC(string name, INetworkPlayer player, params object[] args)
        {
            wrappedObject.RPC(name, player.NetworkPlayer, args);
        }

        public INetworkView Find(NetworkViewID viewId)
        {
            NetworkView networkView = NetworkView.Find(viewId);
            return networkView ? new NetworkViewWrapper(networkView) : null;
        }
    }
}
