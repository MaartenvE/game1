using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class TouchBehaviour : MonoBehaviour 
{

	public GameObject cubePrefab;
	public GameObject cubeFinger;
    private INetworkView _networkView;
	private float maxPickingDistance = 200000;// increase if needed, depending on your scene size

	//gives the boundarys deciding what side of a square is a square (needed due to double roundoff errors)
	private static readonly double LOWER_BOUNDARY = 0.4999;
	private static readonly double UPPER_BOUNDARY = 0.5001;

	//this is the clicker for recognizing single/double click
	private ClickEventHandler clicker;

	//this is the currently selected object (i.e. hit by a raytrace from the center of the screen into the scene)
	private static Transform pickedObject = null;

    public INetworkView networkView
    {
        set { _networkView = value; }
        get { return _networkView; }
    }

	//calculate which side of the cube (i.e. localObject) the RayCastHit hits
	public Vector3 CalculateSide(Transform localObject, Vector3 hit){
		//bring the vector into the movedObjects own localspace
		Vector3 localizedVector = localObject.InverseTransformPoint (hit);
		
		//this is the vector that says in which direction the 
		Vector3 displacement = new Vector3 (0, 0, 0);
		
		//the localizedvector ranges represent the sides of a square (if x=0.5, it half the distance of a square from the center of the square to the x dimension. so it hits the x side of the square)
		if (localizedVector.x >LOWER_BOUNDARY && localizedVector.x<UPPER_BOUNDARY) {
			displacement = new Vector3(1,0,0);
		}
		else if (localizedVector.y >LOWER_BOUNDARY && localizedVector.y<UPPER_BOUNDARY) {
			displacement = new Vector3(0,1,0);
		}
		else if (localizedVector.z >LOWER_BOUNDARY && localizedVector.z<UPPER_BOUNDARY) {
			displacement = new Vector3(0,0,1);
		}
		else if (localizedVector.x <-LOWER_BOUNDARY && localizedVector.x>-UPPER_BOUNDARY) {
			displacement = new Vector3(-1,0,0);
		}
		else if (localizedVector.y <-LOWER_BOUNDARY && localizedVector.y>-UPPER_BOUNDARY) {
			displacement = new Vector3(0,-1,0);
		}
		else if (localizedVector.z <-LOWER_BOUNDARY && localizedVector.z>-UPPER_BOUNDARY) {
			displacement = new Vector3(0,0,-1);
		}
		
		return displacement;
	}
	
	//updates object position, and location index
	public void MoveFingerToSide(Transform finger, IRaycastHit hit){
		pickedObject = hit.transform();
		Vector3 displacement = CalculateSide (pickedObject, hit.point());

		//transform the displacement from localobject to the new coords to which the movedObject should go
		finger.transform.position = pickedObject.TransformPoint (displacement);
		finger.GetComponent<Location> ().index = pickedObject.GetComponent<Location> ().index + displacement;
		
	}

	//places a square at the exact coordinates of the cubefinger
	public void PlaceSquareAtFinger(Vector3 fingerPosition, Vector3 locationIndex, NetworkViewID networkViewID){
		this._networkView.RPC ("PlaceBlock", RPCMode.Server, fingerPosition, locationIndex, networkViewID);
	}

	//calls to remove the current pickedobject to server (assumes it has a networkview)
	public void RemovePickedObject(NetworkViewID networkViewID){
		this._networkView.RPC ("RemoveBlock", RPCMode.Server, networkViewID);
	}

	//Start is called at start
	public void Start(){
		//give the clicker the correct clicking component
		clicker = gameObject.GetComponent<ClickEventHandler> ();
	}


	// Update is called once per frame
	public void Update () 
	{

		//send a ray from the center of the screen to the object
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit hit = new RaycastHit();
		//if the ray hits the object, then do stuff
		if (Physics.Raycast(ray, out hit, maxPickingDistance)) 
		{ 
			//retrieve the object that was hit
			pickedObject = hit.transform;

            IRaycastHit raycastHitWrapper = new RaycastHitWrapper();
            raycastHitWrapper.SetNativeRaycastHit(hit);
			//move the finger to correct position and show it
			MoveFingerToSide(cubeFinger.transform, raycastHitWrapper);
			cubeFinger.SetActive(true);

			//if a build action is given, place the block at the cubefinger location
			if (clicker.SingleClick() && cubeFinger.activeInHierarchy) {
                PlaceSquareAtFinger(cubeFinger.transform.position, cubeFinger.GetComponent<Location>().index, pickedObject.networkView.viewID);
				//disable cubefinger, so it is placed in it s shiny new good position on next update
				cubeFinger.SetActive(false);
			}
			//if a remove action is given, remove the block pointed to by the raycast
			if(clicker.DoubleClick() && cubeFinger.activeInHierarchy){
				RemovePickedObject(pickedObject.networkView.viewID);
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