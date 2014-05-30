using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IRaycastHit
    {
        Transform transform();

        Vector3 point();

        void SetNativeRaycastHit(RaycastHit nativeRaycastHit);

}

