using UnityEngine;

public interface IGameObject
{
    Transform transform { get; }
    INetworkView networkView { get; }

    T GetComponent<T>() where T : Component;
    IGameObject Find(string name);
}