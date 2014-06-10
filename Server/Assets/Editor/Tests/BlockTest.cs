using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Moq;

[TestFixture]
public class BlockTest {

	Block block = new Block();


	[TestCase(1,5, Result=3)]
	public int MixWithColorXBandTest(int a, int b) {
		Vector3 vector1 = new Vector3 (a, a, a);
		Vector3 vector2 = new Vector3 (b, b, b);
		block.SetColor (vector1);
		block.MixWithColor (vector2);

		return (int)block.GetColor ().x;
	}

}
