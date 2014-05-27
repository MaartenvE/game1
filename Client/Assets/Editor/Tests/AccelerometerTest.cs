using UnityEngine;
using NUnit.Framework;
using Moq;
using System;
using System.Collections;

[TestFixture]
public class AccelerometerTest
{
	Mock<IAccelerometerInput> inputMock;
	IAccelerometer accelerometer;

	[SetUp]
	public void AccelerometerSetup()
	{
		inputMock = new Mock<IAccelerometerInput>();
		accelerometer = new Accelerometer(inputMock.Object);
	}

    bool _runTest(Vector3[] inputs, Func<bool> action)
    {
        Vector3 current = Vector3.zero;
        inputMock.SetupGet(acc => acc.Acceleration).Returns(() => current);

        bool result = false;
        foreach (Vector3 value in inputs)
        {
            current = value;
            result = action();
            accelerometer.Update();
        }

        return result;
    }

	[TestCaseSource("AccelerationTestCases")]
	public bool AccelerationTest(Vector3[] inputs)
	{
        return _runTest(inputs, accelerometer.IsAccelerating);
	}

	[TestCaseSource("DecelerationTestCases")]
	public bool DecelerationTest(Vector3[] inputs)
	{
        return _runTest(inputs, accelerometer.IsDecelerating);
	}

	[TestCaseSource("StationaryTestCases")]
	public bool StationaryTest(Vector3[] inputs)
	{
        return _runTest(inputs, accelerometer.IsStationary);
	}

	[TestCaseSource("UprightTestCases")]
	public bool UprightTest(Vector3[] inputs)
	{
        return _runTest(inputs, accelerometer.IsUpright);
	}

    static Vector3 zero = new Vector3(0.0f, -1.0f, 0.0f);
    static Vector3 back = new Vector3(0.0f, -1.0f, -0.5f);
    static Vector3 forward = new Vector3(0.0f, -1.0f, 0.5f);
    static Vector3 up = new Vector3(0.0f, -1.5f, 0.0f);
    static Vector3 down = new Vector3(0.0f, 0.5f, 0.0f);
    static Vector3 left = new Vector3(-0.5f, -1.0f, 0.0f);
    static Vector3 right = new Vector3(0.5f, -1.0f, 0.0f);

    static Vector3[] Z = new[] { zero };
    static Vector3[] B = new[] { back };
    static Vector3[] F = new[] { forward };
    static Vector3[] U = new[] { up };
    static Vector3[] D = new[] { down };
    static Vector3[] L = new[] { left };
    static Vector3[] R = new[] { right };

    static Vector3[] BB = new[] { back, back };
    static Vector3[] FF = new[] { forward, forward };
    static Vector3[] FZ = new[] { forward, zero };
    static Vector3[] ZF = new[] { zero, forward };
    static Vector3[] FB = new[] { forward, back };
    static Vector3[] BF = new[] { back, forward };

    static Vector3[] BZB = new[] { back, zero, back };
    static Vector3[] ZBZ = new[] { zero, back, zero };


    TestCaseData[] AccelerationTestCases = {
        new TestCaseData(Z).SetName("Acceleration: Zero").Returns(false),
        new TestCaseData(B).SetName("Acceleration: Back").Returns(true),
        new TestCaseData(F).SetName("Acceleration: Forward").Returns(false),
        new TestCaseData(U).SetName("Acceleration: Up").Returns(false),
        new TestCaseData(D).SetName("Acceleration: Down").Returns(false),
        new TestCaseData(L).SetName("Acceleration: Left").Returns(false),
        new TestCaseData(R).SetName("Acceleration: Right").Returns(false),

        new TestCaseData(BB).SetName("Acceleration: Back, Back").Returns(false),
        new TestCaseData(FF).SetName("Acceleration: Forward, Forward").Returns(false),
        new TestCaseData(FZ).SetName("Acceleration: Forward, Zero").Returns(true),
        new TestCaseData(ZF).SetName("Acceleration: Zero, Forward").Returns(false),
        new TestCaseData(FB).SetName("Acceleration: Forward, Back").Returns(true),
        new TestCaseData(BF).SetName("Acceleration: Back, Forward").Returns(false),

        new TestCaseData(BZB).SetName("Acceleration: Back, Zero, Back").Returns(true),
        new TestCaseData(ZBZ).SetName("Acceleration: Zero, Back, Zero").Returns(false),
    };


    TestCaseData[] DecelerationTestCases = {
        new TestCaseData(Z).SetName("Deceleration: Zero").Returns(false),
        new TestCaseData(B).SetName("Deceleration: Back").Returns(false),
        new TestCaseData(F).SetName("Deceleration: Forward").Returns(true),
        new TestCaseData(U).SetName("Deceleration: Up").Returns(false),
        new TestCaseData(D).SetName("Deceleration: Down").Returns(false),
        new TestCaseData(L).SetName("Deceleration: Left").Returns(false),
        new TestCaseData(R).SetName("Deceleration: Right").Returns(false),

        new TestCaseData(BB).SetName("Deceleration: Back, Back").Returns(false),
        new TestCaseData(FF).SetName("Deceleration: Forward, Forward").Returns(false),
        new TestCaseData(FZ).SetName("Deceleration: Forward, Zero").Returns(false),
        new TestCaseData(ZF).SetName("Deceleration: Zero, Forward").Returns(true),
        new TestCaseData(FB).SetName("Deceleration: Forward, Back").Returns(false),
        new TestCaseData(BF).SetName("Deceleration: Back, Forward").Returns(true),

        new TestCaseData(BZB).SetName("Deceleration: Back, Zero, Back").Returns(false),
        new TestCaseData(ZBZ).SetName("Deceleration: Zero, Back, Zero").Returns(true),
    };

    TestCaseData[] StationaryTestCases = {
        new TestCaseData(Z).SetName("Stationary: Zero").Returns(true),
        new TestCaseData(B).SetName("Stationary: Back").Returns(false),
        new TestCaseData(F).SetName("Stationary: Forward").Returns(false),
        new TestCaseData(U).SetName("Stationary: Up").Returns(false),
        new TestCaseData(D).SetName("Stationary: Down").Returns(false),
        new TestCaseData(L).SetName("Stationary: Left").Returns(false),
        new TestCaseData(R).SetName("Stationary: Right").Returns(false),

        new TestCaseData(BB).SetName("Stationary: Back, Back").Returns(false),
        new TestCaseData(FF).SetName("Stationary: Forward, Forward").Returns(false),
        new TestCaseData(FZ).SetName("Stationary: Forward, Zero").Returns(true),
        new TestCaseData(ZF).SetName("Stationary: Zero, Forward").Returns(false),
        new TestCaseData(FB).SetName("Stationary: Forward, Back").Returns(false),
        new TestCaseData(BF).SetName("Stationary: Back, Forward").Returns(false),

        new TestCaseData(BZB).SetName("Stationary: Back, Zero, Back").Returns(false),
        new TestCaseData(ZBZ).SetName("Stationary: Zero, Back, Zero").Returns(true),
    };

    TestCaseData[] UprightTestCases = {
        new TestCaseData(Z).SetName("Upright: Zero").Returns(true),
        new TestCaseData(B).SetName("Upright: Back").Returns(true),
        new TestCaseData(F).SetName("Upright: Forward").Returns(true),
        new TestCaseData(U).SetName("Upright: Up").Returns(false),
        new TestCaseData(D).SetName("Upright: Down").Returns(false),
        new TestCaseData(L).SetName("Upright: Left").Returns(true),
        new TestCaseData(R).SetName("Upright: Right").Returns(true),

        new TestCaseData(BB).SetName("Upright: Back, Back").Returns(true),
        new TestCaseData(FF).SetName("Upright: Forward, Forward").Returns(true),
        new TestCaseData(FZ).SetName("Upright: Forward, Zero").Returns(true),
        new TestCaseData(ZF).SetName("Upright: Zero, Forward").Returns(true),
        new TestCaseData(FB).SetName("Upright: Forward, Back").Returns(true),
        new TestCaseData(BF).SetName("Upright: Back, Forward").Returns(true),

        new TestCaseData(BZB).SetName("Upright: Back, Zero, Back").Returns(true),
        new TestCaseData(ZBZ).SetName("Upright: Zero, Back, Zero").Returns(true),
    };
}
