using UnityEngine;
using System.Collections;

//goal of this class is for it to be able to build a block locally from the data contained in here
public class Block : IBlock{

	//GameObject cube;
	//Vector3 location;

	private Vector3 Color = new Vector3(0,0,0);

	public Block(){
		//this.cube = cube;
		//this.location = location;
	}
	
	
	public Vector3 GetColor () {
		return Color;
	}
	public void SetColor(Vector3 color) {
		Color = color;
	}

	//mixes this blocks color with either the block color or the real color. (returns average for each band)
	public void MixWithBlock (Block block) {
		MixWithColor (block.GetColor ());
	}
	public void MixWithColor (Vector3 otherColor) {
		SetColor ( new Vector3 ((otherColor.x+Color.x)/2,(otherColor.y+Color.y)/2,(otherColor.z+Color.z)/2));
	}


}
