using UnityEngine;
using System.Collections;

public class PrefabInstantiate : MonoBehaviour {
	void OnNetworkInstantiate(NetworkMessageInfo info) {
		Debug.Log("New object instantiated by " + info.sender);
	}
	
	// Use this for initialization
	void Start () {
		this.transform.parent = GameObject.Find ("ImageTarget").transform as Transform;
		this.transform.localScale = GameObject.Find ("Cube").transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
