using UnityEngine;

public interface INetworkView
{
    IGameObject gameObject { get; }
    NetworkViewID networkViewId { get; }

    void RPC(string name, RPCMode mode, params object[] args);
    void RPC(string name, INetworkPlayer player, params object[] args);

    INetworkView Find(NetworkViewID viewId);
}
