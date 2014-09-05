
namespace BuildingBlocks.Input
{
    public delegate void TouchDetectorHandler();

    public interface ITouchDetector
    {
        event TouchDetectorHandler OnTouch;

        void Update();
    }
}
