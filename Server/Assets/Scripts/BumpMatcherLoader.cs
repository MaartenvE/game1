using UnityEngine;
using BuildingBlocks.Team;
using BuildingBlocks.Player;
using BuildingBlocks;

public class BumpMatcherLoader : MonoBehaviour
{
    private IBumpMatcher bumpMatcher;
    public IBumpMatcher BumpMatcher
    {
        get
        {
            return bumpMatcher;
        }
    }

	void Start()
    {
        bumpMatcher = new BumpMatcher();
        bumpMatcher.OnBumpMatch += (Bump bump1, Bump bump2) => HandleBumpEvent(bump1.Sender, bump2.Sender);
	}

    private void HandleBumpEvent(INetworkPlayer networkPlayer1, INetworkPlayer networkPlayer2)
    {
        networkView.RPC("BumpMatch", networkPlayer1.NetworkPlayer);
        networkView.RPC("BumpMatch", networkPlayer2.NetworkPlayer);

        IPlayer player1 = Player.GetPlayer(networkPlayer1);
        IPlayer player2 = Player.GetPlayer(networkPlayer2);
        if (Random.value < 0.5)
        {
            player1.CombineBlock(player2); 
        }
        else
        {
            player2.CombineBlock(player1);
        }
               
    }

    [RPC]
    void Tap(float force, NetworkMessageInfo info)
    {
        bumpMatcher.Add(new Bump(info.timestamp, force, new NetworkPlayerWrapper(info.sender)));
    }

    [RPC]
    void BumpMatch() { }
}
