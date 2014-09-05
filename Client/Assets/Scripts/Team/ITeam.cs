using System.Collections.Generic;
using BuildingBlocks.Player;

namespace BuildingBlocks.Team
{
    public interface ITeam : IBuildingBlocksBehaviour
    {
        int TeamId { get; }
        string Name { get; }
        string Target { get; }
        float Progress { get; }
        bool IsMine { get; }

        void RPC_SetTeamInfo(int teamId, string name, string target);
        void RPC_SetTeamProgress(float progress);
    }
}
