using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

	public GameObject GameCamera;

	// Update is called once per frame
	void Update () {
		foreach (Transform transform in this.transform) {
			transform.transform.LookAt (getViewLocation ());
			//rotate letters right side up
			transform.transform.Rotate (new Vector3 (180f, 0f, 180f));
		}
	}

	private Vector3 getViewLocation () {
		Vector3 vector = GameCamera.transform.position;

		return new Vector3(vector.x, vector.y, vector.z);
	}
}
