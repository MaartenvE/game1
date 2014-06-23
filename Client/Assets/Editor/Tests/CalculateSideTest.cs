/*using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Moq;

[TestFixture]
public class TouchBehaviourTest {
    
    private GameObject testObject;
    private Vector3 hit;
    private TouchBehaviour touchBehaviour;
    
    [SetUp]
    public void SetUp()
    {
        touchBehaviour = new TouchBehaviour();
        testObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        testObject.tag = "testObject";
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
        Assert.AreEqual(touchBehaviour.CalculateSide(testObject.transform, hit), result);
    }

    [TearDown]
    public void CleanUp()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("testObject");
        foreach (GameObject g in objects)
        {
            GameObject.DestroyImmediate(g);
        }
    }

}
*/