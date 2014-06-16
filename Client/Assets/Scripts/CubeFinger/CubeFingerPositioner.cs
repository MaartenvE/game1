using UnityEngine;

namespace BuildingBlocks.CubeFinger
{
    public class CubeFingerPositioner : BuildingBlocksBehaviour
    {
        private const float MAX_PICKING_DISTANCE = 200000;

        //gives the boundarys deciding what side of a square is a square (needed due to double roundoff errors)
        private const double LOWER_BOUNDARY = 0.4999;
        private const double UPPER_BOUNDARY = 0.5001;

        private BaseCubeFinger finger;

        public CubeFingerPositioner(BaseCubeFinger finger) : base(finger.gameObject)
        {
            this.finger = finger;
        }
        
        public bool CalculateDisplacement(out IGameObject pickedObject, out Vector3 displacement)
        {
            bool show = false;
            pickedObject = null;
            displacement = Vector3.zero;

            // Send a ray from the center of the screen to the object
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit hit = new RaycastHit();

            // todo: refactor Player.HasFullBlock
            //if ((finger.Mode == CubeFingerMode.Delete || GameObject.Find("Player").GetComponent<PlayerInfo>().HasFullBlock)
            //    && Physics.Raycast(ray, out hit, MAX_PICKING_DISTANCE))
            if (Physics.Raycast(ray, out hit, MAX_PICKING_DISTANCE))
            {
                Transform picked = hit.transform;
                Transform parent = picked.transform.parent;
                // todo: refactor TeamInfo.IsMine
                if (parent.CompareTag("imageTarget") && parent.parent.GetComponent<TeamInfoLoader>().TeamInfo.IsMine())
                {
                    pickedObject = new GameObjectWrapper(picked.gameObject);

                    if (finger.Mode == CubeFingerMode.Build)
                    {
                        displacement = calculateSide(pickedObject, hit);
                    }

                    show = true;
                }
            }

            return show;
        }

        //calculate which side of the cube (i.e. localObject) the RayCastHit hits
        private Vector3 calculateSide(IGameObject localObject, RaycastHit hit)
        {
            //bring the vector into the movedObjects own localspace
            Vector3 localizedVector = localObject.transform.InverseTransformPoint(hit.point);

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

    }
}
