using UnityEngine;

namespace BuildingBlocks.GUI
{
    public class CompassRotation : MonoBehaviour
    {
        public GameObject GameCamera;

        void Update()
        {
            foreach (Transform direction in this.transform)
            {
                direction.transform.LookAt(GameCamera.transform.position);
                //rotate letters right side up
                direction.transform.Rotate(new Vector3(180f, 0f, 180f));                
            }
        }
    }
}
