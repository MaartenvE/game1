using UnityEngine;
using System.Collections;

public interface IInstantiatedBlock : IBlock {

	//InstantiatedBlock(NetworkViewID networkViewID) : base() ;
	
	void SetLocation (Vector3 location) ;
	
	Vector3 GetLocation() ;

	GameObject getBlock() ;

	void setBlock(GameObject block);
	
	NetworkViewID GetNetworkViewID() ;

}
