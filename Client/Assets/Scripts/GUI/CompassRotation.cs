using UnityEngine;

namespace BuildingBlocks.GUI
{
    public class CompassRotation : MonoBehaviour
    {
        private GameObject GameCamera;

        void Start()
        {
            GameCamera = GameObject.Find("ARCamera");
        }

        void Update()
        {
            if (Player.Player.LocalPlayer != null && Player.Player.LocalPlayer.Team != null)
            {
                bool myTarget = Player.Player.LocalPlayer.Team.Target == transform.parent.name;
                {
                    foreach (Transform direction in this.transform)
                    {
                        direction.renderer.enabled = myTarget ? true : false;
                        direction.transform.LookAt(GameCamera.transform.position);
                        //rotate letters right side up
                        direction.transform.Rotate(new Vector3(180f, 0f, 180f));
                    }
                }                
            }
        }
    }
}
