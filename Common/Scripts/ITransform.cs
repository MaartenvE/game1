using UnityEngine;

public interface ITransform : IGameObject
{
    Transform Transform { get; }
    ITransform parent { get; set; }
    Quaternion localRotation { get; set; }
    Vector3 localPosition { get; set; }
    Vector3 localScale { get; set; }

    Vector3 InverseTransformPoint(Vector3 point);
}
