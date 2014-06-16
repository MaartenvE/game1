/*using UnityEngine;
using BuildingBlocks;

public class TeamBlockTracker : BuildingBlocksBehaviour, ITrackableEventHandler
{
    public TeamBlockTracker(IGameObject parent, GameObject prefab) : base(parent)
    {
        ImageTargetBehaviour[] targets = gameObject.GetComponentsInChildren<ImageTargetBehaviour>();
        foreach (ImageTargetBehaviour target in targets)
        {
            Debug.Log(target.name);
        }

        //ImageTargetBehaviour behaviour;
        //behaviour.RegisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status oldStatus, TrackableBehaviour.Status newStatus)
    {
        Debug.Log("Status changed from " + oldStatus + " to " + newStatus);
    }

    [RPC]
    void PlaceBlock(Vector3 location, Vector3 color) { }
}
*/