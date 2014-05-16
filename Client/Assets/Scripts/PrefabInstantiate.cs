using UnityEngine;
using System.Collections;

public class PrefabInstantiate : MonoBehaviour {
	void OnNetworkInstantiate(NetworkMessageInfo info) {
		Debug.Log("New object instantiated by " + info.sender);
	}
	
	// Use this for initialization
	void Start () {
		this.transform.parent = GameObject.Find ("ImageTarget").transform as Transform;
		Vector3 test = new Vector3 (5, 5, 5);
		this.transform.localScale = test; //GameObject.Find ("Cube").transform.localScale;
		this.renderer.material.color = new Color ((float)(Random.Range (0, 1000) / 1000.0),
		                                         (float)(Random.Range (0, 1000) / 1000.0),
		                                         (float)(Random.Range (0, 1000) / 1000.0));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
