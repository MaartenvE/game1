using System.Collections.Generic;

namespace BuildingBlocks.Player
{
    public interface IPlayerTracker
    {
        int Size { get; }
        IEnumerable<IPlayer> Players { get; }

        void AddPlayer(IPlayer player);
        void RemovePlayer(IPlayer player);
    }
}
