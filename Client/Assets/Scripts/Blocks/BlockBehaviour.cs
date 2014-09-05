using UnityEngine;
using System;
using BuildingBlocks.Team;

namespace BuildingBlocks.Blocks
{
    public class BlockBehaviour : MonoBehaviour
    {
        // todo: there has to be a better way to hide blocks on start
        void Update()
        {
            if (this.transform.parent.GetComponent<ImageTargetBehaviour>().CurrentStatus == TrackableBehaviour.Status.NOT_FOUND)
            {
                this.renderer.enabled = false;
            }
        }

        public void Place(Vector3 direction)
        {
            networkView.RPC("PlaceNewBlock", RPCMode.Server, direction);
        }

        public void Remove()
        {
            networkView.RPC("RemoveBlock", RPCMode.Server);
        }

        [RPC]
        void SetBlockInfo(int teamId, Vector3 location, Vector3 color)
        {
            ITeam team = Team.Team.GetTeam(teamId);

            this.transform.parent = GameObject.Find(team.Target).transform as Transform;
            this.transform.localPosition = location;
            this.transform.localRotation = new Quaternion(0, 0, 0, 0);
            this.renderer.material.color = new Color(color.x, color.y, color.z);
        }

        [RPC]
        void PlaceNewBlock(Vector3 direction) { }

        [RPC]
        void RemoveBlock() { }
    }
}
