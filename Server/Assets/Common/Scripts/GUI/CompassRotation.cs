using UnityEngine;

namespace BuildingBlocks.GUI
{
    public class CompassRotation : MonoBehaviour
    {
        public GameObject GameCamera;

        void Update()
        {
            foreach (Transform transform in this.transform)
            {
                transform.transform.LookAt(GameCamera.transform.position);
                //rotate letters right side up
                transform.transform.Rotate(new Vector3(180f, 0f, 180f));
            }
        }
    }
}
