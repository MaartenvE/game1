using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;

[TestFixture]
public class BumpMatcherTest
{
	static NetworkPlayer sender1 = new NetworkPlayer();
	static NetworkPlayer sender2 = Network.player;
	static NetworkPlayer sender3;

	static Bump bump1;
	static Bump bump2;
	static Bump bump3;
	static Bump bump4;
	static Bump bump5;
	static Bump bump6;

	/*
	Bump bump6 = new Bump();
	Bump bump7 = new Bump();*/

	[SetUp]
	public void BumpMatcherSetup()
	{
		bump1 = new Bump(Network.time, 8.56f, sender1);
		bump2 = new Bump(Network.time, 10.68f, sender2);
		bump3 = new Bump(Network.time, 9.02f, sender1);
		bump4 = new Bump(Network.time, 9.02f, sender2);
		bump5 = new Bump(Network.time + 0.1f, 9.02f, sender2);
		bump6 = new Bump ((Network.time + 2.0f), 8.56f, sender2);
	}

	[TestCaseSource ("BumpTests")]
	public bool BumpTester(Bump[] bumps)
	{
		BumpMatcher bumpMatcher = new BumpMatcher();
		bool result = false;
		bumpMatcher.OnBumpMatch += (Bump bump1, Bump bump2) => result = true;
		foreach (Bump bump in bumps) 
		{
			bumpMatcher.Add (bump);
		}
		return result;

	}

	TestCaseData[] BumpTests = {
		new TestCaseData(new Bump[] {bump1, bump2} ).Returns (true).SetName("One Bump Match"),
		new TestCaseData(new Bump[] {bump1}).Returns(false).SetName("One Bump"),
		new TestCaseData(new Bump[] {bump1,bump3} ).Returns (false).SetName("Same sender"),
		new TestCaseData(new Bump[] {bump1, bump2,bump4} ).Returns (true).SetName("Three bumps, two matches test"),
		new TestCaseData(new Bump[] {bump1, bump6}).Returns (false).SetName("Exceeds time"),

	};

}