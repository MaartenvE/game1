
namespace BuildingBlocks
{
    public interface IBuildingBlocksBehaviour
    {
        IGameObject gameObject { get; }
        INetworkView networkView { get; }
    }
}