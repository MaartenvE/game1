using UnityEngine;

public interface IGameObject
{
    GameObject GameObject { get; }

    Transform transform { get; }
    INetworkView networkView { get; }

    T GetComponent<T>() where T : Component;
    IGameObject Find(string name);
}