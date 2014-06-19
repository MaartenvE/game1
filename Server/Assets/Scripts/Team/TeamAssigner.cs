using System;
using System.Linq;
using System.Collections.Generic;

namespace BuildingBlocks.Team
{
    public class TeamAssigner : ITeamAssigner
    {
        public int NumberOfTeams { get; private set; }

        private ITeam[] teams;
        public IEnumerable<ITeam> Teams
        {
            get
            {
                return teams;
            }
        }

        public TeamAssigner(ITeam[] teams)
        {
            if (teams.Count() == 0)
            {
                throw new ArgumentException("No teams provided");
            }

            if (teams.Contains(null))
            {
                throw new ArgumentException("Null provided as team object");
            }

            this.NumberOfTeams = teams.Count();
            this.teams = teams;
        }

        public void AddPlayer(IPlayer player)
        {
            int minSize = teams.Min(t => t.Size);
            IEnumerable<ITeam> minTeams = teams.Where(t => t.Size == minSize);
            ITeam minTeam = minTeams.Aggregate((min, next) => min.Progress < next.Progress ? min : next);
            minTeam.AddPlayer(player);
        }

        public void RemovePlayer(IPlayer player)
        {
            ITeam team = player.Team;
            if (team != null && teams.Contains(team))
            {
                team.RemovePlayer(player);
            }
        }

        public IPlayer GetPlayer(INetworkPlayer networkPlayer)
        {
            foreach (ITeam team in teams)
            {
                foreach (IPlayer player in team.Players)
                {
                    if (player.NetworkPlayer.Equals(networkPlayer))
                    {
                        return player;
                    }
                }
            }
            return null;
        }

        public ITeam GetTeam(int teamId)
        {
            foreach (ITeam team in teams)
            {
                if (team.TeamId == teamId)
                {
                    return team;
                }
            }
            return null;
        }
    }
}
