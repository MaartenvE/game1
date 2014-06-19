using UnityEngine;

namespace BuildingBlocks.Team
{
    public class TeamCreatorLoader : MonoBehaviour
    {
        public static TeamCreator Creator { get; private set; }

        void OnServerInitialized()
        {
            Creator = new TeamCreator(new GameObjectWrapper(gameObject));
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
