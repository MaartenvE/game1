using UnityEngine;

namespace BuildingBlocks.CubeFinger
{
    public interface ICubeFingerRenderer : IBuildingBlocksBehaviour
    {
        Color FingerColor { get; set; }
        bool IsObjectRemoved { get; set; }

        void MoveFinger(IGameObject pickedObject, Vector3 displacement);
        void ShowFinger(bool show);
        void SetColor(Color color);
    }
}
