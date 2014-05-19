using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class TouchBehaviour : MonoBehaviour 
{

	public GameObject cubePrefab;
	public GameObject cubeFinger;
	private float maxPickingDistance = 200000;// increase if needed, depending on your scene size

	//gives the boundarys deciding what side of a square is a square (needed due to double roundoff errors)
	private readonly double LOWER_BOUNDARY = 0.4999;
	private readonly double UPPER_BOUNDARY = 0.5001;

	//this is the clicker for recognizing single/double click
	private ClickEventHandler clicker;

	//this is the currently selected object (i.e. hit by a raytrace from the center of the screen into the scene)
	private Transform pickedObject = null;

	//calculate which side of the cube (i.e. localObject) the RayCastHit hits
	Vector3 calculateSide(Transform localObject, RaycastHit hit){
		//Vector3 res = localObject.position;
		
		//bring the vector into the movedObjects own localspace
		Vector3 localizedVector = localObject.InverseTransformPoint (hit.point);
		
		Debug.Log (localizedVector);
		
		//this is the vector that says in which direction the 
		Vector3 displacement = new Vector3 (0, 0, 0);
		
		//the localizedvector ranges represent the sides of a square (if x=0.5, it half the distance of a square from the center of the square to the x dimension. so it hits the x side of the square)
		if (localizedVector.x >LOWER_BOUNDARY && localizedVector.x<UPPER_BOUNDARY) {
			displacement = new Vector3(1,0,0);
			Debug.Log("Right");
		}
		if (localizedVector.y >LOWER_BOUNDARY && localizedVector.y<UPPER_BOUNDARY) {
			displacement = new Vector3(0,1,0);
			Debug.Log("Above");
		}
		if (localizedVector.z >LOWER_BOUNDARY && localizedVector.z<UPPER_BOUNDARY) {
			displacement = new Vector3(0,0,1);
			Debug.Log("In Front");
		}
		if (localizedVector.x <-LOWER_BOUNDARY && localizedVector.x>-UPPER_BOUNDARY) {
			displacement = new Vector3(-1,0,0);
			Debug.Log("Left");
		}
		if (localizedVector.y <-LOWER_BOUNDARY && localizedVector.y>-UPPER_BOUNDARY) {
			displacement = new Vector3(0,-1,0);
			Debug.Log("Below");
		}
		if (localizedVector.z <-LOWER_BOUNDARY && localizedVector.z>-UPPER_BOUNDARY) {
			displacement = new Vector3(0,0,-1);
			Debug.Log("Behind");
		}
		
		return displacement;
	}
	
	//updates object position, and location index
	void moveFingerToSide(Transform finger, RaycastHit hit){
		pickedObject = hit.transform;
		Vector3 displacement = calculateSide (pickedObject, hit);

		//transform the displacement from localobject to the new coords to which the movedObject should go
		finger.transform.position = pickedObject.TransformPoint (displacement);
		finger.GetComponent<location> ().index = pickedObject.GetComponent<location> ().index + displacement;
		
	}

	//places a square at the exact coordinates of the cubefinger
	void PlaceSquareAtFinger(){
		this.networkView.RPC ("PlaceBlock", RPCMode.Server, cubeFinger.transform.position, cubeFinger.GetComponent<location>().index, pickedObject.networkView.viewID);
	}

	//calls to remove the current pickedobject to server (assumes it has a networkview)
	void RemovePickedObject(){
		this.networkView.RPC ("RemoveBlock", RPCMode.Server, pickedObject.networkView.viewID);
	}

	//Start is called at start
	void Start(){
		//give the clicker the correct clicking component
		clicker = gameObject.GetComponent<ClickEventHandler> ();
	}


	// Update is called once per frame
	void Update () 
	{

		//send a ray from the center of the screen to the object
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit hit = new RaycastHit();
		//if the ray hits the object, then do stuff
		if (Physics.Raycast(ray, out hit, maxPickingDistance)) 
		{ 
			//retrieve the object that was hit
			pickedObject = hit.transform;

			//move the finger to correct position and show it
			moveFingerToSide(cubeFinger.transform, hit);
			cubeFinger.SetActive(true);

			//if a build action is given, place the block at the cubefinger location
			if (clicker.SingleClick() && cubeFinger.activeInHierarchy) {
				PlaceSquareAtFinger();
				//disable cubefinger, so it is placed in it s shiny new good position on next update
				cubeFinger.SetActive(false);
			}
			//if a remove action is given, remove the block pointed to by the raycast
			if(clicker.DoubleClick() && cubeFinger.activeInHierarchy){
				RemovePickedObject();
				cubeFinger.SetActive(false);
			}

		} 
		else
		{
			//if the trace did not hit anything, there is no sense in having a cubefinger enabled
			cubeFinger.SetActive(false);
			pickedObject = null;
		}
	}
}