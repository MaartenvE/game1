using UnityEngine;
using System.Collections;
using AssemblyCSharp;

/**
 * The server class handles instantiating the server and all client server interaction
 */
public class Server : MonoBehaviour{
	
	private GameObject _prefab;
	private int _port = 3825;
	private INetwork _network;
		
	public GameObject prefab{
		set { _prefab = value; }
	}

	public int port{
		set { _port = value; }
	}
	public INetwork network{
		set { _network = value; }
	}


	/// <summary>
	/// Launches the server.
	/// </summary>
	public void LaunchServer() {
		_network.InitializeServer(32, _port, false);
	}

	/// <summary>
	/// Prints the text to console.
	/// </summary>
	/// <param name="text">Text.</param> The text passed as string
	[RPC] 
	void PrintText (string text) {
		Debug.Log(text);
	}
	
	/// <summary>
	/// Places the block as requested by the client.
	/// </summary>
	/// <returns>The blockerror.</returns>
	/// <param name="location">Location.</param>
	[RPC]
	public BlockError PlaceBlock(Vector3 location){
		GameObject prefab = Resources.Load ("TestCube") as GameObject;
		_network.Instantiate (prefab, location, prefab.transform.rotation, 1);
		return null;
	}

	//stubs
	[RPC]
	BlockError RemoveBlock(Vector3 location){	
		return null;
	}
	
	[RPC]
	Block RequestBlock(){
		return null;
	}
	
	[RPC]
	void ThrowAwayBlock(){
	}
	
	[RPC] 
	//Decide: magnetic or true heading. Is compas heading already contained in the attitude from gyro?
	Block Tap(double networkTime, float compasHeading, Quaternion attitude){
		return null;
	}

	/// <summary>
	/// Event handler, prints a console message when a player connects.
	/// </summary>
	/// <param name="player">Player.</param>
	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("Player connected from " + player.ipAddress);
	}

	/// <summary>
	/// Event handler, prints a console message when the server has been intialized
	/// </summary>
	void OnServerInitialized() {
		Debug.Log("Server initialized and ready");
	}
}