using UnityEngine;
using System.Collections;

//goal of this class is for it to be able to build a block locally from the data contained in here
public class Block {

	GameObject cube;
	Vector3 location;

	public Block(GameObject cube, Vector3 location){
		this.cube = cube;
		this.location = location;
	}

}
