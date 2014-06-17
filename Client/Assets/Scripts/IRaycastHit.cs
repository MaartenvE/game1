using UnityEngine;

    public interface IRaycastHit
    {
        void SetNativeRaycastHit(RaycastHit hit);
        Transform transform();
        Vector3 point();
    }
