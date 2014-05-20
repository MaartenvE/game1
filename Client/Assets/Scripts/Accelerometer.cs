using UnityEngine;

/// <summary>
/// Default accelerometer implementation (using unity's Input.acceleration).
/// </summary>
public class Accelerometer : IAccelerometer
{
	public Vector3 Acceleration { 
		get {
			return Input.acceleration;
		}
	}
}
