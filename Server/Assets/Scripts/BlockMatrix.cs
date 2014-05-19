using UnityEngine;
using System.Collections;

public class BlockMatrix {

	//defines the maximum amount of cubes that can be placed from the center block
	private static int MAX_RESOLUTION = 101;
	private static int MAX_DIRECTION = (int)Mathf.Floor(MAX_RESOLUTION/2);
	//this is the array construction to remember where we left our squares
	GameObject[,,] blockLocations;
	
	public BlockMatrix(){
		blockLocations = new GameObject[MAX_RESOLUTION,MAX_RESOLUTION,MAX_RESOLUTION];
	}

	//gets a block from the structure
	public GameObject getBlock(int x, int y, int z){
		return blockLocations[x+MAX_DIRECTION,y+MAX_DIRECTION,z+MAX_DIRECTION];
	}
	public GameObject getBlock(Vector3 v){
		return getBlock ((int)v.x, (int)v.y, (int)v.z);
	}


	
	//adds a block to the structure (BUT DOES NOT PLACE IT IN VIEW)
	public void storeBlock(int x, int y, int z, GameObject block){
		blockLocations[x+MAX_DIRECTION,y+MAX_DIRECTION,z+MAX_DIRECTION] = block;
	}
	public void storeBlock(Vector3 v, GameObject block){
		storeBlock ((int)v.x, (int)v.y, (int)v.z, block);
	}

}
