using UnityEngine;

/// <summary>
/// Load the default bump detector.
/// </summary>
public class BumpDetectorLoader : MonoBehaviour {

	private BumpDetector detector;

	void Start () {
		detector = new BumpDetector(new Accelerometer());
	}

	void Update () {
		detector.DetectBump();
	}
}
