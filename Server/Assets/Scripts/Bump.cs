using UnityEngine;

public struct Bump
{
    public readonly double Time;
    public readonly float Force;
	public readonly NetworkPlayer Sender;

    public Bump(double time, float force, NetworkPlayer sender)
    {
        Time = time;
        Force = force;
		Sender = sender;
    }
}

