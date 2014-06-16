using UnityEngine;
using BuildingBlocks.CubeFinger;

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

    public ICubeFinger CubeFinger { get; set; }

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
	
	
	public void GiveInventoryBlock(){
        if (HalfBlock == null)
        {
            HalfBlock = new HalfBlock(SubtractiveHalfBlockColorBehaviour.RandomPrimaryColor());
            Vector3 color = ColorModel.ConvertToVector3(HalfBlock.CalculateUnityColor());
            this.HasPlaceableBlock = false;
            CubeFinger.Renderer.SetColor(HalfBlock.CalculateUnityColor());
            
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
        if (!this.HasPlaceableBlock && !other.HasPlaceableBlock)
        {
            this.HalfBlock.CombineHalfBlock(other.HalfBlock);
            this.HasPlaceableBlock = true;
            Vector3 color = ColorModel.ConvertToVector3(HalfBlock.CalculateUnityColor());
            _networkView.RPC("SetHalfBlockColor", networkPlayer, color);
            _networkView.RPC("SetBlockFull", networkPlayer);
            CubeFinger.Renderer.SetColor(HalfBlock.CalculateUnityColor());
            other.GiveNewInventoryBlock();
        }
    }


	public void DestroyInventoryBlock(){
        HalfBlock = null;
	}
	
}
