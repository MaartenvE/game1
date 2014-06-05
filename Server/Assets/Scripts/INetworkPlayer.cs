using UnityEngine;

public interface INetworkPlayer
{
    NetworkPlayer getNetworkPlayer();

    string ToString();

    bool Equals(object obj);
}
