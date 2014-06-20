using UnityEngine;

namespace BuildingBlocks.Blocks
{
    public class GoalCubeBehaviour : MonoBehaviour
    {
        public void SetInfo(string parent, Color color)
        {
            networkView.RPC("SetBlockInfo", RPCMode.AllBuffered, parent, new Vector3(color.r, color.g, color.b));
        }

        [RPC]
        void SetBlockInfo(string parent, Vector3 color)
        {
            this.transform.parent = GameObject.Find(parent).transform;
            this.renderer.material.color = new Color(color.x, color.y, color.z, 0.5f);

            if (Network.isClient)
            {
                this.renderer.enabled = false;
            }
        }
    }
}
