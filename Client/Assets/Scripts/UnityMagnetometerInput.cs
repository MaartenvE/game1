using UnityEngine;

public class UnityMagnetometerInput : IMagnetometerInput
{
	public Vector3 Magnetisation
	{
		get {
			return Input.compass.rawVector;
		}
	}
}
