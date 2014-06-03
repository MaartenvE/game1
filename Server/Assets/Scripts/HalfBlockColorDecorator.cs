using UnityEngine;
using System.Collections;

public class HalfBlockColorDecorator {
    private HalfBlockDecorator _wrappedObject;
    private AbstractHalfBlockColor _color;

    public HalfBlockDecorator wrappedObject
    {
        get { return _wrappedObject; }
        set { _wrappedObject = value; }
    }

    public HalfBlockColorDecorator(AbstractHalfBlockColor color)
    {
        this._color = color;
    }

    public Color CalculateUnityColor()
    {
        Color blue1 = Color.blue;
        Color blue2 = Color.blue;

        Debug.Log(blue1.Equals(blue2));
        Debug.Log(blue1.GetHashCode() == blue2.GetHashCode());
        return Color.red;
    }

    private HalfBlockColor CalculateColor()
    {
        return null;
    }
}
