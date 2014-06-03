using UnityEngine;
using System.Collections;

public class HalfBlockColorDecorator : HalfBlockDecorator {
    private HalfBlockColorDecorator _wrappedObject;
    private AbstractHalfBlockColor _color;

    public AbstractHalfBlockColor color
    {
        get { return _color;}
    }

    public HalfBlockColorDecorator wrappedObject
    {
        get { return _wrappedObject; }
        set { _wrappedObject = value; }
    }

    public HalfBlockColorDecorator(AbstractHalfBlockColor color)
    {
        this._color = color;
    }

    public override Color CalculateUnityColor()
    {
        return CalculateColor().color;
    }

    private AbstractHalfBlockColor CalculateColor()
    {
        if (wrappedObject == null)
        {
            return color;
        }
        return color.CombineColor(wrappedObject.color);
    }
}
