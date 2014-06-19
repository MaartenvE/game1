using UnityEngine;
using BuildingBlocks.Player;

namespace BuildingBlocks.Team
{
    public class TeamCreator : BuildingBlocksBehaviour
    {
        public ITeamAssigner Assigner { get; private set; }

        public TeamCreator(IGameObject gameObject) : base(gameObject)
        {
            ITeam team1 = instantiateTeam("Team 1", "ImageTarget1");
            ITeam team2 = instantiateTeam("Team 2", "ImageTarget2");

            Assigner = new TeamAssigner(new[] { team1, team2 });
        }

        public void InstantiateGroundBlocks()
        {
            foreach (ITeam team in Assigner.Teams)
            {
                team.StructureTracker.PlaceGroundBlock();
            }
        }

        private ITeam instantiateTeam(string teamName, string imageTarget)
        {
            GameObject teamPrefab = Resources.Load("Team") as GameObject;
            GameObject teamObject = network.Instantiate(teamPrefab, Vector3.zero, new Quaternion(), 1) as GameObject;
            ITeam team = teamObject.GetComponent<TeamLoader>().Team;
            team.Name = teamName;
            team.Target = imageTarget;
            team.SendInfo();
            return team;
        }

        public void OnPlayerDisconnected(INetworkPlayer networkPlayer)
        {
            IPlayer player = Player.Player.GetPlayer(networkPlayer);

            if (player != null)
            {
                network.RemoveRPCs(player.CubeFinger.networkView.viewID);
                network.Destroy(player.CubeFinger.networkView.viewID);

                Assigner.RemovePlayer(player);
            }
        }

        public void SelectTeam(INetworkPlayer networkPlayer, bool spectate)
        {
            IPlayer player = new Player.Player(networkPlayer);
            PlayerInfo playerInfo = gameObject.Find("Player").GetComponent<PlayerInfo>();
            if (!spectate)
            {
                Assigner.AddPlayer(player);
                playerInfo.SendInfo(player);
                player.Team.SendProgress(networkPlayer);
                player.InstantiateCubeFinger();
            }
            else
            {
                playerInfo.SendInfo(player, 0);
            }
        }
    }
}
