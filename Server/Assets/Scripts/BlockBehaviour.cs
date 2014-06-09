using UnityEngine;

public class BlockBehaviour : GroundBlockBehaviour
{
    [RPC]
    void RemoveBlock()
    {
        Network.RemoveRPCs(networkView.viewID);
        Network.Destroy(networkView.viewID);
    }
}
