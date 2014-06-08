using UnityEngine;
using System.Collections;


public class Player : IPlayer
{
    public ITeam Team { get; set; }

    private INetworkPlayer networkPlayer;
    public INetworkPlayer NetworkPlayer
    {
        get
        {
            return networkPlayer;
        }
    }

    public Player(INetworkPlayer player)
    {
        networkPlayer = player;
    }
}

