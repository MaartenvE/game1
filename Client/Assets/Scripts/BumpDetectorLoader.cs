using UnityEngine;

/// <summary>
/// Load the default bump detector.
/// </summary>
public class BumpDetectorLoader : MonoBehaviour {
	
	public static BumpDetector Detector;
	
	void Start () {
		Detector = new BumpDetector(new Accelerometer());
	}
	
	void Update () {
		Detector.DetectBump();
	}
}
