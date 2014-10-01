using UnityEngine;
using System.Linq;
using BuildingBlocks.Team;

namespace BuildingBlocks.Blocks
{
    public class StructureTracker : IStructureTracker
    {
        public event StructureCompleteHandler OnCompletion;
        public event StructureProgressHandler OnProgressChange;

        private IBlockConstructor constructor;

        private Structure<Color?> goal;
        private Structure<Color?> current;
        private StructureProgress[] progress;

        private int totalBlockCount;        

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
                return progress.Max(p => p.RawProgress);
            }
        }

        public StructureTracker(ITeam team, Structure<Color?> goalStructure)
        {
            if (goalStructure != null)
            {
                this.goal = goalStructure;
                this.current = new Structure<Color?>(goalStructure.GetLength(0), goalStructure.GetLength(1), goalStructure.GetLength(2));

                int blockCount = getBlockCount(goalStructure);
                progress = new StructureProgress[4];
                for (int i = 0; i < 4; i++)
                {
                    progress[i] = new StructureProgress(goalStructure, blockCount);
                    goalStructure = goalStructure.Rotate();
                }

                this.constructor = new BlockConstructor(team);
            }
        }

        public void PlaceGroundBlock()
        {
            Vector3 center = goal.Normalize(Vector3.zero, constructor.Scale);
            Color color = goal[center].GetValueOrDefault();
            checkBlock(center, color);
            constructor.PlaceGroundBlock(color);
        }

        public bool PlaceBlock(Vector3 location, Color color)
        {
            Vector3 position = goal.Normalize(location, constructor.Scale);
            if (current[position] == null)
            {
                checkBlock(position, color);
                constructor.PlaceBlock(location, color);
                return true;
            }
            return false;
        }

        public void RemoveBlock(IGameObject block)
        {
            checkBlock(goal.Normalize(block.transform.localPosition, constructor.Scale), null);
            constructor.RemoveBlock(block);
        }

        private int getBlockCount(Structure<Color?> goal)
        {
            int count = 0;
            foreach (Color? color in goal)
            {
                if (color != null)
                {
                    count++;
                }
            }
            return count;
        }

        private void checkBlock(Vector3 position, Color? color)
        {
            foreach (StructureProgress p in progress)
            {
                p.CheckBlock(position, current[position], color);
            }

            current[position] = color;
            invokeHandlers();
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
