using UnityEngine;

public class TeamBlockTracker
{
    public TeamBlockTracker(GameObject parent, GameObject prefab) { }

    [RPC]
    void PlaceBlock(Vector3 location, Vector3 color) { }
}
