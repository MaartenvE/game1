using UnityEngine;

namespace BuildingBlocks.Team
{
    public class TeamBehaviour : BuildingBlocksBehaviour
    {
        public ImageTargetBehaviour ImageTarget { get; private set; }

        protected TeamBehaviour(IGameObject gameObject) : base(gameObject)
        {

        }

        protected void UpdateTeam()
        {
            Transform parent = gameObject.transform.parent;
            if (parent != null)
            {
                ImageTarget = parent.GetComponent<ImageTargetBehaviour>();
            }
        }

    }
}
