using UnityEngine;
using BuildingBlocks.CubeFinger;
using BuildingBlocks.Team;

namespace BuildingBlocks.Player
{
    public class Player : IPlayer
    {
        public const float FULL_BLOCK_CHANCE = 0.4f;
        public ITeam Team { get; set; }

        public HalfBlock HalfBlock { get; set; }

        public INetworkPlayer NetworkPlayer { get; private set; }
        public ICubeFinger CubeFinger { get; set; }

        public Player(INetworkPlayer player)
        {
            NetworkPlayer = player;
            _networkView = new NetworkViewWrapper(GameObject.Find("Player").networkView);
        }

        private INetworkView _networkView; //this is the networkview on which the player exists.
        private INetwork _Network;
        private INetworkPlayer _NetworkPlayer; //this is the actual networkPlayer

        private IInstantiatedBlock _Finger;
        public bool HasPlaceableBlock { get; set; }

        public void GiveInventoryBlock()
        {
            if (HalfBlock == null)
            {
                HalfBlock = new HalfBlock(SubtractiveHalfBlockColorBehaviour.RandomPrimaryColor());
                Vector3 color = ColorModel.ConvertToVector3(HalfBlock.CalculateUnityColor());
                CubeFinger.Renderer.SetColor(HalfBlock.CalculateUnityColor());

                if (Random.value < FULL_BLOCK_CHANCE)
                {
                    this.HasPlaceableBlock = true;
                    _networkView.RPC("SetBlockFull", NetworkPlayer);
                }
                else
                {
                    this.HasPlaceableBlock = false;
                    _networkView.RPC("SetBlockHalf", NetworkPlayer);
                }
                _networkView.RPC("SetHalfBlockColor", NetworkPlayer, color);

            }

        }

        public void GiveNewInventoryBlock()
        {
            HalfBlock = null;
            GiveInventoryBlock();
        }

        public void CombineBlock(IPlayer other)
        {
            if (!this.HasPlaceableBlock && !other.HasPlaceableBlock)
            {
                this.HalfBlock.CombineHalfBlock(other.HalfBlock);
                this.HasPlaceableBlock = true;
                Vector3 color = ColorModel.ConvertToVector3(HalfBlock.CalculateUnityColor());
                _networkView.RPC("SetHalfBlockColor", NetworkPlayer, color);
                _networkView.RPC("SetBlockFull", NetworkPlayer);
                CubeFinger.Renderer.SetColor(HalfBlock.CalculateUnityColor());
                other.GiveNewInventoryBlock();
            }
        }


        public void DestroyInventoryBlock()
        {
            HalfBlock = null;
        }

        public void InstantiateCubeFinger()
        {
            GameObject prefab = Resources.Load("CubeFinger") as GameObject;
            GameObject cubeFinger = Network.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation, 1) as GameObject;

            CubeFinger.CubeFinger finger = cubeFinger.GetComponent<CubeFingerLoader>().Finger as CubeFinger.CubeFinger;
            finger.SetParent(Team.Target);
            finger.SetPlayer(this);
        }

        public static IPlayer GetPlayer(INetworkPlayer player)
        {
            return TeamCreatorLoader.Creator.Assigner.GetPlayer(player);
        }

    }
}
