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
		Color[] colorArray = new Color[3];
		colorArray [0] = Color.red;
		colorArray [1] = Color.green;
		colorArray [2] = Color.blue;
		this.renderer.material.color = colorArray [Random.Range (0, 2)];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
