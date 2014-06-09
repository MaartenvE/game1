using UnityEngine;

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

    public NetworkViewID networkViewId
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

    public INetworkView Find(NetworkViewID viewId)
    {
        NetworkView networkView = NetworkView.Find(viewId);
        return networkView ? new NetworkViewWrapper(networkView) : null;
    }
}
