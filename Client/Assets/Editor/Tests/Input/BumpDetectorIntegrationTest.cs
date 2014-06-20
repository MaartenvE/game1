using UnityEngine;
using NUnit.Framework;
using Moq;

namespace BuildingBlocks.Input
{
    [TestFixture]
    class BumpDetectorIntegrationTest
    {
        private Vector3 currentAcceleration;
        private Vector3 currentMagnetisation;

        private Mock<IAccelerometerInput> accelerometerInput;
        private Mock<IMagnetometerInput> magnetometerInput;

        [TestFixtureSetUp]
        public void Setup()
        {
            accelerometerInput = new Mock<IAccelerometerInput>();
            magnetometerInput = new Mock<IMagnetometerInput>();

            accelerometerInput.SetupGet(acc => acc.Acceleration).Returns(() => currentAcceleration);
            magnetometerInput.SetupGet(mag => mag.Magnetisation).Returns(() => currentMagnetisation);
        }

        private bool _runTest(Vector3[][] inputs)
        {
            bool detected = false;

            BumpDetector bumpDetector = new BumpDetector(new Accelerometer(accelerometerInput.Object),
                                                         new Magnetometer(magnetometerInput.Object));
            bumpDetector.OnBump += (bump) => detected = true;

            foreach (Vector3[] input in inputs)
            {
                currentAcceleration = input[0];
                currentMagnetisation = input[1];
                bumpDetector.DetectBump();
            }

            return detected;
        }

        [TestCaseSource("MagnetisationTestData")]
        public bool BumpTestWithMagnetisation(Vector3[] inputs)
        {
            Vector3[][] combined = new Vector3[inputs.Length][];
            for (int i = 0; i < inputs.Length; i++)
            {
                combined[i] = new Vector3[] { inputs[i], new Vector3((float)(i % 2) * 50, (float)(i % 2) * 50, (float)(i % 2) * 50) };
            }

            return _runTest(combined);
        }

        [TestCaseSource("NoMagnetisationTestData")]
        public bool BumpTestWithoutMagnetisation(Vector3[] inputs)
        {
            Vector3[][] combined = new Vector3[inputs.Length][];
            for (int i = 0; i < inputs.Length; i++)
            {
                combined[i] = new Vector3[] { inputs[i], Vector3.zero };
            }

            return _runTest(combined);
        }

        #region TestCases
        //---------------- STANDARD ACCELEROMETER FORCES ----------------//
        // These values are accelerometer forces, with gravity acting    //
        // along the Y-axis (phone is held upright).                     //
        //---------------------------------------------------------------//
        static Vector3 zero = new Vector3(0.0f, -1.0f, 0.0f);
        static Vector3 back = new Vector3(0.0f, -1.0f, -0.5f);
        static Vector3 forward = new Vector3(0.0f, -1.0f, 0.5f);
        static Vector3 up = new Vector3(0.0f, -1.5f, 0.0f);
        static Vector3 down = new Vector3(0.0f, 0.5f, 0.0f);
        static Vector3 left = new Vector3(-0.5f, -1.0f, 0.0f);
        static Vector3 right = new Vector3(0.5f, -1.0f, 0.0f);

        static Vector3 tiltedZero = new Vector3(0.0f, 0.0f, -1.0f);
        static Vector3 tiltedBack = new Vector3(0.0f, 0.0f, -1.5f);
        static Vector3 tiltedForward = new Vector3(0.0f, 0.0f, -0.5f);

        /// <summary>
        /// Test case data as an array of TestCaseData objects, containing an 
        /// array of Vector3 objects to be passed to 
        /// <see cref="BumpTestWithMagnetisation"/>, a unique name (without which Unity 
        /// would fail) and the expected return value.
        /// </summary>
        static TestCaseData[] MagnetisationTestData = {

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
			    .Returns(false),
		    new TestCaseData(new[] { tiltedBack, tiltedZero, tiltedForward, 
				    tiltedBack, tiltedZero, tiltedZero })
			    .SetName("TiltedBZFBZZ")
			    .Returns(false),

		    // Not bumping with a tilted phone
		    new TestCaseData(new[] { tiltedForward, tiltedZero, tiltedZero })
			    .SetName("TiltedFZZ")
			    .Returns(false),
		    new TestCaseData(new[] { tiltedBack, tiltedZero, tiltedForward, 
				    tiltedZero })
			    .SetName("TiltedBZFZ")
			    .Returns(false),
	    };



        static TestCaseData[] NoMagnetisationTestData = {

		    // Movement along Z-azis in wrong order.
		    new TestCaseData(new[] { zero })
			    .SetName("No magnetisation: Z")
			    .Returns(false),
		    new TestCaseData(new[] { back, zero })
			    .SetName("No magnetisation: BZ")
			    .Returns(false),
		    new TestCaseData(new[] { forward, zero })
			    .SetName("No magnetisation: FZ")
			    .Returns(false),
		    new TestCaseData(new[] { back, zero, back, zero })
			    .SetName("No magnetisation: BZBZ")
			    .Returns(false),
		    new TestCaseData(new[] { back, zero, forward, zero })
			    .SetName("No magnetisation: BZFZ")
			    .Returns(false),
		    new TestCaseData(new[] { zero, back, zero })
			    .SetName("No magnetisation: ZBZ")
			    .Returns(false),
		    new TestCaseData(new[] { back, forward, zero })
			    .SetName("No magnetisation: BFZ")
			    .Returns(false),
		    new TestCaseData(new[] { forward, zero, zero })
			    .SetName("No magnetisation: FZZ")
			    .Returns(false),

		    // Correct order of movements along Z-axis
		    new TestCaseData(new[] { back, zero, zero })
			    .SetName("No magnetisation: BZZ")
			    .Returns(false),
		    new TestCaseData(new[] { back, forward, zero, zero })
			    .SetName("No magnetisation: BFZZ")
			    .Returns(false),
		    new TestCaseData(new[] { back, zero, forward, back, zero, zero })
			    .SetName("No magnetisation: BZFBZZ")
			    .Returns(false),
            
            // Some movement along other axes
		    new TestCaseData(new[] { up })
			    .SetName("No magnetisation: U")
			    .Returns(false),
		    new TestCaseData(new[] { left })
			    .SetName("No magnetisation: L")
			    .Returns(false),
		    new TestCaseData(new[] { right, zero, zero })
			    .SetName("No magnetisation: RZZ")
			    .Returns(false),
            
            new TestCaseData(new[] { back, zero, down, zero })
			    .SetName("No magnetisation: BZDZ")
			    .Returns(false),

		    // Someone who doesn't know what they want, but end up bumping in 
		    // the direction of the Z-axis.
		    new TestCaseData(new[] { back, zero, up, left, down, right, back, 
				    forward, zero, zero })
			    .SetName("No magnetisation: BZULDRBFZZ")
			    .Returns(false),
		    new TestCaseData(new[] { back, up, up, zero, zero })
			    .SetName("No magnetisation: BUUZZ")
			    .Returns(false),

		    // Bumping with a tilted phone
		    new TestCaseData(new[] { tiltedBack, tiltedZero, tiltedZero })
			    .SetName("No magnetisation: TiltedBZZ")
			    .Returns(false),
		    new TestCaseData(new[] { tiltedBack, tiltedZero, tiltedForward, 
				    tiltedBack, tiltedZero, tiltedZero })
			    .SetName("No magnetisation: TiltedBZFBZZ")
			    .Returns(false),

		    // Not bumping with a tilted phone
		    new TestCaseData(new[] { tiltedForward, tiltedZero, tiltedZero })
			    .SetName("No magnetisation: TiltedFZZ")
			    .Returns(false),
		    new TestCaseData(new[] { tiltedBack, tiltedZero, tiltedForward, 
				    tiltedZero })
			    .SetName("No magnetisation: TiltedBZFZ")
			    .Returns(false),
	    };

        #endregion
    }
}
