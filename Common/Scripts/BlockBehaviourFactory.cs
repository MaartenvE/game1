using UnityEngine;
using System.Collections;

public class BlockBehaviourFactory : MonoBehaviour
{
    public string BlockBehaviourType = "BlockBehaviour";

    void Awake()
    {
        gameObject.AddComponent(BlockBehaviourType);
    }
}
