using UnityEngine;
using System.Collections;
using BuildingBlocks;

public class GoalStructureLoader : MonoBehaviour
{
    public static GoalStructure GoalStructure{ get; private set; }

    void OnServerInitialized()
    {
        GoalStructure = new GoalStructure(new GameObjectWrapper(gameObject));
    }
}
