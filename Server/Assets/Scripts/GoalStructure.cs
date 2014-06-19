using UnityEngine;
using System.Collections;
using BuildingBlocks;

public class GoalStructure : BuildingBlocksBehaviour
{
    private static Structure<Color?> structure;
    public static Structure<Color?> Structure
    { 
        get
        {
            if (structure == null)
            {
                structure = new Structure<Color?>(StructureReader.loadRandomLevelFromMaps());
            }
            return structure;
        }
    }

    public GoalStructure(IGameObject gameObject) : base(gameObject)
    {
        instantiateGoalStructure();
    }

    private void instantiateGoalStructure()
    {
        GameObject prefab = Resources.Load("GoalCube") as GameObject;
        foreach (Vector3 position in Structure.Keys)
        {
            Vector3 location = Structure.Denormalize(position, prefab.transform.localScale.x);
            GameObject blockObject = network.Instantiate(prefab, location, prefab.transform.rotation, 1) as GameObject;
            blockObject.GetComponent<GoalCubeBehaviour>().SetInfo("GoalStructure", Structure[position].GetValueOrDefault());
        }
    }
}
