using UnityEngine;
using System.Collections;

public class Location : MonoBehaviour {

	public Vector3 index = new Vector3(0,0,0);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[RPC]
	void UpdateLocation(Vector3 newLocation){
		index = newLocation;
	}
}
