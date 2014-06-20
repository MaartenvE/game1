using UnityEngine;

namespace BuildingBlocks.Input
{
    public class UnityMouseInput : IMouseInput
    {
        public bool GetMouseButtonDown(int buttonId)
        {
            return UnityEngine.Input.GetMouseButtonDown(buttonId) && GUIUtility.hotControl == 0;
        }

        public bool GetMouseButtonUp(int buttonId)
        {
            return UnityEngine.Input.GetMouseButtonUp(buttonId);
        }
    }
}
