using UnityEngine;

public class GameObjectWrapper : IGameObject
{
    private GameObject wrappedObject;
    public GameObject GameObject
    {
        get
        {
            return wrappedObject;
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
            return new NetworkViewWrapper(wrappedObject.networkView);
        }
    }

    public GameObjectWrapper(GameObject wrappedObject)
    {
        this.wrappedObject = wrappedObject;
    }

    public T GetComponent<T>() where T : Component
    {
        return wrappedObject.GetComponent<T>();
    }

    public IGameObject Find(string name)
    {
        GameObject gameObject = GameObject.Find(name);
        return gameObject ? new GameObjectWrapper(gameObject) : null;
    }
}
