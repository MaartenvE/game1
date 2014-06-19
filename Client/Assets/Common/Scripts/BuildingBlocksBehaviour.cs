
namespace BuildingBlocks
{
    public abstract class BuildingBlocksBehaviour : IBuildingBlocksBehaviour
    {
        public IGameObject gameObject { get; private set; }
        public INetworkView networkView { get; private set; }
        public INetwork network { get; private set; }

        protected BuildingBlocksBehaviour(IGameObject gameObject)
        {
            this.gameObject = gameObject;
            this.network = new NetworkWrapper();

            if (gameObject != null)
            {
                this.networkView = gameObject.networkView;
            }
        }
    }
}
