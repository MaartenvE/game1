using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Moq;

[TestFixture]
public class HalfBlockColorTest {
    private AbstractHalfBlockColor _red;
	private AbstractHalfBlockColor _yellow;
	private AbstractHalfBlockColor _blue;

    private AbstractHalfBlockColor _green;
    private AbstractHalfBlockColor _orange;
    private AbstractHalfBlockColor _purple;

	[SetUp]
	public void CreateHalfBlocksColors(){
        _red = new HalfBlockColor (ColorModel.RED); 
		_yellow = new HalfBlockColor (ColorModel.YELLOW);
        _blue = new HalfBlockColor(ColorModel.BLUE);

        _green = new HalfBlockColor(ColorModel.GREEN);
        _orange = new HalfBlockColor(ColorModel.ORANGE);
        _purple = new HalfBlockColor(ColorModel.PURPLE);
	}

	[Test]
	public void GetColorValueTest(){
		Assert.AreEqual (ColorModel.GREEN, _green.color);
	}

	[Test]
	public void EqualsTest(){
		AbstractHalfBlockColor _otherGreen = new HalfBlockColor (ColorModel.GREEN);
		Assert.True (_green.Equals(_otherGreen));
	}

    static object[] Colors = 
    {
        new object[] {ColorModel.RED, ColorModel.RED,ColorModel.RED},
        new object[] {ColorModel.YELLOW, ColorModel.YELLOW,ColorModel.YELLOW},
        new object[] {ColorModel.BLUE, ColorModel.BLUE,ColorModel.BLUE},

        new object[] {ColorModel.RED, ColorModel.YELLOW,ColorModel.ORANGE},
        new object[] {ColorModel.YELLOW, ColorModel.RED,ColorModel.ORANGE},
        new object[] {ColorModel.RED, ColorModel.BLUE,ColorModel.PURPLE},
        new object[] {ColorModel.BLUE, ColorModel.RED,ColorModel.PURPLE},
        new object[] {ColorModel.YELLOW, ColorModel.BLUE,ColorModel.GREEN},
        new object[] {ColorModel.BLUE, ColorModel.YELLOW,ColorModel.GREEN},
    };

	[Test, TestCaseSource("Colors")]
	public void CombineColorTest(Color firstColor, Color secondColor, Color expectedColor){
        AbstractHalfBlockColor first = new HalfBlockColor(firstColor);
        AbstractHalfBlockColor second = new HalfBlockColor(secondColor);
        AbstractHalfBlockColor expected = new HalfBlockColor(expectedColor);
		AbstractHalfBlockColor result = first.CombineColor (second);
		Assert.True (expected.Equals(result));
	}

   
}
