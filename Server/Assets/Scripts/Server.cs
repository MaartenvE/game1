using UnityEngine;
using System;

/**
 * The server class handles instantiating the server and all client server interaction
 */
public class Server : MonoBehaviour{

    private int _port = 3825;
	private INetwork _network;
	private INetworkView _networkView;

    BumpMatcher bumpMatcher = new BumpMatcher();
	
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
        bumpMatcher.OnBumpMatch += (Bump bump1, Bump bump2) => Debug.Log ("Detected bump between players " + bump1.Sender + " and " + bump2.Sender);
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
	/*[RPC]
	public void PlaceBlock(Vector3 location, Vector3 matrixLocation, NetworkViewID NVI, NetworkMessageInfo info)
    {
        IPlayer player = TeamLoader.TeamManager.GetPlayer(new NetworkPlayerWrapper(info.sender));

		GameObject prefab = Resources.Load ("TestCube") as GameObject;

		location = roundLocation (location);

		GameObject block = _network.Instantiate(prefab, location, prefab.transform.rotation, 1) as GameObject;
        block.GetComponent<BlockBehaviour>().SetInfo(randomTarget(), randomColor());
        
		GameObject sideBlock = _networkView.Find (NVI).gameObject();
		
		block.GetComponent<MatrixLocation> ().index = sideBlock.GetComponent<MatrixLocation> ().index + matrixLocation;

		Debug.Log (block.GetComponent<MatrixLocation> ().index);
	}*/

    string randomTarget()
    {
        int rand = UnityEngine.Random.Range(0, 2);
        string[] targets = { "ImageTarget1", "ImageTarget2" };
        return targets[rand];
    }

	//stubs
	[RPC]
	Block RequestBlock(){
		return null;
	}
	
	[RPC]
	void ThrowAwayBlock(){
	}
	
	[RPC]
	Block Tap(float force, NetworkMessageInfo info)
    {
		bumpMatcher.Add(new Bump(info.timestamp, force, new NetworkPlayerWrapper(info.sender)));
		return null;
	}

	/// <summary>
	/// Rounds the location to the nearest int
	/// </summary>
	/// <returns>The roundedlocation.</returns>
	/// <param name="location">Location.</param>
	private Vector3 roundLocation(Vector3 location){
        return new Vector3((float) Math.Round(location.x, 1), (float) Math.Round(location.y, 1), (float) Math.Round(location.z, 1));
		//return new Vector3(Mathf.Round(location.x), Mathf.Round(location.y), Mathf.Round(location.z));
	}

	/// <summary>
	/// Generates a random colour as Vector3 to enable using it in RPC call.
	/// </summary>
	/// <returns>The color (all values between 0 and 1).</returns>
	private Vector3 randomColor(){
		return new Vector3 ((float)(UnityEngine.Random.Range (0, 1000) / 1000.0), 
		                    (float)(UnityEngine.Random.Range (0, 1000) / 1000.0), 
		                    (float)(UnityEngine.Random.Range (0, 1000) / 1000.0));
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