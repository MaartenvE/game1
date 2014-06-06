using UnityEngine;

public class NetworkPlayerWrapper : INetworkPlayer
{

    private NetworkPlayer _wrappedPlayer;

    public NetworkPlayerWrapper(NetworkPlayer player)
    {
        _wrappedPlayer = player;
    }

    public NetworkPlayer getNetworkPlayer()
    {
        return _wrappedPlayer;
    }

    public override string ToString()
    {
        return _wrappedPlayer.ToString();
    }

    public override bool Equals(object obj)
    {
        INetworkPlayer p = obj as INetworkPlayer;
        if (p != null)
        {
            return _wrappedPlayer.Equals(p.getNetworkPlayer());
        }
        return false;
    }
}
