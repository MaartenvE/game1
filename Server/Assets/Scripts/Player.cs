using UnityEngine;

public class Player : IPlayer
{

	public const float FULL_BLOCK_CHANCE = 0.4f;
    public ITeam Team { get; set; }

    public HalfBlock HalfBlock { get; set; }

    private INetworkPlayer networkPlayer;
    public INetworkPlayer NetworkPlayer
    {
        get
        {
            return networkPlayer;
        }
    }

    public CubeFingerBehaviour CubeFinger { get; set; }

    public Player(INetworkPlayer player)
    {
        networkPlayer = player;
        _networkView = new NetworkViewWrapper(GameObject.Find("Player").networkView);
    }

	private INetworkView _networkView; //this is the networkview on which the player exists.
	private INetwork _Network;
	private INetworkPlayer _NetworkPlayer; //this is the actual networkPlayer

	private IInstantiatedBlock _Finger;
    public bool HasPlaceableBlock { get; set; }
	

	public Player(INetworkView networkView, INetwork network, INetworkPlayer networkPlayer){
		_networkView = networkView;
		_Network = network;
        _NetworkPlayer = networkPlayer;
        HalfBlock = null;
        InstantiateFinger();
	}

    // unused
	private void InstantiateFinger(){
		GameObject prefab = Resources.Load ("Block") as GameObject;
		GameObject finger = _Network.Instantiate (prefab, new Vector3(0,0,0), prefab.transform.rotation, 1) as GameObject;
        _Finger = new InstantiatedBlock(finger) as IInstantiatedBlock;
        Vector3 color = new Vector3(0, 0, 0);
		finger.layer = 2;


		//this tells the client that this is the players personal finger. (as oposed to the other fingers)
        _networkView.RPC("InstantiateFinger", RPCMode.OthersBuffered, finger.networkView.viewID, color);
		_networkView.RPC ("InstantiatePersonalFinger", _NetworkPlayer, finger.networkView.viewID);

		//now color the block correctly
		_networkView.RPC ("ColorBlock", RPCMode.AllBuffered, finger.networkView.viewID, color);
	}

	
	public void GiveInventoryBlock(){
        if (HalfBlock == null)
        {
            HalfBlock = new HalfBlock(SubtractiveHalfBlockColorBehaviour.RandomPrimaryColor());
            Vector3 color = ColorModel.ConvertToVector3(HalfBlock.CalculateUnityColor());
            
            CubeFinger.UpdateColor(color);

			if(Random.value < FULL_BLOCK_CHANCE){
				this.HasPlaceableBlock = true;
				_networkView.RPC("SetBlockFull", networkPlayer);
			}
            else{
				this.HasPlaceableBlock = false;
				_networkView.RPC("SetBlockHalf", networkPlayer);
			}
            _networkView.RPC("SetHalfBlockColor", networkPlayer, color);
            
        }        
        
	}

    public void GiveNewInventoryBlock()
    {
        HalfBlock = null;
        GiveInventoryBlock();
    }

    public void CombineBlock(IPlayer other)
    {
        if (!this.HasPlaceableBlock && !other.HasPlaceableBlock)
        {
            this.HalfBlock.CombineHalfBlock(other.HalfBlock);
            this.HasPlaceableBlock = true;
            Vector3 color = ColorModel.ConvertToVector3(HalfBlock.CalculateUnityColor());
            _networkView.RPC("SetHalfBlockColor", networkPlayer, color);
            _networkView.RPC("SetBlockFull", networkPlayer);
            CubeFinger.UpdateColor(color);
            other.GiveNewInventoryBlock();
        }
    }


	public void DestroyInventoryBlock(){
        HalfBlock = null;
	}


    public void givePlayerAColor(Vector3 color){
        _Finger.SetColor(color);
        _networkView.RPC("GivePlayerAColor", networkPlayer, color);
    }
	

	//incoming error
	[RPC]
	public void GiveServerAnError(string errorMessage){
		Debug.LogError (errorMessage);
	}

	//outgoing error
	public void GivePlayerAnError(string errorMessage){

		_networkView.RPC ("GivePlayerAnError", _NetworkPlayer , errorMessage);
	}
	
}
