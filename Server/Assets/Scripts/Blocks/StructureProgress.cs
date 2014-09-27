using UnityEngine;

namespace BuildingBlocks.Blocks
{
    class StructureProgress
    {
        public readonly Structure<Color?> GoalStructure;
        private int totalBlockCount;
        private int correctBlockCount = 0;
        private int wrongBlockCount = 0;

        public float Progress
        {
            get
            {
                return Mathf.Max(0.0f, RawProgress);
            }
        }

        public float RawProgress
        {
            get
            {
                return (correctBlockCount - (wrongBlockCount / 2f)) / totalBlockCount;
            }
        }

        public StructureProgress(Structure<Color?> goalStructure, int blockCount)
        {
            this.GoalStructure = goalStructure;
            this.totalBlockCount = blockCount;
        }

        public bool CheckBlock(Vector3 position, Color? previousColor, Color? newColor)
        {
            bool wasCorrect = GoalStructure[position] == previousColor;
            bool isCorrect = GoalStructure[position] == newColor;

            if (newColor == null)
            {
                if (!isCorrect && wasCorrect) correctBlockCount--;
                else wrongBlockCount--;
            }
            else
            {
                if (isCorrect) correctBlockCount++;
                else wrongBlockCount++;
            }

            return isCorrect;
        }
    }
}
