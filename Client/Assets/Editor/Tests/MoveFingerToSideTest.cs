/*using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Moq;

[TestFixture]
public class MoveFingerToSideTest
{
    private Mock<IRaycastHit> raycastHit;
    private TouchBehaviour touchBehaviour;

    [SetUp]
    public void SetupTest()
    {
        raycastHit = new Mock<IRaycastHit>();
        touchBehaviour = new TouchBehaviour();
        touchBehaviour.cubeFinger = GameObject.CreatePrimitive(PrimitiveType.Cube);
        touchBehaviour.cubeFinger.AddComponent<MatrixLocation>();
        touchBehaviour.cubeFinger.tag = "testObject";
    }

    [Test]
    public void MoveFingerToSideTest1()
    {
        GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        gameObject.tag = "testObject";
        gameObject.AddComponent <MatrixLocation>();
        gameObject.transform.position = new Vector3(0, 0, 0);
        raycastHit.Setup(ray => ray.transform()).Returns(gameObject.transform);
        raycastHit.Setup(ray => ray.point()).Returns(new Vector3(0.5f, 0, 0));

        touchBehaviour.MoveFingerToSide(touchBehaviour.cubeFinger.transform, raycastHit.Object);

        raycastHit.Verify(ray => ray.transform());
        raycastHit.Verify(ray => ray.point());

        Assert.AreEqual(touchBehaviour.cubeFinger.transform.position, new Vector3(1, 0, 0));
        Assert.AreEqual(touchBehaviour.cubeFinger.GetComponent<MatrixLocation>().index, new Vector3(1, 0, 0));

    }

    [TearDown]
    public void CleanUp()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("testObject");
        foreach(GameObject g in objects){
            GameObject.DestroyImmediate(g);
        }
    }
    
}

*/