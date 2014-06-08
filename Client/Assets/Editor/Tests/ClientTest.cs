using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Moq;

[TestFixture]
public class ClientTest
{

    private Client _client;
    private Mock<INetwork> _network;
    private Mock<INetworkView> _networkView;
    private GameObject block;

    [SetUp]
    public void SetUp()
    {
        _client = new Client();
        _network = new Mock<INetwork>();
        _networkView = new Mock<INetworkView>();
        _client.networkView = _networkView.Object;
        _client.network = _network.Object;
        block = Resources.Load("TestCube") as GameObject;
    }

    [Test]
    public void ConnectToServerTest1()
    {
        _network.Setup(net => net.Connect(It.IsAny<string>(), It.IsAny<int>())).Returns(NetworkConnectionError.ConnectionFailed);
        NetworkConnectionError error = _client.ConnectToServer("127.0.0.1", 3830);
        _network.Verify(net => net.Connect(It.IsAny<string>(), It.IsAny<int>()));
        Assert.AreEqual(error, NetworkConnectionError.ConnectionFailed);
    }

    [Test]
    public void ConnectToServerTest2()
    {
        _network.Setup(net => net.Connect("127.0.0.1", 3830)).Returns(NetworkConnectionError.NoError);
        NetworkConnectionError error = _client.ConnectToServer("127.0.0.1", 3830);
        _network.Verify(net => net.Connect("127.0.0.1", 3830));
        Assert.AreEqual(error, NetworkConnectionError.NoError);
    }

    [Test]
    public void DestroyAllBlocksTest()
    {
        GameObject instantiation = GameObject.Instantiate(block) as GameObject;
        instantiation.name = "TestCube";
        instantiation.tag = "block";
        Assert.IsNotNull(GameObject.Find("TestCube"));
        _client.DestroyAllBlocks();
        Assert.IsNull(GameObject.Find("TestCube"));
    }

    /*[Test]
    public void ColorBlockTest()
    {
        Vector3 color = new Vector3((float)0.12, (float)0.13, (float)0.14);

        _networkView.Setup(netV => netV.Find(block.networkView.viewID)).Returns(_networkView.Object);
        _networkView.Setup(netV => netV.gameObject()).Returns(block);

        _client.ColorBlock(block.networkView.viewID, color);
        Assert.AreEqual(block.renderer.sharedMaterial.color, new Color(color.x, color.y, color.z));
    }*/
    
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
