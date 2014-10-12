using UnityEngine;
using BuildingBlocks.Team;
using BuildingBlocks.CubeFinger;
using BuildingBlocks.Blocks;

namespace BuildingBlocks.Player
{
    public class Player : BuildingBlocksBehaviour, IPlayer
    {
        public ITeam Team { get; set; }
        public INetworkPlayer NetworkPlayer { get; private set; }
        public ICubeFinger CubeFinger { get; private set; }
        public Block Block { get; private set; }

        public Player(INetworkPlayer networkPlayer) : base(new GameObjectWrapper(GameObject.Find("Player")))
        {
            NetworkPlayer = networkPlayer;
        }

        public void SetBlock(Block block)
        {
            this.Block = block;
            Color color = Block.Color;
            networkView.RPC("SetBlockType", NetworkPlayer, 1, ColorModel.ConvertToVector3(color));
            CubeFinger.Renderer.SetColor(color);
        }
        
        public void GiveBlock()
        {
            SetBlock(new Block());
        }

        public bool CombineBlock(IPlayer other)
        {
            if (Block.Mix(other.Block))
            {
                this.SetBlock(Block);
                other.SetBlock(Block);
                return true;
            }
            return false;
        }

        public void InstantiateCubeFinger()
        {
            GameObject prefab = Resources.Load("CubeFinger") as GameObject;
            GameObject cubeFinger = Network.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation, 1) as GameObject;

            CubeFinger = cubeFinger.GetComponent<CubeFingerLoader>().Finger;
            CubeFinger.CubeFinger finger = CubeFinger as CubeFinger.CubeFinger;
            finger.SetParent(Team.Target);
            finger.SetPlayer(NetworkPlayer);

            GiveBlock();
            CubeFinger.Renderer.SetColor(Block.Color);
        }

        public static IPlayer GetPlayer(INetworkPlayer player)
        {
            return TeamCreatorLoader.Creator.Assigner.GetPlayer(player);
        }
    }
}
