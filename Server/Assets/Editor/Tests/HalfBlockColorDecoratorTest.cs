using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class HalfBlockColorDecoratorTest {

    private HalfBlockDecorator _wrappedObject;
    private HalfBlockColorDecorator _colorDecorator;
    private AbstractHalfBlockColor _color;
    private Color _unityColor;

    [SetUp]
    public void SetUp()
    {
        _color = new HalfBlockColor(ColorModel.RED);
        _colorDecorator = new HalfBlockColorDecorator(_color);
        _unityColor = ColorModel.RED;
    }
    

    [Test]
    public void CalculateSingleUnityColorTest()
    {
        Color result = _colorDecorator.CalculateUnityColor();
        Assert.AreEqual(_unityColor, result);
    }
}
