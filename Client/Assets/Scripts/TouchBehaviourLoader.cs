using UnityEngine;
using System.Collections;

public class TouchBehaviourLoader : MonoBehaviour {

    public GameObject cubeFinger;

    public void Start()
    {
        INetworkView networkView = new NetworkViewWrapper(gameObject.networkView);
        TouchBehaviour touchBehaviour = this.gameObject.AddComponent<TouchBehaviour>();
        touchBehaviour.cubeFinger = cubeFinger;
        touchBehaviour.networkView = networkView;
    }
	
}
