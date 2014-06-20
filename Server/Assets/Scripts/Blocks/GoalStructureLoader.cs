using UnityEngine;

namespace BuildingBlocks.Blocks
{
    public class GoalStructureLoader : MonoBehaviour
    {
        public static GoalStructure GoalStructure { get; private set; }

        void OnServerInitialized()
        {
            GoalStructure = new GoalStructure(new GameObjectWrapper(gameObject));
        }
    }
}
