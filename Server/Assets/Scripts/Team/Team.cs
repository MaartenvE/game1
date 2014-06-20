using UnityEngine;
using System.Collections.Generic;
using BuildingBlocks.Player;
using BuildingBlocks.Blocks;
using BuildingBlocks.Server;

namespace BuildingBlocks.Team
{
    public class Team : BuildingBlocksBehaviour, ITeam
    {
        // ID of the next created team.
        private static int nextTeamId = 1;

        public int TeamId { get; private set; }
        public string Name { get; set; }

        public IStructureTracker StructureTracker { get; private set; }
        private IPlayerTracker playerTracker;

        public int Size
        {
            get
            {
                return playerTracker.Size;
            }
        }

        private string target;
        public string Target
        {
            get
            {
                return target;
            }

            set
            {
                target = value;
                gameObject.transform.parent = gameObject.Find(target).transform;
                gameObject.transform.localPosition = Vector3.zero;
            }
        }

        public float Progress
        {
            get
            {
                return StructureTracker.Progress;
            }
        }

        public IEnumerable<IPlayer> Players
        {
            get
            {
                return playerTracker.Players;
            }
        }

        public Team(IGameObject gameObject) : base(gameObject)
        {
            TeamId = nextTeamId++;
            playerTracker = new PlayerTracker(this);
            StructureTracker = new StructureTracker(this, GoalStructure.Structure);

            StructureTracker.OnProgressChange += sendProgress;
            StructureTracker.OnCompletion += () => ServerLoader.Server.Win(TeamId);
        }

        public void AddPlayer(IPlayer player)
        {
            playerTracker.AddPlayer(player);
        }

        public void RemovePlayer(IPlayer player)
        {
            playerTracker.RemovePlayer(player);
        }

        public void SendInfo()
        {
            networkView.RPC("SetTeamInfo", RPCMode.OthersBuffered, TeamId, Name, Target);
        }

        private void sendProgress(float progress)
        {
            networkView.RPC("SetTeamProgress", RPCMode.Others, progress);
        }

        public void SendProgress(INetworkPlayer player)
        {
            networkView.RPC("SetTeamProgress", player, StructureTracker.Progress);
        }

        public void RPC_SetTeamInfo(int teamId, string name, string target) { }
        public void RPC_SetTeamProgress(float progress) { }

        public static ITeam GetTeam(int teamId)
        {
            return TeamCreatorLoader.Creator.Assigner.GetTeam(teamId);
        }

    }
}
