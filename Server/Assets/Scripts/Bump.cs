using UnityEngine;

class Bump
{
    public readonly float Time;
    public readonly float Heading;
    public readonly Quaternion Attitude;

    public Bump(float time, float heading, Quaternion attitude)
    {
        Time = time;
        Heading = heading;
        Attitude = attitude;
    }
}

