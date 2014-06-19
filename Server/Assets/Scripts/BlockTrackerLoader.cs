/*using UnityEngine;

public class BlockTrackerLoader : MonoBehaviour
{
    public BlockTracker Tracker { get; private set; }

    public void Start()
    {
        Tracker = new BlockTracker(new GameObjectWrapper(gameObject), GoalStructureLoader.GoalStructure.Structure);
        Tracker.OnProgressChange +=
                (float progress) => Tracker.TeamInfo.SetProgress(progress);
        Tracker.OnStructureComplete +=
            () => ServerLoader.Server.Win(Tracker.TeamInfo.ID);
    }
}
*/