using UnityEngine;

public class GameObjectWrapper : IGameObject
{
    private GameObject wrappedObject;
    private INetworkView wrappedNetworkView;

    public IRenderer renderer
    {
        get
        {
            return wrappedObject ? new RendererWrapper(wrappedObject.renderer) : null;
        }
    }

    public ITransform transform
    {
        get
        {
            return new TransformWrapper(wrappedObject.transform);
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
