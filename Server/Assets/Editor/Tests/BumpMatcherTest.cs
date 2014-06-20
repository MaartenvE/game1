using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using BuildingBlocks;

[TestFixture]
public class BumpMatcherTest
{
	[TestCaseSource ("BumpTests")] 
	public bool MatchBumpTest(Bump[] bumps)
	{
		BumpMatcher bumpMatcher = new BumpMatcher();
		bool result = false;
		bumpMatcher.OnBumpMatch += (Bump bump1, Bump bump2) => result = true;
		foreach (Bump bump in bumps)
		{
			bumpMatcher.Add(bump);
		}
		return result;
	}

    public static IEnumerable BumpTests
    {
        get
        {
            var mock1 = new Mock<INetworkPlayer>();
            var mock2 = new Mock<INetworkPlayer>();
            var mock3 = new Mock<INetworkPlayer>();

            var sender1 = mock1.Object;
            var sender2 = mock2.Object;
            var sender3 = mock3.Object;

            mock1.Setup(p => p.Equals(sender1)).Returns(true);
            mock2.Setup(p => p.Equals(sender2)).Returns(true);
            mock3.Setup(p => p.Equals(sender3)).Returns(true);

            Bump bump1 = new Bump(Network.time, 8.56f, sender1);
            Bump bump2 = new Bump(Network.time, 10.68f, sender2);
            Bump bump3 = new Bump(Network.time, 9.02f, sender1);
            Bump bump4 = new Bump(Network.time, 9.02f, sender2);
            Bump bump5 = new Bump(Network.time + 2.0f, 8.56f, sender2);
            Bump bump6 = new Bump(Network.time - 5.0f, 8.56f, sender1);
            Bump bump7 = new Bump(Network.time - 5.0f, 8.56f, sender2);
            Bump bump8 = new Bump(Network.time - 0.08f, 9.02f, sender2);
            Bump bump9 = new Bump(Network.time + 0.08f, 9.02f, sender3);

            yield return new TestCaseData(new Bump[] { bump1, bump2 }).Returns(true).SetName("One Bump Match");
            yield return new TestCaseData(new Bump[] { bump1 }).Returns(false).SetName("One Bump");
		    yield return new TestCaseData(new Bump[] { bump1, bump3 }).Returns(false).SetName("Same sender");
		    yield return new TestCaseData(new Bump[] { bump1, bump2, bump4 }).Returns(true).SetName("Three bumps, two matches test");
            yield return new TestCaseData(new Bump[] { bump1, bump5 }).Returns(false).SetName("Exceeds time");
            yield return new TestCaseData(new Bump[] { bump6, bump7 }).Returns(false).SetName("Outdated bump");
            yield return new TestCaseData(new Bump[] { bump8, bump9, bump1 }).Returns(false).SetName("Multiple matches");
        }
    }
}