using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using BuildingBlocks.Team;

namespace BuildingBlocks.Server
{
    public class Server
    {
        private INetwork network;
        private INetworkView networkView;

        public Server(INetwork network, INetworkView networkView)
        {
            this.network = network;
            this.networkView = networkView;
        }

        public void Launch(int maxPlayers, int port, bool useNAT)
        {
            network.InitializeServer(maxPlayers, port, useNAT);
        }

        public void Win(int team)
        {
            networkView.RPC("Win", RPCMode.OthersBuffered, team);
            network.SetSendingEnabled(1, false);
            network.isMessageQueueRunning = false;
            Application.LoadLevel(0);
        }

        public void TimeUp()
        {
            IEnumerable<ITeam> teams = TeamCreatorLoader.Creator.Assigner.Teams;
            float maxProgress = teams.Max(t => t.RawProgress);
            IEnumerable<ITeam> maxTeams = teams.Where(t => Mathf.Approximately(t.RawProgress, maxProgress));
            if (maxTeams.Count() == teams.Count())
            {
                Win(-1);
            }
            else
            {
                Win(maxTeams.First().TeamId);
            }
        }
    }
}
