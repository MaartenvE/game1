using UnityEngine;
using BuildingBlocks.Player;

namespace BuildingBlocks.Team
{
    public class TeamCreator : BuildingBlocksBehaviour
    {
        public ITeamAssigner Assigner { get; private set; }

        private ITeamInstantiator instantiator;

        public TeamCreator(IGameObject gameObject, ITeamInstantiator teamInstantiator) : base(gameObject)
        {
            this.instantiator = teamInstantiator;

            ITeam team1 = instantiateTeam("Team 1", "ImageTarget1");
            ITeam team2 = instantiateTeam("Team 2", "ImageTarget2");

            Assigner = new TeamAssigner(new[] { team1, team2 });
        }

        private ITeam instantiateTeam(string teamName, string imageTarget)
        {
            ITeam team = instantiator.InstantiateTeam();
            team.Name = teamName;
            team.Target = imageTarget;
            team.SendInfo();
            return team;
        }

        public void InstantiateGroundBlocks()
        {
            foreach (ITeam team in Assigner.Teams)
            {
                team.StructureTracker.PlaceGroundBlock();
            }
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
