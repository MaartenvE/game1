using UnityEngine;
using System.Collections;

public class HalfBlockDecorator : HalfBlock{

    public virtual Color CalculateUnityColor()
    {
        return wrappedObject.CalculateUnityColor();
    }

}
