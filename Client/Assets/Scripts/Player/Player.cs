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
        public bool HasFullBlock { get; private set; }

        private IGameObject fullBlock;
        private IGameObject halfBlock;

        public Player(IGameObject gameObject) : base(gameObject)
        {
            LocalPlayer = this;
            fullBlock = gameObject.Find("RotatingBlock");
            halfBlock = gameObject.Find("RotatingHalfBlock");
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
            HasFullBlock = isFullBlock;
            setupBlock(color);
            startAnimation();
        }

        private void setupBlock(Color color)
        {
            halfBlock.renderer.enabled = !HasFullBlock;
            fullBlock.renderer.enabled = HasFullBlock;
            halfBlock.renderer.material.color = color;
            fullBlock.renderer.material.color = color;
        }

        private void startAnimation()
        {
            IGameObject block = HasFullBlock ? fullBlock : halfBlock;
            BlockAnimationBehaviour animation = block.AddComponent<BlockAnimationBehaviour>();
            animation.SetUpAnimation(new Vector3(0, 0.5f, 2), gameObject.Find("BlockPosition").transform.localPosition);
        }
    }
}
