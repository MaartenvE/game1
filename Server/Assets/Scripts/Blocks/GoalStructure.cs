using UnityEngine;

namespace BuildingBlocks.Blocks
{
    public class GoalStructure : BuildingBlocksBehaviour
    {
        public static Structure<Color?> Structure { get; private set; }

        public static void Awake()
        {
            Structure = new Structure<Color?>(StructureReader.loadRandomLevelFromMaps());
        }

        public GoalStructure(IGameObject gameObject)
            : base(gameObject)
        {
            instantiateGoalStructure();
        }

        private void instantiateGoalStructure()
        {
            GameObject prefab = Resources.Load("GoalCube") as GameObject;
            foreach (Vector3 position in Structure.Keys)
            {
                Vector3 location = Structure.Denormalize(position, prefab.transform.localScale.x);
                location.y += gameObject.transform.localPosition.y;
                GameObject blockObject = network.Instantiate(prefab, location, prefab.transform.rotation, 1) as GameObject;
                blockObject.GetComponent<GoalCubeBehaviour>().SetInfo("GoalStructure", Structure[position].GetValueOrDefault());
            }
        }
    }
}
