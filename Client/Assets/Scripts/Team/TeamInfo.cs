using UnityEngine;

namespace BuildingBlocks.Team
{
    public class TeamInfo : BuildingBlocksBehaviour
    {
        public int ID { get; private set; }
        public float Progress { get; private set; }
        public string Name { get; private set; }
        public string ImageTarget { get; private set; }

        /// <summary>
        /// Determine if the team represented by this TeamInfo object belongs to the local player. 
        /// This requires a PlayerInfo script to be present.
        /// </summary>
        public bool IsMine
        {
            get
            {
                return PlayerInfo.Team == ID;
            }
        }

        public TeamInfo(IGameObject gameObject) : base(gameObject) { }

        /// <summary>
        /// When the Team object to which this TeamInfo component belongs is instantiated, it should 
        /// be grouped under the Teams object.
        /// </summary>
        public void OnNetworkInstantiate()
        {
            gameObject.transform.parent = gameObject.Find("Teams").transform;
        }

        /// <summary>
        /// Send team information (team id, name and target) to all clients.
        /// </summary>
        public void SetInfo(int id, string name, string imageTarget)
        {
            gameObject.networkView.RPC("SetTeamInfo", RPCMode.AllBuffered, id, name, imageTarget);
        }

        /// <summary>
        /// Send team progress to all clients.
        /// </summary>
        public void SetProgress(float progress)
        {
            gameObject.networkView.RPC("SetTeamProgress", RPCMode.AllBuffered, progress);
        }

        /// <summary>
        /// Receive team progress from server.
        /// </summary>
        public void RPC_SetTeamProgress(float progress)
        {
            this.Progress = progress;
        }

        /// <summary>
        /// Receive team info from server.
        /// </summary>
        public void RPC_SetTeamInfo(int id, string name, string imageTarget)
        {
            ID = id;
            Name = name;
            ImageTarget = imageTarget;

            IGameObject target = gameObject.Find(imageTarget);
            target.transform.parent = gameObject.transform;
        }

        public static TeamInfo GetTeam(int teamId)
        {
            TeamInfoLoader[] teamLoaders = GameObject.Find("Teams").GetComponentsInChildren<TeamInfoLoader>();
            foreach (TeamInfoLoader loader in teamLoaders)
            {
                if (loader.TeamInfo.ID == teamId)
                {
                    return loader.TeamInfo;
                }
            }
            return null;
        }
    }
}
