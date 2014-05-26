using UnityEngine;

/// <summary>
/// Load the default bump detector.
/// </summary>
public class BumpDetectorLoader : MonoBehaviour {
	
	public static BumpDetector Detector;
	
	void Start () {
		Detector = new BumpDetector(
			new Accelerometer(new UnityAccelerometerInput()), 
			new Magnetometer(new UnityMagnetometerInput()));
	}
	
	void Update () {
		Detector.DetectBump();
	}
}
