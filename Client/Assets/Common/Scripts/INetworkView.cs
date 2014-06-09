using UnityEngine;

public interface INetworkView
{
    IGameObject gameObject { get; }
    NetworkViewID networkViewId { get; }

    void RPC(string name, RPCMode mode, params object[] args);
    INetworkView Find(NetworkViewID viewId);
}
