using UnityEngine;
using System.Collections;

public class NetworkViewWrapper :INetworkView{

	private NetworkView nativeNetworkView;

	public GameObject gameObject(){
		return nativeNetworkView.gameObject;
	}
	
	public void SetNativeNetworkView(NetworkView nativeNetworkView){
		this.nativeNetworkView = nativeNetworkView;
	}

	public NetworkView NativeNetworkView{
		set{nativeNetworkView = value;}
	}
	
	public void RPC(string name, RPCMode mode, params object[] args){
		nativeNetworkView.RPC(name, mode, args);
	}

<<<<<<< HEAD
	public void RPC(string name, INetworkPlayer player, params object[] args){
		nativeNetworkView.RPC (name, player.getNetworkPlayer(), args);
=======
	public void RPC(string name, NetworkPlayer player, params object[] args){
		nativeNetworkView.RPC (name, player, args);
>>>>>>> 505cab0c1b03405b9bb5caa0a05db20c3762f808
	}

	public INetworkView Find(NetworkViewID viewID){
		INetworkView networkView = new NetworkViewWrapper ();
		networkView.SetNativeNetworkView(NetworkView.Find(viewID));
		return networkView;
	}



	public NetworkViewID getNetworkViewID(){

		return nativeNetworkView.viewID;
	}
}
