using BuildingBlocks.Player;

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
        
        IStructureTracker StructureTracker { get; }
    }
}
