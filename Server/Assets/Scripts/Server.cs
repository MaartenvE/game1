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

	public BlockMatrix blockMatrix = new BlockMatrix();
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
	public void PlaceBlock(Vector3 location, Vector3 matrixLocation, NetworkViewID NVI){
		GameObject prefab = Resources.Load ("TestCube") as GameObject;
		GameObject block = Network.Instantiate (prefab, location, prefab.transform.rotation, 1) as GameObject;

		GameObject sideBlock = NetworkView.Find (NVI).gameObject;

		block.GetComponent<location> ().index = sideBlock.GetComponent<location> ().index + matrixLocation;

		Debug.Log (block.GetComponent<location> ().index);
	}

	//stubs
	[RPC]
	void RemoveBlock(NetworkViewID NVI){	
		Network.Destroy (NVI);
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