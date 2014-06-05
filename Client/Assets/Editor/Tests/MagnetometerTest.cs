using UnityEngine;
using NUnit.Framework;
using Moq;
using System;
using System.Collections;

[TestFixture]
public class MagnetometerTest
{
    Mock<IMagnetometerInput> inputMock;
    IMagnetometer magnetometer;

    [TestFixtureSetUp]
    public void AccelerometerSetup()
    {
        inputMock = new Mock<IMagnetometerInput>();
        magnetometer = new Magnetometer(inputMock.Object);
    }

    [TestCaseSource("MagnetometerTestData")]
    public bool ChangingTest(Vector3 nextInput)
    {
        inputMock.SetupGet(mag => mag.Magnetisation).Returns(nextInput);

        bool result = magnetometer.IsChanging();
        magnetometer.Update();
        return result;
    }

    static Vector3 twenty = new Vector3(20.0f, 20.0f, 20.0f);
    static Vector3 thirty = new Vector3(30.0f, 30.0f, 30.0f);

    static IEnumerable MagnetometerTestData
    {
        get
        {
            yield return new TestCaseData(twenty).SetName("Empty").Returns(false);
            yield return new TestCaseData(twenty).SetName("No change").Returns(false);
            yield return new TestCaseData(twenty).SetName("Still no change").Returns(false);
            yield return new TestCaseData(thirty).SetName("Change will occur only after update").Returns(false);
            yield return new TestCaseData(twenty).SetName("Change in history").Returns(true);

            for (int i = 1; i < 20; i++) {
                yield return new TestCaseData(twenty).SetName("Dequeue " + i).Returns(true);
            }

            yield return new TestCaseData(twenty).SetName("Change removed from queue").Returns(false);
        }
    }   
}

