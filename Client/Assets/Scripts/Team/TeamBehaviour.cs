using UnityEngine;

namespace BuildingBlocks.Team
{
    public class TeamBehaviour : BuildingBlocksBehaviour
    {
        public ImageTargetBehaviour ImageTarget { get; private set; }
        public TeamInfo Team { get; private set; }

        protected TeamBehaviour(IGameObject gameObject) : base(gameObject)
        {

        }

        protected void UpdateTeam()
        {
            ITransform target = gameObject.transform.parent;
            if (target != null)
            {
                ImageTarget = target.GetComponent<ImageTargetBehaviour>();
                ITransform parent = target.parent;
                if (parent != null)
                {
                    Team = parent.GetComponent<TeamInfoLoader>().TeamInfo;
                }
            }
        }

    }
}
