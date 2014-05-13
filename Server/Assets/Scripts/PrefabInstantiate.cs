using UnityEngine;
using System.Collections;

public class PrefabInstantiate : MonoBehaviour {
	void OnNetworkInstantiate(NetworkMessageInfo info) {
		Debug.Log("New object instantiated by " + info.sender);
	}
	
	// Use this for initialization
	void Start () {
		this.transform.parent = GameObject.Find ("ImageTarget").transform as Transform;
		Vector3 test = new Vector3 (10, 10, 10);
		this.transform.localScale = test; //GameObject.Find ("Cube").transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
