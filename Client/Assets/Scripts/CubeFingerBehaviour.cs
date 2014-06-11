using UnityEngine;
using System.Collections;

public class CubeFingerBehaviour : MonoBehaviour
{
    public static bool DeleteMode = false;

    public bool IsMine = false;
    private float maxPickingDistance = 200000;// increase if needed, depending on your scene size

    //gives the boundarys deciding what side of a square is a square (needed due to double roundoff errors)
    private static readonly double LOWER_BOUNDARY = 0.4999;
    private static readonly double UPPER_BOUNDARY = 0.5001;

    //this is the clicker for recognizing single/double click
    private ClickEventHandler clicker;

    //this is the currently selected object (i.e. hit by a raytrace from the center of the screen into the scene)
    private Transform pickedObject = null;
    private Vector3 previousLocation = Vector3.zero;

    //calculate which side of the cube (i.e. localObject) the RayCastHit hits
    private Vector3 CalculateSide(Transform localObject, Vector3 hit)
    {
        //bring the vector into the movedObjects own localspace
        Vector3 localizedVector = localObject.InverseTransformPoint(hit);

        //this is the vector that says in which direction the 
        Vector3 displacement = Vector3.zero;

        //the localizedvector ranges represent the sides of a square (if x=0.5, it half the distance of a square from the center of the square to the x dimension. so it hits the x side of the square)
        if (localizedVector.x > LOWER_BOUNDARY && localizedVector.x < UPPER_BOUNDARY)
        {
            displacement = Vector3.right;
        }
        else if (localizedVector.y > LOWER_BOUNDARY && localizedVector.y < UPPER_BOUNDARY)
        {
            displacement = Vector3.up;
        }
        else if (localizedVector.z > LOWER_BOUNDARY && localizedVector.z < UPPER_BOUNDARY)
        {
            displacement = Vector3.forward;
        }
        else if (localizedVector.x < -LOWER_BOUNDARY && localizedVector.x > -UPPER_BOUNDARY)
        {
            displacement = Vector3.left;
        }
        else if (localizedVector.y < -LOWER_BOUNDARY && localizedVector.y > -UPPER_BOUNDARY)
        {
            displacement = Vector3.down;
        }
        else if (localizedVector.z < -LOWER_BOUNDARY && localizedVector.z > -UPPER_BOUNDARY)
        {
            displacement = Vector3.back;
        }

        return displacement;
    }

    void moveFinger(Transform finger, IRaycastHit hit)
    {
        pickedObject = hit.transform();
        Vector3 displacement = CalculateSide(pickedObject, hit.point());

        transform.position = pickedObject.TransformPoint(displacement);

        if (transform.localPosition != previousLocation)
        {
            networkView.RPC("MoveFinger", RPCMode.Server, transform.localPosition);
            previousLocation = transform.localPosition;
        }
    }

    void showFinger(bool show)
    {
        if (gameObject.renderer.enabled != show)
        {
            gameObject.renderer.enabled = show;
            networkView.RPC("ShowFinger", RPCMode.Server, show ? 1 : 0);
        }
    }

    void placeObject(GameObject obj, Vector3 direction)
    {
        obj.GetComponent<BlockBehaviour>().Place(direction);
    }

    void removeObject(GameObject obj)
    {
        obj.GetComponent<BlockBehaviour>().Remove();
    }

    [RPC]
    void SetCubeFingerParent(string parent)
    {
        this.transform.parent = GameObject.Find(parent).transform as Transform;
        this.transform.localRotation = new Quaternion();
    }

    [RPC]
    void SetPersonalCubeFinger()
    {
        this.IsMine = true;
        clicker = GameObject.Find("Client").GetComponent<ClickEventHandler>();
    }

    [RPC]
    void MoveFinger(Vector3 location)
    {
        if (!IsMine)
        {
            gameObject.transform.localPosition = location;
        }
    }

    [RPC]
    void ColorFinger(Vector3 color)
    {
        Color temp = ColorModel.ConvertToUnityColor(color);
        temp.a = 0.6f;
        this.renderer.material.color = temp;
    }

    [RPC]
    void ShowFinger(int show)
    {
        if (!IsMine)
        {
            gameObject.renderer.enabled = show != 0;
        }
    }

    void Update()
    {
        if (IsMine)
        {
            bool show = false;

            //send a ray from the center of the screen to the object
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, maxPickingDistance))
            {
                pickedObject = hit.transform;

                if (pickedObject.transform.parent.parent.GetComponent<TeamInfoLoader>().TeamInfo.IsMine())
                {
                    IRaycastHit raycastHitWrapper = new RaycastHitWrapper();
                    raycastHitWrapper.SetNativeRaycastHit(hit);

                    //move the finger to correct position and show it
                    moveFinger(transform, raycastHitWrapper);
                    show = true;

                    if (!DeleteMode && clicker.SingleClick() && gameObject.activeInHierarchy)
                    {
                        placeObject(pickedObject.gameObject, CalculateSide(pickedObject, hit.point));
                        show = false;
                    }

                    else if (DeleteMode && clicker.SingleClick() && gameObject.activeInHierarchy)
                    {
                        removeObject(pickedObject.gameObject);
                        show = false;
                    }
                }
            }


            else
            {
                // if the trace did not hit anything, there is no sense in having a cubeFinger enabled.
                show = false;
                pickedObject = null;
            }

            showFinger(show);
        }
    }
}
