using UnityEngine;

public interface IPlayer
{
    ITeam Team { get; set; }
    INetworkPlayer NetworkPlayer { get; }
}
