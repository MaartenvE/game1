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

	static object[] Sides = {
		new object[] { new Vector3 (-0.5f, 0, 0), new Vector3 (-1, 0, 0)},
		new object[] { new Vector3 (0.5f, 0, 0), new Vector3 (1, 0, 0)},
		new object[] { new Vector3 (0, -0.5f, 0), new Vector3 (0, -1, 0)},
		new object[] { new Vector3 (0, 0.5f, 0), new Vector3 (0, 1, 0)},
		new object[] { new Vector3 (0, 0, -0.5f), new Vector3 (0, 0, -1)},
		new object[] { new Vector3 (0, 0, 0.5f), new Vector3 (0, 0, 1)}
	};

	[Test, TestCaseSource("Sides")]
	public void CalculateSideTest(Vector3 hit, Vector3 result)
    {
        Assert.AreEqual(TouchBehaviour.CalculateSide(testObject.transform, hit), result);
    }

}
