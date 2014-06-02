using UnityEngine;
using System.Collections;


public interface IBlock {


	//bool GetIsHalfBlock () ;
	//bool GetIsWholeBlock () ;
	Vector3 GetColor () ;
    Vector3 SetColor (Vector3 color) ; 

	//mixes this blocks color with either the block color or the real color.
	void MixWithBlock (Block block) ;
	void MixWithColor (Vector3 color) ;

}
