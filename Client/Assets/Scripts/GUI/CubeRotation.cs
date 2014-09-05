using UnityEngine;

namespace BuildingBlocks.GUI
{
    public class CubeRotation : MonoBehaviour
    {
        // Rotate block
        void Update()
        {
            float r = Time.deltaTime * 25.0f;
            transform.Rotate(r, r / 2, r / 3);
        }
    }
}
