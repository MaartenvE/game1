using UnityEngine;
using BuildingBlocks.Team;
using BuildingBlocks.CubeFinger;

namespace BuildingBlocks.Player
{
    public class Player : BuildingBlocksBehaviour, IPlayer
    {
        private const float FULL_BLOCK_CHANCE = 0.4f;

        public ITeam Team { get; set; }
        public INetworkPlayer NetworkPlayer { get; private set; }
        public ICubeFinger CubeFinger { get; private set; }
        public HalfBlock HalfBlock { get; private set; }
        public bool HasPlaceableBlock { get; private set; }

        public Player(INetworkPlayer networkPlayer) : base(new GameObjectWrapper(GameObject.Find("Player")))
        {
            NetworkPlayer = networkPlayer;
        }

        public void GiveNewInventoryBlock()
        {
            HalfBlock = new HalfBlock(SubtractiveHalfBlockColorBehaviour.RandomPrimaryColor());
            Vector3 color = ColorModel.ConvertToVector3(HalfBlock.CalculateUnityColor());
            CubeFinger.Renderer.SetColor(HalfBlock.CalculateUnityColor());

            HasPlaceableBlock = Random.value < FULL_BLOCK_CHANCE;
            networkView.RPC("SetBlockType", NetworkPlayer, HasPlaceableBlock ? 1 : 0, color);
        }

        public void CombineBlock(IPlayer other)
        {
            if (!this.HasPlaceableBlock && !other.HasPlaceableBlock)
            {
                this.HalfBlock.CombineHalfBlock(other.HalfBlock);
                this.HasPlaceableBlock = true;

                Color color = HalfBlock.CalculateUnityColor();
                networkView.RPC("SetBlockType", NetworkPlayer, 1, ColorModel.ConvertToVector3(color));
                CubeFinger.Renderer.SetColor(color);

                other.GiveNewInventoryBlock();
            }
        }

        public void InstantiateCubeFinger()
        {
            GameObject prefab = Resources.Load("CubeFinger") as GameObject;
            GameObject cubeFinger = Network.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation, 1) as GameObject;

            CubeFinger = cubeFinger.GetComponent<CubeFingerLoader>().Finger;
            CubeFinger.CubeFinger finger = CubeFinger as CubeFinger.CubeFinger;
            finger.SetParent(Team.Target);
            finger.SetPlayer(NetworkPlayer);

            GiveNewInventoryBlock();
            CubeFinger.Renderer.SetColor(HalfBlock.CalculateUnityColor());
        }

        public static IPlayer GetPlayer(INetworkPlayer player)
        {
            return TeamCreatorLoader.Creator.Assigner.GetPlayer(player);
        }
    }
}
