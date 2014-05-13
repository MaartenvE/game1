using UnityEngine;
using System.Collections;

public class PrefabInstantiate : MonoBehaviour {
	void OnNetworkInstantiate(NetworkMessageInfo info) {
		Debug.Log("New object instantiated by " + info.sender);
	}
	
	// Use this for initialization
	void Start () {
		this.transform.parent = GameObject.Find ("ImageTarget").transform as Transform;
<<<<<<< HEAD
		Vector3 test = new Vector3 (10, 10, 10);
		this.transform.localScale = test; //GameObject.Find ("Cube").transform.localScale;
=======
		this.transform.localScale = GameObject.Find ("Cube").transform.lossyScale;
>>>>>>> 9532a6ce5a78457125fedd000241121bcda3f848
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
