using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using UnityEngine;

namespace BuildingBlocksTest
{
	[TestFixture]
	public class BumpDetectorTest
	{
		/// <summary>
		/// Runs a series of bump detection tests on sequences of accelerometer 
		/// readings, defined in the <see cref="TestCases"/> object.
		/// </summary>
		/// <returns><c>true</c>, if a bump was detected, 
		/// <c>false</c> otherwise.</returns>
		/// <param name="inputs">An array containing Vector3 objects. These 
		/// objects will be passed as accelerometer readings to each update of 
		/// the <see cref="BumpDetector"/>.</param>
		[TestCaseSource("TestCases")]
		public bool RunBumpTestCase(Vector3[] inputs)
		{
			var fired  = false;
			var values = new Queue<Vector3>(inputs);
			var currentValue = Vector3.zero;

			var mock = new Mock<IAccelerometer>();
			mock.SetupGet(acc => acc.Acceleration).Returns(() => currentValue);

			var detector = new BumpDetector(mock.Object);
			detector.OnBump += (bump) => {
				fired = true;
			};

			while (values.Count > 0) {
				currentValue = values.Dequeue();
				detector.DetectBump();
			}

			return fired;
		}

		#region TestCases
		//---------------- STANDARD ACCELEROMETER FORCES ----------------//
		// These values are accelerometer forces, with gravity acting    //
		// along the Y-axis (phone is held upright).                     //
		//---------------------------------------------------------------//
		static Vector3 zero    = new Vector3(0.0f, -1.0f, 0.0f);
		static Vector3 back    = new Vector3(0.0f, -1.0f, -0.5f);
		static Vector3 forward = new Vector3(0.0f, -1.0f, 0.5f);
		static Vector3 up      = new Vector3(0.0f, -1.5f, 0.0f);
		static Vector3 down    = new Vector3(0.0f, 0.5f, 0.0f);
		static Vector3 left    = new Vector3(-0.5f, -1.0f, 0.0f);
		static Vector3 right   = new Vector3(0.5f, -1.0f, 0.0f);

		static Vector3 tiltedZero    = new Vector3(0.0f, 0.0f, -1.0f);
		static Vector3 tiltedBack    = new Vector3(0.0f, 0.0f, -1.5f);
		static Vector3 tiltedForward = new Vector3(0.0f, 0.0f, -0.5f);

		/// <summary>
		/// Test case data as an array of TestCaseData objects, containing an 
		/// array of Vector3 objects to be passed to 
		/// <see cref="RunBumpTestCase"/>, a unique name (without which Unity 
		/// would fail) and the expected return value.
		/// </summary>
		static TestCaseData[] TestCases = {

			// Movement along Z-azis in wrong order.
			new TestCaseData(new[] { zero })
				.SetName("Z")
				.Returns(false),
			new TestCaseData(new[] { back, zero })
				.SetName("BZ")
				.Returns(false),
			new TestCaseData(new[] { forward, zero })
				.SetName("FZ")
				.Returns(false),
			new TestCaseData(new[] { back, zero, back, zero })
				.SetName("BZBZ")
				.Returns(false),
			new TestCaseData(new[] { back, zero, forward, zero })
				.SetName("BZFZ")
				.Returns(false),
			new TestCaseData(new[] { zero, back, zero })
				.SetName("ZBZ")
				.Returns(false),
			new TestCaseData(new[] { back, forward, zero })
				.SetName("BFZ")
				.Returns(false),
			new TestCaseData(new[] { forward, zero, zero })
				.SetName("FZZ")
				.Returns(false),

			// Correct order of movements along Z-axis
			new TestCaseData(new[] { back, zero, zero })
				.SetName("BZZ")
				.Returns(true),
			new TestCaseData(new[] { back, forward, zero, zero })
				.SetName("BFZZ")
				.Returns(true),
			new TestCaseData(new[] { back, zero, forward, back, zero, zero })
				.SetName("BZFBZZ")
				.Returns(true),

			// Some movement along other axes
			new TestCaseData(new[] { up })
				.SetName("U")
				.Returns(false),
			new TestCaseData(new[] { left })
				.SetName("L")
				.Returns(false),
			new TestCaseData(new[] { right, zero, zero })
				.SetName("RZZ")
				.Returns(false),

			new TestCaseData(new[] { back, zero, down, zero })
				.SetName("BZDZ")
				.Returns(true),

			// Someone who doesn't know what they want, but end up bumping in 
			// the direction of the Z-axis.
			new TestCaseData(new[] { back, zero, up, left, down, right, back, 
					forward, zero, zero })
				.SetName("BZULDRBFZZ")
				.Returns(true),
			new TestCaseData(new[] { back, up, up, zero, zero })
				.SetName("BUUZZ")
				.Returns(true),

			// Bumping with a tilted phone
			new TestCaseData(new[] { tiltedBack, tiltedZero, tiltedZero })
				.SetName("TiltedBZZ")
				.Returns(true),
			new TestCaseData(new[] { tiltedBack, tiltedZero, tiltedForward, 
					tiltedBack, tiltedZero, tiltedZero })
				.SetName("TiltedBZFBZZ")
				.Returns(true),

			// Not bumping with a tilted phone
			new TestCaseData(new[] { tiltedForward, tiltedZero, tiltedZero })
				.SetName("TiltedFZZ")
				.Returns(false),
			new TestCaseData(new[] { tiltedBack, tiltedZero, tiltedForward, 
					tiltedZero })
				.SetName("TiltedBZFZ")
				.Returns(false),

		};
		#endregion
	}
}