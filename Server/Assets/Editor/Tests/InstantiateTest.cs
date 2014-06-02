using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Moq;

[TestFixture]
public class ServerTest {
	private Server testServer;
	private Mock<INetwork> network;
	private Mock<INetworkView> networkView;

	/*
	 * Setup for the tests by creating the appropiate mocks and setting up the server
	*/
	[SetUp]
	public void SetupServer(){
		testServer = new Server ();
		network = new Mock <INetwork> ();
		networkView = new Mock <INetworkView> ();
		testServer.network = network.Object;
		testServer.networkView = networkView.Object;
		testServer.LaunchServer ();
	}

	/**
	 * Test rather the server has made an InitializeServer call
	*/
	[Test]
	public void TestInitialization(){
		network.Verify (net => net.InitializeServer(It.IsAny<int> (), It.IsAny<int> (), It.IsAny<bool>()));
	}


	[Test]
	public void TestStart(){
		network.Setup(net => net.Instantiate(It.IsAny <UnityEngine.Object>(), It.IsAny <Vector3>(), It.IsAny<Quaternion> (), It.IsAny<int>())).Returns(Resources.Load("TestCube") as GameObject);
		testServer.Start ();

		Vector3 location = new Vector3 (0, 0, 0);
		network.Verify (net => net.Instantiate (It.IsAny <UnityEngine.Object>(), location, It.IsAny <Quaternion>(), It.IsAny<int> ()));
		networkView.Verify( netV => netV.RPC ("ColorBlock", RPCMode.AllBuffered, It.IsAny<object[]>()),Times.Once());
	}


	/**
	 *  Test rather the given colour is applied correctly.
	 */
	[Test]
	public void TestColour(){
		GameObject block = Resources.Load("TestCube") as GameObject;

		Vector3 color = new Vector3 ((float)0.12, (float)0.13, (float)0.14);

		networkView.Setup(netV => netV.Find(block.networkView.viewID)).Returns(networkView.Object);
		networkView.Setup(netV => netV.gameObject()).Returns(block);

		testServer.ColorBlock (block.networkView.viewID, color);
		Assert.AreEqual (block.renderer.sharedMaterial.color, new Color (color.x, color.y, color.z));
	}

	/**
	 * Test rather the Server has made the (network) Instantiate call using the given location.
	*/

	[Test]
	public void TestInstantiation(){
		GameObject block = Resources.Load("TestCube") as GameObject;
		GameObject sideBlock = Resources.Load("TestCube") as GameObject;

		network.Setup(net => net.Instantiate(It.IsAny <UnityEngine.Object>(), It.IsAny <Vector3>(), It.IsAny<Quaternion> (), It.IsAny<int>())).Returns(block);

		Mock<INetworkView> sideBlockNetworkView = new Mock <INetworkView> ();
		
		sideBlockNetworkView.Setup(netV => netV.gameObject()).Returns(sideBlock);

		networkView.Setup(netV => netV.Find(sideBlock.networkView.viewID)).Returns(sideBlockNetworkView.Object);


		Vector3 location = new Vector3 (1, 1, 1);
		Vector3 matrixLocation = new Vector3 (1, 1, 1);

		testServer.PlaceBlock (location, matrixLocation, sideBlock.networkView.viewID);

		network.Verify (net => net.Instantiate (It.IsAny <UnityEngine.Object>(), location, block.transform.rotation, It.IsAny<int>()));

		//Assert.AreEqual(block.GetComponent<location> ().index,sideBlock.GetComponent<location> ().index + matrixLocation);

	}

}
