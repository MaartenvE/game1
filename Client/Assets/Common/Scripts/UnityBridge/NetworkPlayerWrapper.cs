using UnityEngine;

namespace BuildingBlocks
{
    public class NetworkPlayerWrapper : INetworkPlayer
    {
        private NetworkPlayer wrappedObject;

        public NetworkPlayer NetworkPlayer
        {
            get
            {
                return wrappedObject;
            }
        }

        public NetworkPlayerWrapper(NetworkPlayer player)
        {
            wrappedObject = player;
        }

        public override string ToString()
        {
            return wrappedObject.ToString();
        }

        public override bool Equals(object obj)
        {
            INetworkPlayer p = obj as INetworkPlayer;
            if (p != null)
            {
                return wrappedObject.Equals(p.NetworkPlayer);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return wrappedObject.GetHashCode();
        }
    }
}
