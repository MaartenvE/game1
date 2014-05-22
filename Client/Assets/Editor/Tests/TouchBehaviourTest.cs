using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Moq;

[TestFixture]
public class TouchBehaviourTest {
    
    private GameObject testObject;
    private Vector3 hit;

    [SetUp]
    public void SetUp()
    {
        testObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        testObject.transform.position = new Vector3(0, 0, 0);
    }

    [Test]
    public void CalculateSideTestLeft()
    {
        hit = new Vector3(-0.5f, 0, 0);
        Assert.AreEqual(TouchBehaviour.CalculateSide(testObject.transform, hit), new Vector3(-1, 0, 0));
    }

    [Test]
    public void CalculateSideTestRight()
    {
        hit = new Vector3(0.5f, 0, 0);
        Assert.AreEqual(TouchBehaviour.CalculateSide(testObject.transform, hit), new Vector3(1, 0, 0));
    }

    [Test]
    public void CalculateSideTestAbove()
    {
        hit = new Vector3(0, 0.5f, 0);
        Assert.AreEqual(TouchBehaviour.CalculateSide(testObject.transform, hit), new Vector3(0, 1, 0));
    }

    [Test]
    public void CalculateSideTestBelow()
    {
        hit = new Vector3(0, -0.5f, 0);
        Assert.AreEqual(TouchBehaviour.CalculateSide(testObject.transform, hit), new Vector3(0, -1, 0));
    }

    [Test]
    public void CalculateSideTestFront()
    {
        hit = new Vector3(0, 0, 0.5f);
        Assert.AreEqual(TouchBehaviour.CalculateSide(testObject.transform, hit), new Vector3(0, 0, 1));
    }

    [Test]
    public void CalculateSideTestBehind()
    {
        hit = new Vector3(0, 0, -0.5f);
        Assert.AreEqual(TouchBehaviour.CalculateSide(testObject.transform, hit), new Vector3(0, 0, -1));
    }


}
