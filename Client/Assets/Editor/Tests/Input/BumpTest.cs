using UnityEngine;
using NUnit.Framework;

namespace BuildingBlocks.Input
{
    [TestFixture]
    class BumpTest
    {
        float time;
        Bump bump;

        [SetUp]
        public void SetupBump()
        {
            time = Time.time;
            bump = new Bump(time - 1.0f, time, 0.5f);
        }

        [Test]
        public void TestStartTime()
        {
            Assert.AreEqual(time - 1.0f, bump.StartTime);
        }

        [Test]
        public void TestEndTime()
        {
            Assert.AreEqual(time, bump.EndTime);
        }

        [Test]
        public void TestForce()
        {
            Assert.AreEqual(0.5f, bump.Force);
        }
    }
}
