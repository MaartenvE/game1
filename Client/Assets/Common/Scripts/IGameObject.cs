using UnityEngine;

public interface IGameObject
{
    //GameObject GameObject { get; }

    Renderer renderer { get; }
    Transform transform { get; }
    INetworkView networkView { get; }

    T GetComponent<T>() where T : Component;
    T[] GetComponentsInChildren<T>() where T : Component;

    IGameObject Find(string name);
}