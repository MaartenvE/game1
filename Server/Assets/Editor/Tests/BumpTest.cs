using UnityEngine;
using NUnit.Framework;
using Moq;


[TestFixture]
public class BumpTest {

	//private Mock <NetworkPlayer> sender = new Mock <NetworkPlayer> ();
	//private NetworkPlayer sender = new NetworkPlayer();
	NetworkPlayer sender = new NetworkPlayer();
	Bump target;
	[SetUp]
	public void BumpSetup()
	{
		NetworkPlayer sender = new NetworkPlayer();
		target = new Bump(23.86, 8.95f,sender);
	}
	[Test]
	public void BumpTimeTest()
	{
		Assert.AreEqual (23.86, target.Time);
	}

	[Test]
	public void BumpForceTest()
	{
		Assert.AreEqual (8.95f, target.Force);
	}

	[Test]
	public void BumpSenderTest()
	{
		Assert.AreEqual (sender, target.Sender);
	}


}
