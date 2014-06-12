using UnityEngine;
using System.Collections;
using System.Linq;

// todo: refactor CubeFingerBehaviour
public class CubeFingerBehaviour : MonoBehaviour
{
    private bool deleteMode = false;
    public bool DeleteMode
    {
        get
        {
            return deleteMode;
        }

        set
        {
            if (IsMine)
            {
                this.deleteMode = value;
                networkView.RPC("SetFingerDeleteMode", RPCMode.Server, value ? 1 : 0);
            }
        }
    }

    private Color color;

    public bool IsMine = false;
    private float maxPickingDistance = 200000;// increase if needed, depending on your scene size

    //gives the boundarys deciding what side of a square is a square (needed due to double roundoff errors)
    private static readonly double LOWER_BOUNDARY = 0.4999;
    private static readonly double UPPER_BOUNDARY = 0.5001;

    //this is the clicker for recognizing single/double click
    private ClickEventHandler clicker;

    //this is the currently selected object (i.e. hit by a raytrace from the center of the screen into the scene)
    private GameObject previousObject = null;
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

    void drawFinger()
    {
        if (deleteMode)
        {
            if (previousObject != null)
            {
                Color color = previousObject.renderer.material.color;
                color.a = 0.4f;
                this.renderer.material.color = color;
                previousObject.renderer.enabled = false;
            }
        }

        else
        {
            this.renderer.material.color = this.color;
        }
    }

    void moveFinger(Transform finger, IRaycastHit hit)
    {
        if (deleteMode)
        {
            transform.localPosition = finger.localPosition;
        }

        else
        {
            Vector3 displacement = CalculateSide(finger, hit.point());
            transform.position = finger.TransformPoint(displacement);
        }

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
        GameObject.Find("Player").GetComponent<PlayerInfo>().CubeFinger = this;
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
    void SetFingerDeleteMode(int deleteMode)
    {
        if (!IsMine)
        {
            this.deleteMode = deleteMode != 0;
        }
    }

    [RPC]
    void ColorFinger(Vector3 color)
    {
        Color unityColor = ColorModel.ConvertToUnityColor(color);
        unityColor.a = 0.6f;
        this.renderer.material.color = unityColor;
        this.color = unityColor;
    }

    [RPC]
    void ShowFinger(int show)
    {
        if (!IsMine)
        {
            gameObject.renderer.enabled = show != 0;
        }
    }

    void Start()
    {
        deleteMode = false;
    }

    void Update()
    {
        if (this.transform.parent.GetComponent<ImageTargetBehaviour>().CurrentStatus == TrackableBehaviour.Status.NOT_FOUND)
        {
            this.renderer.enabled = false;
        }

        else
        {

            if (previousObject != null)
            {
                previousObject.renderer.enabled = true;
            }

            if (IsMine)
            {
                bool show = false;

                //send a ray from the center of the screen to the object
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                RaycastHit hit = new RaycastHit();

                if ((DeleteMode || GameObject.Find("Player").GetComponent<PlayerInfo>().HasFullBlock)
                    && Physics.Raycast(ray, out hit, maxPickingDistance))
                {
                    Transform pickedObject = hit.transform;

                    Transform parent = pickedObject.transform.parent;
                    if (parent.CompareTag("imageTarget") && parent.parent.GetComponent<TeamInfoLoader>().TeamInfo.IsMine())
                    {
                        IRaycastHit raycastHitWrapper = new RaycastHitWrapper();
                        raycastHitWrapper.SetNativeRaycastHit(hit);

                        //move the finger to correct position and show it
                        moveFinger(pickedObject, raycastHitWrapper);
                        show = true;

                        if (!deleteMode && clicker.SingleClick() && gameObject.activeInHierarchy)
                        {
                            placeObject(pickedObject.gameObject, CalculateSide(pickedObject, hit.point));
                            show = false;
                        }

                        else if (deleteMode && clicker.SingleClick() && gameObject.activeInHierarchy)
                        {
                            removeObject(pickedObject.gameObject);
                            show = false;
                        }
                    }
                }

                showFinger(show);
            }

            if (this.renderer.enabled)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, 0.05f);
                if (colliders.Count() > 0)
                {
                    previousObject = colliders[0].gameObject;
                }
            }
            else
            {
                if (previousObject != null)
                {
                    previousObject.renderer.enabled = true;
                    previousObject = null;
                }
            }

            drawFinger();
        }
    }
}
