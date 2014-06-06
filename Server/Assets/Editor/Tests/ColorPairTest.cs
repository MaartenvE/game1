using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class ColorPairTest {

    private AbstractHalfBlockColor color1;
    private AbstractHalfBlockColor color2;
    private ColorPair colorPair1;
    private ColorPair colorPair2;

    [SetUp]
    public void SetUp()
    {
        color1 = new HalfBlockColor(ColorModel.RED);
        color2 = new HalfBlockColor(ColorModel.BLUE);
        colorPair1 = new ColorPair(color1, color2);
        colorPair2 = new ColorPair(color2, color1);
    }

    [Test]
    public void EqualsSameOrderTest()
    {
        Assert.True(colorPair1.Equals(colorPair1));
    }

    [Test]
    public void EqualsReverseOrderTest()
    {
        Assert.True(colorPair1.Equals(colorPair2));
    }

    [Test]
    public void HashCodeSymmetricTest()
    {
        Assert.AreEqual(colorPair1.GetHashCode(), colorPair2.GetHashCode());
    }
}
