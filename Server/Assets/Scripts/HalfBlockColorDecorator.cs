using UnityEngine;
using System.Collections;

public class HalfBlockColorDecorator : HalfBlockDecorator {

    private AbstractHalfBlockColor _color;

    public AbstractHalfBlockColor color
    {
        get { return _color;}
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
