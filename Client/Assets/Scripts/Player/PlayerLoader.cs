using UnityEngine;
using BuildingBlocks.Blocks;

namespace BuildingBlocks.Player
{
    public class PlayerLoader : MonoBehaviour
    {
        public static IPlayer Player { get; private set; }

        void Awake()
        {
            Player = new Player(new GameObjectWrapper(gameObject));
        }

        [RPC]
        void SetPlayerInfo(int team)
        {
            Player.SetTeam(team);
        }

        [RPC]
        void SetBlockType(int full, Vector3 color)
        {
            Player.SetBlockType(full != 0, ColorModel.ConvertToUnityColor(color));
        }

        [RPC]
        void ThrowAwayBlock() { }
    }
}
