﻿using UnityEngine;
using System.Collections;

public class MatrixLocation : MonoBehaviour {
	
	public Vector3 index = new Vector3(0,0,0);
		
	[RPC]
	void UpdateLocation(Vector3 newLocation){
		index = newLocation;
	}
}
