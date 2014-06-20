using UnityEngine;

namespace BuildingBlocks.Team
{
    public class TeamLoader : MonoBehaviour
    {
        public ITeam Team { get; private set; }
        private TeamProgressOverlay overlay;

        void OnNetworkInstantiate(NetworkMessageInfo info)
        {
            Team = new Team(new GameObjectWrapper(gameObject));
            overlay = new TeamProgressOverlay(Team);
        }

        void OnGUI()
        {
            overlay.OnGUI();
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

/*using UnityEngine;
using BuildingBlocks.CubeFinger;
using BuildingBlocks.Team;

namespace BuildingBlocks.Team
{
    public class TeamLoader : MonoBehaviour
    {
        private static ITeamManager teamManager;
        public static ITeamManager TeamManager
        {
            get
            {
                return teamManager;
            }
        }

        void OnServerInitialized()
        {
            teamManager = new TeamManager(new[] {
                new Team("Team 1", "ImageTarget1"),
                new Team("Team 2", "ImageTarget2")
            });

            instantiateTeamObjects();
        }

        [RPC]
        void SelectTeam(int spectate, NetworkMessageInfo info)
        {
            IPlayer player = new Player(new NetworkPlayerWrapper(info.sender));
            if (spectate == 0)
            {
                teamManager.AddPlayer(player);
                Debug.Log("Player assigned to " + player.Team.Name);

                GameObject.Find("Player").GetComponent<PlayerInfo>().SendInfo(player);
                player.Team.TeamObject.GetComponent<TeamInfoLoader>().TeamInfo.SetProgress(player.Team.Progress);

                instantiateCubeFinger(player);
            }
            else
            {
                GameObject.Find("Player").GetComponent<PlayerInfo>().SendInfo(player, 0);
                Debug.Log("Player assigned to Spectator");
            }
        }

        void OnPlayerDisconnected(NetworkPlayer networkPlayer)
        {
            IPlayer player = TeamLoader.TeamManager.GetPlayer(new NetworkPlayerWrapper(networkPlayer));

            if (player != null)
            {
                Network.RemoveRPCs(player.CubeFinger.networkView.viewID);
                Network.Destroy(player.CubeFinger.networkView.viewID);

                teamManager.RemovePlayer(player);
            }

            Debug.Log("Player " + networkPlayer + " left.");
        }

        void instantiateTeamObjects()
        {
            GameObject prefab = Resources.Load("Team") as GameObject;
            foreach (ITeam team in teamManager.Teams)
            {
                GameObject teamObject = Network.Instantiate(prefab, Vector3.zero, prefab.transform.rotation, 1) as GameObject;
                teamObject.GetComponent<TeamInfoLoader>().TeamInfo.SetInfo(team.ID, team.Name, team.ImageTarget);
                team.TeamObject = new GameObjectWrapper(teamObject);
            }
        }

        void instantiateCubeFinger(IPlayer player)
        {
            GameObject prefab = Resources.Load("CubeFinger") as GameObject;
            GameObject cubeFinger = Network.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation, 1) as GameObject;

            CubeFinger.CubeFinger finger = cubeFinger.GetComponent<CubeFingerLoader>().Finger as CubeFinger.CubeFinger;
            finger.SetParent(player.Team.ImageTarget);
            finger.SetPlayer(player);
        }
    }
}
*/
