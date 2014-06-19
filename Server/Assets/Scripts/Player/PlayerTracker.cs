using System.Collections.Generic;
using BuildingBlocks.Team;

namespace BuildingBlocks.Player
{
    public class PlayerTracker : IPlayerTracker
    {
        private ITeam team;
        private LinkedList<IPlayer> players;

        public int Size
        {
            get
            {
                return players.Count;
            }
        }

        public IEnumerable<IPlayer> Players
        {
            get
            {
                return players;
            }
        }

        public PlayerTracker(ITeam team)
        {
            this.team = team;
            this.players = new LinkedList<IPlayer>();
        }

        public void AddPlayer(IPlayer player)
        {
            player.Team = team;
            players.AddLast(player);
        }

        public void RemovePlayer(IPlayer player)
        {
            player.Team = null;
            players.Remove(player);
        }
    }
}
