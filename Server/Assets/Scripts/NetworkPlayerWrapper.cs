using UnityEngine;
using System.Collections;

public class NetworkPlayerWrapper : INetworkPlayer {

    NetworkPlayer wrappedPlayer;

    public NetworkPlayerWrapper(NetworkPlayer player){
        wrappedPlayer = player;
    }

    public NetworkPlayer getNetworkPlayer(){
        return wrappedPlayer;
    }

    public string toString(){
        return wrappedPlayer.ToString();
    }
	
}
