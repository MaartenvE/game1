using UnityEngine;
using BuildingBlocks.Team;
using BuildingBlocks.CubeFinger;
using BuildingBlocks.GUI;

namespace BuildingBlocks.Player
{
    public delegate void TeamChangeHandler(ITeam team);

    public class Player : BuildingBlocksBehaviour, IPlayer
    {
        public event TeamChangeHandler OnTeamChange;

        public static IPlayer LocalPlayer { get; private set; }

        public ITeam Team { get; private set; }
        public ICubeFinger CubeFinger { get; set; }

        private IGameObject block;

        public Player(IGameObject gameObject) : base(gameObject)
        {
            LocalPlayer = this;
            block = gameObject.Find("RotatingBlock");
        }

        public void SetTeam(int teamId)
        {
            Team = BuildingBlocks.Team.Team.GetTeam(teamId);
            if (OnTeamChange != null)
            {
                OnTeamChange(Team);
            }
        }

        public void SetBlockType(bool isFullBlock, Color color)
        {
            setupBlock(color);
            startAnimation();
        }

        private void setupBlock(Color color)
        {
            block.renderer.material.color = color;
        }

        private void startAnimation()
        {
            BlockAnimationBehaviour animation = block.AddComponent<BlockAnimationBehaviour>();
            animation.SetUpAnimation(new Vector3(0, 0.5f, 2), gameObject.Find("BlockPosition").transform.localPosition);
        }
    }
}
