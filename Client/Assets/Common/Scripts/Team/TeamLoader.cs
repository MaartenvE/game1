using UnityEngine;

namespace BuildingBlocks.Team
{
    public class TeamLoader : MonoBehaviour
    {
        public ITeam Team { get; private set; }

        void OnNetworkInstantiate(NetworkMessageInfo info)
        {
            Team = new Team(new GameObjectWrapper(gameObject));
        }

        [RPC]
        void SetTeamInfo(int teamId, string teamName, string imageTarget)
        {
            Team.RPC_SetTeamInfo(teamId, teamName, imageTarget);
        }

        [RPC]
        void SetTeamProgress(float progress)
        {
            Team.RPC_SetTeamProgress(progress);
        }
    }
}
