using UnityEngine;

public interface IGameObject
{
    IRenderer renderer { get; }
    ITransform transform { get; }
    INetwork network { get; }
    INetworkView networkView { get; }

    T GetComponent<T>() where T : Component;
    T[] GetComponentsInChildren<T>() where T : Component;

    IGameObject Find(string name);
}