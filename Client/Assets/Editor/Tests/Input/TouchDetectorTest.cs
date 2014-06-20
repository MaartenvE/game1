using NUnit.Framework;
using Moq;

namespace BuildingBlocks.Input
{
    [TestFixture]
    public class TouchDetectorTest
    {
        private Mock<IMouseInput> mouseInputMock;
        private TouchDetector touchDetector;
        private bool fired;

        [SetUp]
        public void Setup()
        {
            mouseInputMock = new Mock<IMouseInput>();
            touchDetector = new TouchDetector(mouseInputMock.Object);
            touchDetector.OnTouch += () => fired = true;
            fired = false;
        }

        [Test]
        public void TestNoMouseEvent()
        {
            touchDetector.Update();
            Assert.IsFalse(fired);
        }

        [Test]
        public void TestMouseDown()
        {
            mouseInputMock.Setup(m => m.GetMouseButtonDown(0)).Returns(true);
            touchDetector.Update();
            Assert.IsFalse(fired);
        }

        [Test]
        public void TestMouseUp()
        {
            mouseInputMock.Setup(m => m.GetMouseButtonUp(0)).Returns(true);
            touchDetector.Update();
            Assert.IsFalse(fired);
        }

        [Test]
        public void TestMouseDownTwice()
        {
            mouseInputMock.Setup(m => m.GetMouseButtonDown(0)).Returns(true);
            touchDetector.Update();
            touchDetector.Update();
            Assert.IsFalse(fired);
        }

        [Test]
        public void TestMouseUpTwice()
        {
            mouseInputMock.Setup(m => m.GetMouseButtonUp(0)).Returns(true);
            touchDetector.Update();
            touchDetector.Update();
            Assert.IsFalse(fired);
        }

        [Test]
        public void TestMouseDownUp()
        {
            int time = 0;
            mouseInputMock.Setup(m => m.GetMouseButtonDown(0)).Returns(() => time++ == 0);
            mouseInputMock.Setup(m => m.GetMouseButtonUp(0)).Returns(() => time++ == 2);
            touchDetector.Update();
            touchDetector.Update();
            Assert.IsTrue(fired);
        }

        [Test]
        public void TestMouseUpDown()
        {
            int time = 0;
            mouseInputMock.Setup(m => m.GetMouseButtonDown(0)).Returns(() => time++ == 2);
            mouseInputMock.Setup(m => m.GetMouseButtonUp(0)).Returns(() => time++ == 0);
            touchDetector.Update();
            touchDetector.Update();
            Assert.IsFalse(fired);
        }

        [Test]
        public void TestMouseDownDownUp()
        {
            int time = 0;
            mouseInputMock.Setup(m => m.GetMouseButtonDown(0)).Returns(() => time++ == 0 || time == 3);
            mouseInputMock.Setup(m => m.GetMouseButtonUp(0)).Returns(() => time++ == 4);
            touchDetector.Update();
            touchDetector.Update();
            touchDetector.Update();
            Assert.IsTrue(fired);
        }
    }
}
