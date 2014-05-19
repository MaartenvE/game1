using UnityEngine;
using System.Collections;

//goal of this class is for it to be able to build a block locally from the data contained in here
public class Block : MonoBehaviour {

	GameObject cube;
	Vector3 location;

	public Block(GameObject cube, Vector3 location){
		this.cube = cube;
		this.location = location;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
