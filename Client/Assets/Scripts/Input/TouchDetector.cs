
namespace BuildingBlocks.Input
{
    public class TouchDetector : ITouchDetector
    {
        private IMouseInput input;
        private bool clickStarted;

        public event TouchDetectorHandler OnTouch;

        public TouchDetector(IMouseInput input)
        {
            this.input = input;
        }

        public void Update()
        {
            if (input.GetMouseButtonDown(0))
            {
                clickStarted = true;
            }

            else if (clickStarted && input.GetMouseButtonUp(0))
            {
                clickStarted = false;
                if (OnTouch != null)
                {
                    OnTouch();
                }
            }
        }
    }
}
