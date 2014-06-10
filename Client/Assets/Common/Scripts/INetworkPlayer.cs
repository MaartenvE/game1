using UnityEngine;

public interface INetworkPlayer
{
    NetworkPlayer NetworkPlayer { get; }

    string ToString();

    bool Equals(object obj);
}
