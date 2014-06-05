using UnityEngine;
using System.Collections;
using AssemblyCSharp;

/**
 * The server class handles instantiating the server and all client server interaction
 */
public class Server : MonoBehaviour{

    private int _port = 3825;
	private INetwork _network;
	private INetworkView _networkView;

	private ArrayList players = new ArrayList ();

	public BlockMatrix blockMatrix = new BlockMatrix();
	public int port{
		set { _port = value; }
	}
	public INetwork network{
		set { _network = value; }
	}

	public new INetworkView networkView{
		set { _networkView = value; }
		get { return _networkView; }
	}

    public void Start()
    {
		GameObject prefab = Resources.Load("TestCube") as GameObject;
		GameObject block = _network.Instantiate(prefab, new Vector3(0,0,0), prefab.transform.rotation, 1) as GameObject;
		networkView.RPC("ColorBlock", RPCMode.AllBuffered, block.networkView.viewID, randomColor());
    }

	/// <summary>
	/// Launches the server.
	/// </summary>
	public void LaunchServer() {
		_network.InitializeServer(32, _port, false);
	}
	
	/// <summary>
	/// Places the block as requested by the client.
	/// </summary>
	/// <returns>The blockerror.</returns>
	/// <param name="location">Location.</param>
	[RPC]
	public void PlaceBlock(Vector3 location, Vector3 matrixLocation, NetworkViewID NVI){
		PlaceColoredBlock (location, matrixLocation, NVI, randomColor());
	}

	[RPC]
	public void PlaceColoredBlock(Vector3 location, Vector3 matrixLocation, NetworkViewID NVI, Vector3 color){
		GameObject prefab = Resources.Load ("TestCube") as GameObject;
		
		location = roundLocation (location);
		
		GameObject block = _network.Instantiate (prefab, location, prefab.transform.rotation, 1) as GameObject;
		
		_networkView.RPC("ColorBlock", RPCMode.AllBuffered, block.networkView.viewID, color);
		
		GameObject sideBlock = _networkView.Find (NVI).gameObject();
		
		block.GetComponent<Location> ().index = sideBlock.GetComponent<Location> ().index + matrixLocation;
		
		Debug.Log (block.GetComponent<Location> ().index);
	}



	[RPC]
	public void ColorBlock(NetworkViewID NVI, Vector3 color){
            GameObject block = _networkView.Find(NVI).gameObject();
            block.renderer.material.color = new Color(color.x, color.y, color.z);   
    }


	[RPC]
	public void RemoveBlock(NetworkViewID NVI){
		GameObject cube = _networkView.Find (NVI).gameObject();

		Debug.Log ("tried to remove id "+NVI.ToString());

		//check if the cube is not to be removed (as it is a raytrace immune block)
		if (cube.layer != 2) {
			Debug.Log ("removed id "+NVI.ToString());
						Network.RemoveRPCs (NVI);
						Network.Destroy (NVI);
				}
	}

	[RPC]
	public void MoveServerFinger(NetworkViewID networkViewID, Vector3 coords, Vector3 location, int visible){

		GameObject finger = _networkView.Find (networkViewID).gameObject();
		finger.transform.position = coords;
		finger.GetComponent<Location> ().index = location;

		if (visible == 1) {
			finger.renderer.enabled = true;
		} else {
			finger.renderer.enabled = false;
		}
		_networkView.RPC ("MoveClientFinger", RPCMode.OthersBuffered, networkViewID, coords, location, visible);
	}

	[RPC]
	public void MoveClientFinger(NetworkViewID networkViewID, Vector3 coords, Vector3 location, int visible){

	}

	[RPC]
	public void InstantiateFinger(NetworkViewID networkViewID, Vector3 color){

	}
	[RPC]
	public void InstantiatePersonalFinger(NetworkViewID networkViewID){
		
	}
	//networkView.RPC ("MoveFinger", RPCMode.Server, cubeFinger.networkView.viewID, pickedObject.TransformPoint (displacement), pickedObject.GetComponent<Location> ().index+displacement);

	//stubs
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
	/// Rounds the location to the nearest int
	/// </summary>
	/// <returns>The roundedlocation.</returns>
	/// <param name="location">Location.</param>
	private Vector3 roundLocation(Vector3 location){
		return new Vector3(Mathf.Round(location.x), Mathf.Round(location.y), Mathf.Round(location.z));
	}

	/// <summary>
	/// Generates a random colour as Vector3 to enable using it in RPC call.
	/// </summary>
	/// <returns>The color (all values between 0 and 1).</returns>
	private Vector3 randomColor(){
		return new Vector3 ((float)(Random.Range (0, 1000) / 1000.0), 
		                    (float)(Random.Range (0, 1000) / 1000.0), 
		                    (float)(Random.Range (0, 1000) / 1000.0));
	}

	/// <summary>
	/// Event handler, prints a console message when a player connects.
	/// </summary>
	/// <param name="player">Player.</param>
	void OnPlayerConnected(NetworkPlayer player)
    {
		Player newPlayer = new Player(_networkView, _network, new NetworkPlayerWrapper(player));
		players.Add(newPlayer);

		Debug.Log("Player connected from " + player.ipAddress);
	}

	/// <summary>
	/// Event handler, prints a console message when the server has been intialized
	/// </summary>
	void OnServerInitialized() {
		Debug.Log("Server initialized and ready");
	}
}