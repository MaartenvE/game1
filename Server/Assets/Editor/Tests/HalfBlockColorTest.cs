using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Moq;

[TestFixture]
public class HalfBlockColorTest {
	private AbstractHalfBlockColor _green;
	private AbstractHalfBlockColor _yellow;
	private AbstractHalfBlockColor _blue;

	[SetUp]
	public void CreateHalfBlocksColors(){
		_green = new HalfBlockColor ("green");
		_yellow = new HalfBlockColor ("yellow");
		_blue = new HalfBlockColor ("blue");
	}

	[Test]
	public void GetColorNameTest(){
		Assert.AreEqual ("green", _green.color);
	}

	[Test]
	public void EqualsTest(){
		AbstractHalfBlockColor _otherGreen = new HalfBlockColor ("green");
		Assert.True (_green.Equals(_otherGreen));
	}

	[Test]
	public void StringEqualsTest(){
		Assert.True ("green".Equals("green"));
	}

	[Test]
	public void CombineColorTest(){
		AbstractHalfBlockColor result = _yellow.CombineColor (_blue);
		Assert.True (_green.Equals( result));
	}
}
