using UnityEngine;
using BuildingBlocks.Team;

namespace BuildingBlocks.Blocks
{
    public class StructureTracker : IStructureTracker
    {
        public event StructureCompleteHandler OnCompletion;
        public event StructureProgressHandler OnProgressChange;

        private ITeam team;
        private IBlockConstructor constructor;

        private Structure<Color?> goal;
        private Structure<Color?> current;

        private int totalBlockCount;
        private int correctBlockCount = 0;
        private int wrongBlockCount = 0;

        public float Progress
        {
            get
            {
                return Mathf.Max(0.0f, (correctBlockCount - (wrongBlockCount / 2f)) / totalBlockCount);
            }
        }

        public StructureTracker(ITeam team, Structure<Color?> goalStructure)
        {
            this.team = team;
            this.goal = goalStructure;
            this.current = new Structure<Color?>(goal.GetLength(0), goal.GetLength(1), goal.GetLength(2));
            this.constructor = new BlockConstructor(team);

            initializeCorrectness();
        }

        public void PlaceGroundBlock()
        {
            Vector3 center = goal.Normalize(Vector3.zero, constructor.Scale);
            constructor.PlaceGroundBlock(goal[center].GetValueOrDefault());
        }

        public void PlaceBlock(Vector3 location, Color color)
        {
            checkBlock(goal.Normalize(location, constructor.Scale), color);
            constructor.PlaceBlock(location, color);
        }

        public void RemoveBlock(IGameObject block)
        {
            checkBlock(goal.Normalize(block.transform.localPosition, constructor.Scale), null);
            constructor.RemoveBlock(block);
        }

        private void initializeCorrectness()
        {
            foreach (Color? color in goal)
            {
                if (color != null)
                {
                    this.totalBlockCount++;
                }
            }
        }

        private bool checkBlock(Vector3 position, Color? color)
        {
            bool wasCorrect = goal[position] == current[position];
            bool isCorrect = goal[position] == color;

            if (color == null)
            {
                if (!isCorrect && wasCorrect) correctBlockCount--;
                else wrongBlockCount--;
            }
            else
            {
                if (isCorrect) correctBlockCount++;
                else wrongBlockCount++;
            }

            current[position] = color;
            invokeHandlers();
            return isCorrect;
        }

        private void invokeHandlers()
        {
            if (OnProgressChange != null)
            {
                OnProgressChange(Progress);
            }

            if (Progress == 1 && OnCompletion != null)
            {
                OnCompletion();
            }
        }
    }
}
