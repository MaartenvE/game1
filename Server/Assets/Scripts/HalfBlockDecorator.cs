using UnityEngine;
using System.Collections;

public class HalfBlockDecorator : HalfBlock{

    private HalfBlockDecorator _wrappedObject;

    public HalfBlockDecorator wrappedObject
    {
        get { return _wrappedObject; }
        set { _wrappedObject = value; }
    }

    public virtual Color CalculateUnityColor()
    {
        return wrappedObject.CalculateUnityColor();
    }

}
