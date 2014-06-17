using UnityEngine;
using BuildingBlocks.CubeFinger;

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
	
	public void GiveInventoryBlock(){
        if (HalfBlock == null)
        {
            HalfBlock = new HalfBlock(SubtractiveHalfBlockColorBehaviour.RandomPrimaryColor());
            Vector3 color = ColorModel.ConvertToVector3(HalfBlock.CalculateUnityColor());
            CubeFinger.Renderer.SetColor(HalfBlock.CalculateUnityColor());

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
            CubeFinger.Renderer.SetColor(HalfBlock.CalculateUnityColor());
            other.GiveNewInventoryBlock();
        }
    }


	public void DestroyInventoryBlock(){
        HalfBlock = null;
	}
	
}
