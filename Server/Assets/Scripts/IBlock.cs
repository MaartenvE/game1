using UnityEngine;
using System.Collections;


public interface IBlock {


	//bool GetIsHalfBlock () ;
	//bool GetIsWholeBlock () ;
	Vector3 GetColor () ;
<<<<<<< HEAD
    void SetColor (Vector3 color) ; 
=======
    Vector3 SetColor (Vector3 color) ; 
>>>>>>> 505cab0c1b03405b9bb5caa0a05db20c3762f808

	//mixes this blocks color with either the block color or the real color.
	void MixWithBlock (Block block) ;
	void MixWithColor (Vector3 color) ;

}
