using UnityEngine;
using NUnit.Framework;
using Moq;

[TestFixture]
public class TouchBehaviourNetworkTest {

    private Mock<INetworkView> networkView;
    private TouchBehaviour touchBehaviour;
    private GameObject gameObject;

	[SetUp]
    public void SetUp()
    {
        networkView = new Mock<INetworkView>();
        touchBehaviour = new TouchBehaviour();
        touchBehaviour.networkView = networkView.Object;
        gameObject = Resources.Load("TestCube") as GameObject;
        gameObject.tag = "testObject";
    }

    [Test]
    public void PlaceSquareAtFingerTest()
    {
        touchBehaviour.PlaceSquareAtFinger(new Vector3(0, 0, 0), new Vector3(0, 0, 0), gameObject.networkView.viewID);
        networkView.Verify(netV => netV.RPC("PlaceBlock", RPCMode.Server, new Vector3(0, 0, 0), new Vector3(0, 0, 0), gameObject.networkView.viewID));
    }

    [Test]
    public void RemovePickedObjectTest()
    {
        touchBehaviour.RemovePickedObject(gameObject.networkView.viewID);
        networkView.Verify(netV => netV.RPC("RemoveBlock", RPCMode.Server, gameObject.networkView.viewID));
        Assert.AreEqual(GameObject.FindGameObjectsWithTag("testObject").Length, 0);
    }

}
