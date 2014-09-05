using UnityEngine;

namespace BuildingBlocks.GUI
{
    public class CrosshairBehaviour : MonoBehaviour
    {

        private GameObject[] crosshairLines;
        private bool destroy;

        void Start()
        {
            crosshairLines = GameObject.FindGameObjectsWithTag("crosshair");
            destroy = false;
        }

        public void CycleModes()
        {
            destroy = !destroy;
            foreach (GameObject line in crosshairLines)
            {
                line.transform.renderer.material.color = destroy ? Color.red : Color.white;
                line.transform.Rotate(line.transform.rotation.x, 45, line.transform.rotation.z);
            }
        }
    }
}
