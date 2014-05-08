using UnityEngine;
using System.Collections;

public class TouchBehaviour : MonoBehaviour 
{

	public GameObject parentObject;
	public GameObject cubePrefab;
	private float maxPickingDistance = 2000;// increase if needed, depending on your scene size
	
	private Transform pickedObject = null;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
				if (Input.GetMouseButtonDown (0)) {

						//if(touch.phase == TouchPhase.Began)
						//{
						//Debug.Log("Touching at: " + touch.position);
			
						//Gets the ray at position where the screen is touched
						Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
						//Debug.Log("Touch phase began at: " + touch.position);
				
						RaycastHit hit = new RaycastHit ();
						if (Physics.Raycast (ray, out hit, maxPickingDistance)) { 
								pickedObject = hit.transform;
								Vector3 pos = hit.point;
								GameObject newCube = Instantiate (cubePrefab, getPosition (hit, pickedObject), pickedObject.rotation) as GameObject;
								newCube.transform.parent = parentObject.transform;					
								newCube.transform.localScale = cubePrefab.transform.localScale;				

						} else {
								pickedObject = null;
						}
						//}

				}
				if (Input.GetMouseButtonDown (1)) {
						Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
						//Debug.Log("Touch phase began at: " + touch.position);
			
						RaycastHit hit = new RaycastHit ();
						if (Physics.Raycast (ray, out hit, maxPickingDistance)) { 
								pickedObject = hit.transform;
								Destroy (pickedObject.gameObject);
						} else {
								pickedObject = null;
						}
				}
		}
		
	Vector3 getPosition(RaycastHit hit, Transform gameObject)
	{
				Vector3 res = gameObject.position;
				print ("GameObject Hit: (x: " + res.x + " y: " + res.y + " z: " + res.z + ")");
				Vector3 relativePosition = gameObject.InverseTransformPoint (hit.point);
				Debug.Log ("x: " + relativePosition.x + " y: " + relativePosition.y + " z: " + relativePosition.z);
				if (relativePosition.x > 0.499995 && relativePosition.x < 0.500005) {
						res.x += gameObject.lossyScale.x;
						Debug.Log ("To the right");
				} 
				if (relativePosition.x < -0.499995 && relativePosition.x > -0.500005) {
						res.x -= gameObject.lossyScale.x;
						Debug.Log ("To the left");
				}
				if (relativePosition.y > 0.499995 && relativePosition.y < 0.500005) {
						res.y += gameObject.lossyScale.y;
						Debug.Log ("Above");
				}
				if (relativePosition.y < -0.499995 && relativePosition.y > -0.500005) {
						res.y -= gameObject.lossyScale.y;	
						Debug.Log ("Below");
				}
				if (relativePosition.z > 0.499995 && relativePosition.z < 0.500005) {
						res.z += gameObject.lossyScale.z;
						Debug.Log ("In Front");
				} 
				if (relativePosition.z < -0.499995 && relativePosition.z > -0.500005) {
						res.z -= gameObject.lossyScale.z;	
						Debug.Log ("Behind");
				}
				print ("Returned Pos: (x: " + res.x + " y: " + res.y + " z: " + res.z + ")");
				return res;

	}

}