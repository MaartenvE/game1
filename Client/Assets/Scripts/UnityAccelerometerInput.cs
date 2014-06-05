using UnityEngine;

public class UnityAccelerometerInput : IAccelerometerInput
{
	public Vector3 Acceleration
	{
		get {
			return Input.acceleration;
		}
	}
}
