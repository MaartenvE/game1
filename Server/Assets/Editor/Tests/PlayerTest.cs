using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Moq;

[TestFixture]
public class PlayerTest {
	private Mock<INetworkView> _NetworkView;
	private Mock<INetwork> _Network;
	private Mock<IInstantiatedBlock> _Finger;
	//private Mock<NetworkPlayer> _NetworkPlayer;

	private Player _TestPlayer;
	
	///
	/// Sets up the Player for the playertest
	///
	[SetUp]
	public void SetupPlayer(){
		_NetworkView = new Mock<INetworkView>();
		_Network = new Mock<INetwork> ();
		_Finger = new Mock<IInstantiatedBlock> ();

	//	_NetworkPlayer = new Mock<NetworkPlayer> ();

		_NetworkView.Setup (Mock => Mock.RPC ("GivePlayerAnError", It.IsAny<NetworkPlayer> (), It.IsAny<Object[]> ())).Verifiable();
		//_NetworkView.Setup (Mock => Mock.RPC ("UpdateFinger", It.IsAny<NetworkPlayer> (), It.IsAny<Object[]> ())).Verifiable();

		_TestPlayer = new Player (_NetworkView.Object, _Network.Object);
	}

	///
	/// tests the giveInventoryBlock function 
	///
	[Test]
	public void GiveInventoryBlockTest(){
		Assert.Fail ("not yet implemented");
	}

	///
	/// tests the canPlaceBlock function 
	///
	[Test]
	public void CanPlaceBlockTest(){
		Assert.Fail ("not yet implemented");
	}

	///
	/// tests the GiveError function 
	///
	[Test]
	public void GivePlayerAnError(){
		string errorMessage = "this is an error message";

		_TestPlayer.GivePlayerAnError(errorMessage);

		_NetworkView.Verify(netV => netV.RPC ("GivePlayerAnError", It.IsAny<NetworkPlayer>(), It.IsAny<object[]>()));
	}

}
