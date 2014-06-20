using UnityEngine;

namespace BuildingBlocks
{
    public interface INetworkPlayer
    {
        NetworkPlayer NetworkPlayer { get; }

        string ToString();

        bool Equals(object obj);
    }
}
