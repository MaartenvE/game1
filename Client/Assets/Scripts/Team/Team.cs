using UnityEngine;
using BuildingBlocks.Player;

namespace BuildingBlocks.Team
{
    public class Team : BuildingBlocksBehaviour, ITeam
    {
        private static IGameObject teamsObject;

        public int TeamId { get; private set; }
        public string Name { get; private set; }
        public string Target { get; private set; }
        public float Progress { get; private set; }

        public bool IsMine
        {
            get
            {
                return Player.Player.LocalPlayer.Team == this;
            }
        }

        /// <summary>
        /// When the Team object to which this TeamInfo component belongs is instantiated, it should 
        /// be grouped under the Teams object.
        /// </summary>
        public Team(IGameObject gameObject) : base(gameObject)
        {
            teamsObject = gameObject.Find("Teams");
            gameObject.transform.parent = teamsObject.transform;
        }

        public void RPC_SetTeamInfo(int teamId, string name, string target)
        {
            TeamId = teamId;
            Name = name;
            Target = target;

            setTargetTransform();
            cloneGoalStructure();
        }

        /// <summary>
        /// The ImageTarget gameObject should be grouped under the Team object it belongs to.
        /// </summary>
        private void setTargetTransform()
        {
            gameObject.Find(Target).transform.parent = gameObject.transform;
        }

        private void cloneGoalStructure()
        {
            IGameObject goalStructure = gameObject.Find("GoalStructure");
            IGameObject clone = goalStructure.Clone();
            clone.transform.parent = gameObject.Find(Target).transform;
            clone.transform.localPosition = Vector3.zero;
        }

        public void RPC_SetTeamProgress(float progress)
        {
            Progress = progress;
        }

        public static ITeam GetTeam(int teamId)
        {
            TeamLoader[] loaders = teamsObject.GetComponentsInChildren<TeamLoader>();
            foreach (TeamLoader loader in loaders)
            {
                if (loader.Team.TeamId == teamId)
                {
                    return loader.Team;
                }
            }
            return null;
        }

    }
}