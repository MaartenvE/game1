using UnityEngine;

public interface IGameObject
{
    IRenderer renderer { get; }
    ITransform transform { get; }
    INetwork network { get; }
    INetworkView networkView { get; }

    IGameObject Clone();

    T AddComponent<T>() where T : Component;
    T GetComponent<T>() where T : Component;
    T[] GetComponentsInChildren<T>() where T : Component;

    IGameObject Find(string name);
}