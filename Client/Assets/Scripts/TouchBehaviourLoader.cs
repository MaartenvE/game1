using UnityEngine;
using System.Collections;

public class TouchBehaviourLoader : MonoBehaviour {

    public GameObject cubeFinger;

    public void Start()
    {
        INetworkView networkView = new NetworkViewWrapper();
        NetworkView nativeNetworkView = this.GetComponent<NetworkView>();

        networkView.SetNativeNetworkView(nativeNetworkView);
        TouchBehaviour touchBehaviour = this.gameObject.AddComponent<TouchBehaviour>();
        touchBehaviour.cubeFinger = cubeFinger;
        touchBehaviour.networkView = networkView;
    }
	
}
