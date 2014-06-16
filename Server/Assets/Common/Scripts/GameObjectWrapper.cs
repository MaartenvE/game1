using UnityEngine;

public class GameObjectWrapper : IGameObject
{
    private GameObject wrappedObject;
    private INetworkView wrappedNetworkView;

    public Renderer renderer
    {
        get
        {
            return wrappedObject ? wrappedObject.renderer : null;
        }
    }

    public Transform transform
    {
        get
        {
            return wrappedObject.transform;
        }
    }

    public INetworkView networkView
    {
        get
        {
            return wrappedNetworkView;
        }
    }

    public GameObjectWrapper(GameObject wrappedObject)
    {
        this.wrappedObject = wrappedObject;
        this.wrappedNetworkView = new NetworkViewWrapper(wrappedObject.networkView);
    }

    public T GetComponent<T>() where T : Component
    {
        return wrappedObject.GetComponent<T>();
    }

    public T[] GetComponentsInChildren<T>() where T : Component
    {
        return wrappedObject.GetComponentsInChildren<T>();
    }

    public IGameObject Find(string name)
    {
        GameObject gameObject = GameObject.Find(name);
        return gameObject ? new GameObjectWrapper(gameObject) : null;
    }
}
