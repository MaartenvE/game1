using UnityEngine;

public struct Bump
{
    public readonly double Time;
    public readonly float Force;
	public readonly INetworkPlayer Sender;

    public Bump(double time, float force, INetworkPlayer sender)
    {
        Time = time;
        Force = force;
		Sender = sender;
    }
}

