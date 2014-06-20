using Moq;
using NUnit.Framework;
using UnityEngine;
using System.Threading;

namespace BuildingBlocks.Input
{
    [TestFixture]
    public class BumpDetectorTest
    {
        private BumpDetector bumpDetector;
        private Mock<IAccelerometer> accelerometer;
        private Mock<IMagnetometer> magnetometer;
        private bool isBumpDetected;

        /// <summary>
        /// Start with a clean slate for every test by creating new objects and mocks.
        /// </summary>
        [SetUp]
        public void SetupTest()
        {
            accelerometer = new Mock<IAccelerometer>();
            magnetometer = new Mock<IMagnetometer>();

            isBumpDetected = false;
            bumpDetector = new BumpDetector(accelerometer.Object, magnetometer.Object);
            bumpDetector.OnBump += (bump) => isBumpDetected = true;
        }

        /// <summary>
        /// Nothing should happen if no movement is detected.
        /// </summary>
        [Test]
        public void NotMovingTest()
        {
            bumpDetector.DetectBump();
            accelerometer.Verify(acc => acc.Update());
            accelerometer.Verify(acc => acc.IsAccelerating());
            accelerometer.Verify(acc => acc.IsDecelerating(), Times.Never());
            accelerometer.Verify(acc => acc.IsStationary(), Times.Never());
            accelerometer.Verify(acc => acc.IsUpright(), Times.Never());
            magnetometer.Verify(mag => mag.Update());
            magnetometer.Verify(mag => mag.IsChanging(), Times.Never());

            Assert.IsFalse(isBumpDetected);
        }

        /// <summary>
        /// If the phone only accelerates once, nothing should happen (only internally).
        /// </summary>
        [Test]
        public void Accelerate()
        {
            accelerometer.Setup(acc => acc.IsAccelerating()).Returns(true);
            bumpDetector.DetectBump();

            accelerometer.Verify(acc => acc.Update());
            accelerometer.Verify(acc => acc.IsAccelerating());
            accelerometer.Verify(acc => acc.IsDecelerating(), Times.Never());
            accelerometer.Verify(acc => acc.IsStationary(), Times.Never());
            accelerometer.Verify(acc => acc.IsUpright(), Times.Never());
            magnetometer.Verify(mag => mag.Update());
            magnetometer.Verify(mag => mag.IsChanging(), Times.Never());

            Assert.IsFalse(isBumpDetected);
        }

        /// <summary>
        /// If the phone accelerates, then keeps moving forward without accelerating or decelerating,
        /// check if the accelerometer is polled for deceleration, but not for magnetometer change.
        /// </summary>
        [Test]
        public void AccelerateOnce()
        {
            int accelerate = 0;
            accelerometer.Setup(acc => acc.IsAccelerating()).Returns(() => accelerate++ == 0);
            bumpDetector.DetectBump();
            bumpDetector.DetectBump();

            accelerometer.Verify(acc => acc.Update(), Times.Exactly(2));
            accelerometer.Verify(acc => acc.IsAccelerating(), Times.Exactly(2));
            accelerometer.Verify(acc => acc.IsDecelerating());
            accelerometer.Verify(acc => acc.IsStationary(), Times.Never());
            accelerometer.Verify(acc => acc.IsUpright(), Times.Never());
            magnetometer.Verify(mag => mag.Update(), Times.Exactly(2));
            magnetometer.Verify(mag => mag.IsChanging(), Times.Never());

            Assert.IsFalse(isBumpDetected);
        }

        /// <summary>
        /// If the phone keeps accelerating, the accelerometer should not be polled for 
        /// deceleration (a new bump is started each time acceleration is detected).
        /// </summary>
        [Test]
        public void KeepAccelerating()
        {
            accelerometer.Setup(acc => acc.IsAccelerating()).Returns(true);
            bumpDetector.DetectBump();
            bumpDetector.DetectBump();

            accelerometer.Verify(acc => acc.Update(), Times.Exactly(2));
            accelerometer.Verify(acc => acc.IsAccelerating(), Times.Exactly(2));
            accelerometer.Verify(acc => acc.IsDecelerating(), Times.Never());
            accelerometer.Verify(acc => acc.IsStationary(), Times.Never());
            accelerometer.Verify(acc => acc.IsUpright(), Times.Never());
            magnetometer.Verify(mag => mag.Update(), Times.Exactly(2));
            magnetometer.Verify(mag => mag.IsChanging(), Times.Never());

            Assert.IsFalse(isBumpDetected);
        }

        /// <summary>
        /// If the phone accelerates, then decelerates, but the magnetometer is not changing,
        /// the bump should not progress.
        /// </summary>
        [Test]
        public void AccelerateThenDecelerateWithoutMagnetometer()
        {
            int accelerate = 0;
            accelerometer.Setup(acc => acc.IsAccelerating()).Returns(() => accelerate++ == 0);
            accelerometer.Setup(acc => acc.IsDecelerating()).Returns(true);

            bumpDetector.DetectBump();
            bumpDetector.DetectBump();

            accelerometer.Verify(acc => acc.Update(), Times.Exactly(2));
            accelerometer.Verify(acc => acc.IsAccelerating(), Times.Exactly(2));
            accelerometer.Verify(acc => acc.IsDecelerating());
            accelerometer.Verify(acc => acc.IsStationary(), Times.Never());
            accelerometer.Verify(acc => acc.IsUpright(), Times.Never());
            magnetometer.Verify(mag => mag.Update(), Times.Exactly(2));
            magnetometer.Verify(mag => mag.IsChanging());

            Assert.IsFalse(isBumpDetected);
        }

        /// <summary>
        /// If the phone accelerates, then decelerates and the magnetometer changes, the 
        /// bump should be progressed and the accelerometer should be polled for being 
        /// stationary.
        /// </summary>
        [Test]
        public void AccelerateThenDecelerateWithMagnetometer()
        {
            int accelerate = 0;
            int decelerate = 0;
            accelerometer.Setup(acc => acc.IsAccelerating()).Returns(() => accelerate++ == 0);
            accelerometer.Setup(acc => acc.IsDecelerating()).Returns(() => decelerate++ == 0);
            magnetometer.Setup(acc => acc.IsChanging()).Returns(true);

            bumpDetector.DetectBump();
            bumpDetector.DetectBump();
            bumpDetector.DetectBump();

            accelerometer.Verify(acc => acc.Update(), Times.Exactly(3));
            accelerometer.Verify(acc => acc.IsAccelerating(), Times.Exactly(3));
            accelerometer.Verify(acc => acc.IsDecelerating(), Times.Exactly(2));
            accelerometer.Verify(acc => acc.IsStationary());
            accelerometer.Verify(acc => acc.IsUpright(), Times.Never());
            magnetometer.Verify(mag => mag.Update(), Times.Exactly(3));
            magnetometer.Verify(mag => mag.IsChanging());

            Assert.IsFalse(isBumpDetected);
        }

        /// <summary>
        /// If the phone is stationary after accelerating and decelerating, the accelerometer
        /// should be polled for being upright.
        /// </summary>
        [Test]
        public void AccelerateThenDecelerateThenStationary()
        {
            int accelerate = 0;
            int decelerate = 0;
            accelerometer.Setup(acc => acc.IsAccelerating()).Returns(() => accelerate++ == 0);
            accelerometer.Setup(acc => acc.IsDecelerating()).Returns(() => decelerate++ == 0);
            accelerometer.Setup(acc => acc.IsStationary()).Returns(true);
            magnetometer.Setup(acc => acc.IsChanging()).Returns(true);

            bumpDetector.DetectBump();
            bumpDetector.DetectBump();
            bumpDetector.DetectBump();

            accelerometer.Verify(acc => acc.Update(), Times.Exactly(3));
            accelerometer.Verify(acc => acc.IsAccelerating(), Times.Exactly(3));
            accelerometer.Verify(acc => acc.IsDecelerating(), Times.Exactly(2));
            accelerometer.Verify(acc => acc.IsStationary());
            accelerometer.Verify(acc => acc.IsUpright());
            magnetometer.Verify(mag => mag.Update(), Times.Exactly(3));
            magnetometer.Verify(mag => mag.IsChanging());

            Assert.IsFalse(isBumpDetected);
        }

        /// <summary>
        /// If the phone accelerates, then decelerates with magnetometer change, then is 
        /// held stationary and upright, a bump should be detected.
        /// </summary>
        [Test]
        public void AccelerateThenDecelerateThenStationaryAndUpright()
        {
            int accelerate = 0;
            int decelerate = 0;
            accelerometer.Setup(acc => acc.IsAccelerating()).Returns(() => accelerate++ == 0);
            accelerometer.Setup(acc => acc.IsDecelerating()).Returns(() => decelerate++ == 0);
            accelerometer.Setup(acc => acc.IsStationary()).Returns(true);
            accelerometer.Setup(acc => acc.IsUpright()).Returns(true);
            magnetometer.Setup(acc => acc.IsChanging()).Returns(true);

            bumpDetector.DetectBump();
            bumpDetector.DetectBump();
            bumpDetector.DetectBump();

            accelerometer.Verify(acc => acc.Update(), Times.Exactly(3));
            accelerometer.Verify(acc => acc.IsAccelerating(), Times.Exactly(3));
            accelerometer.Verify(acc => acc.IsDecelerating(), Times.Exactly(2));
            accelerometer.Verify(acc => acc.IsStationary());
            accelerometer.Verify(acc => acc.IsUpright());
            magnetometer.Verify(mag => mag.Update(), Times.Exactly(3));
            magnetometer.Verify(mag => mag.IsChanging());

            Assert.IsTrue(isBumpDetected);
        }

        /// <summary>
        /// After detecting a bump, the bump detection state should be reset, so nothing 
        /// will be checked until acceleration is detected.
        /// </summary>
        [Test]
        public void ResetAfterBump()
        {
            int accelerate = 0;
            int decelerate = 0;
            accelerometer.Setup(acc => acc.IsAccelerating()).Returns(() => accelerate++ == 0);
            accelerometer.Setup(acc => acc.IsDecelerating()).Returns(() => decelerate++ == 0);
            accelerometer.Setup(acc => acc.IsStationary()).Returns(true);
            accelerometer.Setup(acc => acc.IsUpright()).Returns(true);
            magnetometer.Setup(acc => acc.IsChanging()).Returns(true);

            bumpDetector.DetectBump();
            bumpDetector.DetectBump();
            bumpDetector.DetectBump();
            bumpDetector.DetectBump();

            accelerometer.Verify(acc => acc.Update(), Times.Exactly(4));
            accelerometer.Verify(acc => acc.IsAccelerating(), Times.Exactly(4));
            accelerometer.Verify(acc => acc.IsDecelerating(), Times.Exactly(2));
            accelerometer.Verify(acc => acc.IsStationary());
            accelerometer.Verify(acc => acc.IsUpright());
            magnetometer.Verify(mag => mag.Update(), Times.Exactly(4));
            magnetometer.Verify(mag => mag.IsChanging());

            Assert.IsTrue(isBumpDetected);
        }

        /// <summary>
        /// Bump detection state should not be reset if an earlier phase (such as acceleration) is encountered.
        /// </summary>
        [Test]
        public void NoResetAtAcceleration()
        {
            int accelerate = 0;
            accelerometer.Setup(acc => acc.IsAccelerating()).Returns(() => accelerate++ == 0 || accelerate == 3);

            bumpDetector.DetectBump();
            bumpDetector.DetectBump();
            bumpDetector.DetectBump();
            bumpDetector.DetectBump();

            accelerometer.Verify(acc => acc.Update(), Times.Exactly(4));
            accelerometer.Verify(acc => acc.IsAccelerating(), Times.Exactly(4));
            accelerometer.Verify(acc => acc.IsDecelerating(), Times.Exactly(2));
            accelerometer.Verify(acc => acc.IsStationary(), Times.Never());
            accelerometer.Verify(acc => acc.IsUpright(), Times.Never());
            magnetometer.Verify(mag => mag.Update(), Times.Exactly(4));
            magnetometer.Verify(mag => mag.IsChanging(), Times.Never());

            Assert.IsFalse(isBumpDetected);
        }

        /// <summary>
        /// Bump detection state should be reset when the delay between accelerating and 
        /// decelerating is too large.
        /// </summary>
        [Test]
        public void ProgressionTimeout()
        {
            int accelerate = 0;
            accelerometer.Setup(acc => acc.IsAccelerating()).Returns(() => accelerate++ == 0);
            accelerometer.Setup(acc => acc.IsDecelerating()).Returns(true);
            magnetometer.Setup(mag => mag.IsChanging()).Returns(true);

            bumpDetector.DetectBump();

            Thread.Sleep(110);

            bumpDetector.DetectBump();
            bumpDetector.DetectBump();

            accelerometer.Verify(acc => acc.Update(), Times.Exactly(3));
            accelerometer.Verify(acc => acc.IsAccelerating(), Times.Exactly(3));
            accelerometer.Verify(acc => acc.IsDecelerating(), Times.Exactly(1));
            accelerometer.Verify(acc => acc.IsStationary(), Times.Never());
            accelerometer.Verify(acc => acc.IsUpright(), Times.Never());
            magnetometer.Verify(mag => mag.Update(), Times.Exactly(3));
            magnetometer.Verify(mag => mag.IsChanging());

            Assert.IsFalse(isBumpDetected);
        }

        /// <summary>
        /// Bump detection should be reset when the delay between decelerating and being 
        /// stationary is too large.
        /// </summary>
        [Test]
        public void CompletionTimeout()
        {
            int accelerate = 0;
            int decelerate = 0;
            accelerometer.Setup(acc => acc.IsAccelerating()).Returns(() => accelerate++ == 0);
            accelerometer.Setup(acc => acc.IsDecelerating()).Returns(() => decelerate++ == 0);
            accelerometer.Setup(acc => acc.IsStationary()).Returns(true);
            accelerometer.Setup(acc => acc.IsUpright()).Returns(true);
            magnetometer.Setup(mag => mag.IsChanging()).Returns(true);

            bumpDetector.DetectBump();
            bumpDetector.DetectBump();

            Thread.Sleep(200);

            bumpDetector.DetectBump();
            bumpDetector.DetectBump();

            accelerometer.Verify(acc => acc.Update(), Times.Exactly(4));
            accelerometer.Verify(acc => acc.IsAccelerating(), Times.Exactly(4));
            accelerometer.Verify(acc => acc.IsDecelerating(), Times.Exactly(2));
            accelerometer.Verify(acc => acc.IsStationary());
            accelerometer.Verify(acc => acc.IsUpright());
            magnetometer.Verify(mag => mag.Update(), Times.Exactly(4));
            magnetometer.Verify(mag => mag.IsChanging());

            Assert.IsFalse(isBumpDetected);
        }
    }
}
