using BuildingBlocks.Player;
using BuildingBlocks.Blocks;

namespace BuildingBlocks.Team
{
    public interface ITeam : IBuildingBlocksBehaviour, IPlayerTracker
    {
        int TeamId { get; }
        string Name { get; set; }
        string Target { get; set; }
        float Progress { get; }

        void SendInfo();
        void SendProgress(INetworkPlayer player);

        void RPC_SetTeamInfo(int teamId, string name, string target);
        void RPC_SetTeamProgress(float progress);
        
        IStructureTracker StructureTracker { get; }
    }
}
