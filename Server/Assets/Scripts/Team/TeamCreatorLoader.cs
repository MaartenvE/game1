using UnityEngine;

namespace BuildingBlocks.Team
{
    public class TeamCreatorLoader : MonoBehaviour, ITeamInstantiator
    {
        public static TeamCreator Creator { get; private set; }

        public ITeam InstantiateTeam()
        {
            GameObject teamPrefab = Resources.Load("Team") as GameObject;
            GameObject teamObject = Network.Instantiate(teamPrefab, Vector3.zero, new Quaternion(), 1) as GameObject;
            return teamObject.GetComponent<TeamLoader>().Team;
        }

        void OnServerInitialized()
        {
            Creator = new TeamCreator(new GameObjectWrapper(gameObject), this);
            Creator.InstantiateGroundBlocks();
        }

        void OnPlayerDisconnected(NetworkPlayer player)
        {
            Creator.OnPlayerDisconnected(new NetworkPlayerWrapper(player));
        }

        [RPC]
        void SelectTeam(int spectate, NetworkMessageInfo info)
        {
            Creator.SelectTeam(new NetworkPlayerWrapper(info.sender), spectate != 0);
        }
    }
}
