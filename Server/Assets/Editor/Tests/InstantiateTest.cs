using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Moq;

[TestFixture]
public class ServerTest {
	private Server testServer;
	private Mock<INetwork> network;

	/*
	 * Setup for the tests by creating the appropiate mocks and setting up the server
	*/
	[SetUp]
	public void SetupServer(){
		testServer = new Server();
		network = new Mock <INetwork> ();
		testServer.network = network.Object;
		testServer.prefab = new GameObject();
		testServer.LaunchServer ();
	}

	/**
	 * Test rather the server has made an InitializeServer call
	*/
	[Test]
	public void TestInitialization(){
		network.Verify (net => net.InitializeServer(It.IsAny<int> (), It.IsAny<int> (), It.IsAny<bool>()));
	}

	/**
	 * Test rather the Server has made an (network) Instantiate call using the given location.
	*/
	[Test]
	public void TestInstantiation(){
		Vector3 location = new Vector3 (1, 1, 1);
		testServer.PlaceBlock (location);
		network.Verify (net => net.Instantiate (It.IsAny <UnityEngine.Object>(), location, It.IsAny <Quaternion>(), It.IsAny<int> ()));
	}
}
