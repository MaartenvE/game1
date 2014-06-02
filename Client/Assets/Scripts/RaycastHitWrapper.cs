using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class RaycastHitWrapper :IRaycastHit 
    {
        private RaycastHit nativeRaycastHit;

        public void SetNativeRaycastHit(RaycastHit nativeRaycastHit)
        {
            this.nativeRaycastHit = nativeRaycastHit;
        }

        public Transform transform()
        {
            return nativeRaycastHit.transform;
        }

        public Vector3 point()
        {
            return nativeRaycastHit.point;
        }

}

