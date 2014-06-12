using UnityEngine;

public class Player : IPlayer
{
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
	//private IBlock _InventoryBlock; //this is the currently allocated block the player has.

	//private Time _StartOfPenaltyTime; //this is the time the penalty started.
	//private Time _PenaltyLength; //this is how long the penalty lasts.
	

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
		//finger.renderer.material.color = new Color(0.8f,0.05f,0.8f,0.08f);
	}

	
	public void GiveInventoryBlock(){
        if (HalfBlock == null)
        {
            HalfBlock = new HalfBlock(SubtractiveHalfBlockColorBehaviour.RandomPrimaryColor());
            Vector3 color = ColorModel.ConvertToVector3(HalfBlock.CalculateUnityColor());
            this.HasPlaceableBlock = false;
            CubeFinger.UpdateColor(color);
            
            _networkView.RPC("SetHalfBlockColor", networkPlayer, color);
            _networkView.RPC("SetBlockHalf", networkPlayer);

            //_networkView.RPC("SetBlockHalf", _NetworkPlayer);
            //_networkView.RPC("SetHalfBlockColor", _NetworkPlayer, color);
            //finger.renderer.material.color = ColorModel.ConvertToUnityColor(color);
        }        
        
	}

    public void GiveNewInventoryBlock()
    {
        HalfBlock = null;
        GiveInventoryBlock();
    }

    public void CombineBlock(IPlayer other)
    {
        this.HalfBlock.CombineHalfBlock(other.HalfBlock);
        this.HasPlaceableBlock = true;
        Vector3 color = ColorModel.ConvertToVector3(HalfBlock.CalculateUnityColor());
        _networkView.RPC("SetHalfBlockColor", networkPlayer, color);
        _networkView.RPC("SetBlockFull", networkPlayer);
        other.GiveInventoryBlock();
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
