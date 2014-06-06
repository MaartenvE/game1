using UnityEngine;
using System.Collections;

public class PrefabInstantiate : MonoBehaviour {
	void OnNetworkInstantiate(NetworkMessageInfo info) {
		Debug.Log("New object instantiated by " + info.sender);
	}
	
	// Use this for initialization
	void Start () {
		this.transform.parent = GameObject.Find ("ImageTarget1").transform as Transform;
        this.tag = "block";
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 location = this.transform.position;
        this.transform.position = new Vector3(Mathf.Round(location.x), Mathf.Round(location.y), Mathf.Round(location.z));	
	}
}
