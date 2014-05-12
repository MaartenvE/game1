using UnityEngine;
using System.Collections;

public class TouchBehaviour : MonoBehaviour 
{
	
	//public GameObject parentObject;
	public GameObject cubePrefab;
	public GameObject cubeFinger;
	private float maxPickingDistance = 200000;// increase if needed, depending on your scene size
	private readonly double lowerBoundary = 0.4999;
	private readonly double upperBoundary = 0.5001;
	
	private Transform pickedObject = null;

	//move object to vector distance from localObject in the localObjects own space
	GameObject moveObjectToSide(Transform localObject, Vector3 vector, GameObject movedObject){
		Vector3 oldPos = localObject.transform.position;
		
		Debug.Log (movedObject.transform.position+" "+vector);
		
		//bring the vector into the movedObjects own localspace
		Vector3 localizedVector = movedObject.transform.InverseTransformPoint (vector);
		
		Debug.Log (localizedVector);

		//the localizedvector ranges represent the sides of a square (if x=0.5, it half the distance of a square from the center of the square to the x dimension. so it hits the x side of the square)
		if (localizedVector.x >lowerBoundary && localizedVector.x<upperBoundary) {
			movedObject.transform.position = localObject.transform.TransformPoint(new Vector3(1,0,0));
		}
		if (localizedVector.y >lowerBoundary && localizedVector.y<upperBoundary) {
			movedObject.transform.position = localObject.transform.TransformPoint(new Vector3(0,1,0));
		}
		if (localizedVector.z >lowerBoundary && localizedVector.z<upperBoundary) {
			movedObject.transform.position = localObject.transform.TransformPoint(new Vector3(0,0,1));
		}
		if (localizedVector.x <-lowerBoundary && localizedVector.x>-upperBoundary) {
			movedObject.transform.position = localObject.transform.TransformPoint(new Vector3(-1,0,0));
		}
		if (localizedVector.y <-lowerBoundary && localizedVector.y>-upperBoundary) {
			movedObject.transform.position = localObject.transform.TransformPoint(new Vector3(0,-1,0));
		}
		if (localizedVector.z <-lowerBoundary && localizedVector.z>-upperBoundary) {
			movedObject.transform.position = localObject.transform.TransformPoint(new Vector3(0,0,-1));
		}
		
		Debug.Log ("current square is at "+oldPos+"the new square will be located at: "+movedObject.transform.position);
		return movedObject;
	}

	//places a square at the exact coordinates of the cubefinger
	void placeSquareAtFinger(){
		GameObject square =  Instantiate(cubePrefab, cubeFinger.transform.position, cubeFinger.transform.rotation) as GameObject;
		square.transform.parent = this.transform;
		square.transform.localScale = cubeFinger.transform.localScale;
	}

	/*
	//creates a square and puts it on the correct side (as determined by vector from the center of the cube)
	GameObject BuildObjectOnSide(Transform localObject, Vector3 vector){
		GameObject square =  Instantiate(cubePrefab, localObject.transform.position, localObject.transform.rotation) as GameObject;
		square.transform.parent = this.transform;
		square.transform.localScale = localObject.localScale;
		//moved the created object to the correct side
		square = moveObjectToSide (localObject, vector, square);

		return square;
		}*/

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
			Vector3 pos = hit.point;

			//place the cubeFinger at the correct location (or remove it if it shouldn t be seen)
			if(pickedObject.Equals(cubeFinger.transform)){
				//if the objects hits the cubeFinger, it is in the right place already.
				//so don t do anything
			}
			else{
				//if it hits a new block, move the blockfinger there
				cubeFinger.SetActive(true);
				moveObjectToSide(pickedObject, pos, cubeFinger);
			}

			//if a build action is given, place the block at the cubefinger location
			if (Input.GetMouseButtonDown (0) && cubeFinger.activeInHierarchy) {
				placeSquareAtFinger();
				//disable cubefinger, so it is placed in it s shiny new good position on next update
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