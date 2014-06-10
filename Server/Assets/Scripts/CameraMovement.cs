using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public Vector3 CenterOfMove;
	public float RadiusOfMove;
	public float MovementSpeed;

	private float currentAngle;
	 //float circleLength;
	private Vector3 RadiusVector;

	// Use this for initialization
	void Start () {
		//circleLength = 2 * Mathf.PI * RadiusOfMove;
		float x = RadiusOfMove * Mathf.Cos (45);
		float z = RadiusOfMove * Mathf.Sin (45);
		RadiusVector = new Vector3 (x,0,z);
	}

	private GameObject getObject(){
		return this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject camera = getObject ();

		currentAngle += MovementSpeed;
		camera.transform.position = CenterOfMove+rotateVector (RadiusVector, currentAngle);
		GameObject testblock = GameObject.FindGameObjectWithTag("testcube");
		camera.transform.LookAt (getCameraRotationTo(CenterOfMove));

	}

	public Vector3 rotateVector(Vector3 vector, float degrees){
		//simulates a rotation matrix (not possible in unity, as it only has 4x4 matrixes)
		float newX = vector.x * Mathf.Cos (degrees) - vector.z * Mathf.Sin (degrees);
		float newZ = vector.x * Mathf.Sin (degrees) + vector.z * Mathf.Cos (degrees);

		return new Vector3(newX, vector.y, newZ);
	}

	public Vector3 getCameraRotationTo(Vector3 vector){
		return new Vector3(50,0,50);
	}


}
