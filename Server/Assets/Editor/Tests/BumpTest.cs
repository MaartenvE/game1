using UnityEngine;
using NUnit.Framework;
using Moq;
using BuildingBlocks;


[TestFixture]
public class BumpTest
{
    private INetworkPlayer sender = new NetworkPlayerWrapper(new NetworkPlayer());
	private Bump target;

	[SetUp]
	public void BumpSetup()
	{
		INetworkPlayer sender = new NetworkPlayerWrapper(new NetworkPlayer());
		target = new Bump(23.86, 8.95f, sender);
	}

    [Test]
    public void NetworkPlayerTest()
    {
       Assert.AreEqual(new NetworkPlayerWrapper(new NetworkPlayer()), new NetworkPlayerWrapper(new NetworkPlayer()));
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
