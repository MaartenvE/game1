using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class HalfBlockDecoratorTest
{
    static object[] Colors = 
    {
        new object[] {ColorModel.RED, null, ColorModel.RED},
        new object[] {ColorModel.RED, ColorModel.RED,ColorModel.RED},
        new object[] {ColorModel.YELLOW, ColorModel.YELLOW,ColorModel.YELLOW},
        new object[] {ColorModel.BLUE, ColorModel.BLUE,ColorModel.BLUE},

        new object[] {ColorModel.RED, ColorModel.YELLOW,ColorModel.ORANGE},
        new object[] {ColorModel.YELLOW, ColorModel.RED,ColorModel.ORANGE},
        new object[] {ColorModel.RED, ColorModel.BLUE,ColorModel.PURPLE},
        new object[] {ColorModel.BLUE, ColorModel.RED,ColorModel.PURPLE},
        new object[] {ColorModel.YELLOW, ColorModel.BLUE,ColorModel.GREEN},
        new object[] {ColorModel.BLUE, ColorModel.YELLOW,ColorModel.GREEN}
    };


    [Test, TestCaseSource("Colors")]
    public void CalculateUnityColorTest(Color firstColor, Color secondColor, Color expectedColor)
    {
        HalfBlockDecorator halfBlockDecorator = new HalfBlockDecorator();
        
        HalfBlockColorDecorator first = new HalfBlockColorDecorator(new HalfBlockColor(firstColor));
        HalfBlockColorDecorator second = null;
        if (!secondColor.Equals(new Color()))
        {
            second = new HalfBlockColorDecorator(new HalfBlockColor(secondColor));
        }
        halfBlockDecorator.wrappedObject = first;
        first.wrappedObject = second;

        Color result = halfBlockDecorator.CalculateUnityColor();
        Assert.AreEqual(expectedColor, result);
    }
}
