using UnityEngine;
using BuildingBlocks.CubeFinger;
using BuildingBlocks.Team;
using BuildingBlocks.Player;

namespace BuildingBlocks.GUI
{
    public class InGameOverlay : MonoBehaviour
    {
        private const float REFRESH_SIZE = .15f;
        private const float REFRESH_PADDING = .8f;

        private const float VIEW_SELECTOR_SIZE = .12f;
        private const float VIEW_SELECTOR_SELECTED_SIZE = .15f;
        private const float VIEW_SELECTOR_TOP = .01f;
        private const float VIEW_SELECTOR_PADDING = .03f;

        public const float PROGRESSBAR_WIDTH = .15f;
        public const float PROGRESSBAR_HEIGHT = .03f;
        public const float PROGRESSBAR_PADDING = .01f;

        public Texture2D TrashcanIcon;
        public Texture2D ConstructionIcon;
        public Texture2D RefreshIcon;

        public bool AnimationDone { get; set; }

        private bool trashcanSelected = false;

        void Start()
        {
            AnimationDone = true;
        }

        void OnGUI()
        {
            drawTrashcanIcon();
            drawBuildIcon();

            // Percentage in top right
            drawProgressBar();

            // Own block in bottom right
            if (AnimationDone)
            {
                drawRefreshIcon();
            }
        }

        private void drawTrashcanIcon()
        {
            float size = Screen.width * (trashcanSelected ? VIEW_SELECTOR_SELECTED_SIZE : VIEW_SELECTOR_SIZE);
            float xpos = Screen.width / 2 - size - Screen.width * (VIEW_SELECTOR_PADDING / 2);

            Color color = trashcanSelected ? Color.red : Color.white;
            if (Icon.IsPressed(new Rect(xpos, Screen.width * VIEW_SELECTOR_TOP, size, size), TrashcanIcon, color))
            {
                switchMode();
            }
        }

        private void drawBuildIcon()
        {
            float size = Screen.width * (trashcanSelected ? VIEW_SELECTOR_SIZE : VIEW_SELECTOR_SELECTED_SIZE);
            float xpos = Screen.width / 2 + Screen.width * (VIEW_SELECTOR_PADDING / 2);

            Color color = trashcanSelected ? Color.white : Color.green;
            if (Icon.IsPressed(new Rect(xpos, Screen.width * VIEW_SELECTOR_TOP, size, size), ConstructionIcon, color))
            {
                switchMode();
            }
        }

        private void drawRefreshIcon()
        {
            float size = Screen.width * REFRESH_SIZE;
            float padding_left = Screen.width * REFRESH_PADDING;
            float padding_top = Screen.height * REFRESH_PADDING;

            if (Icon.IsPressed(new Rect(padding_left, padding_top, size, size), RefreshIcon, Color.white))
            {
                INetworkView networkView = Player.Player.LocalPlayer.networkView;
                networkView.RPC("ThrowAwayBlock", RPCMode.Server);
            }
        }

        private void switchMode()
        {
            trashcanSelected = !trashcanSelected;
            ICubeFinger cubeFinger = Player.Player.LocalPlayer.CubeFinger;
            GameObject.Find("Crosshair").GetComponent<CrosshairBehaviour>().CycleModes();
            if (cubeFinger != null)
            {
                cubeFinger.Mode = trashcanSelected ? CubeFingerMode.Delete : CubeFingerMode.Build;
            }
        }

        private void drawProgressBar()
        {
            ITeam team = Player.Player.LocalPlayer.Team;
            if (team != null)
            {
                ProgressBar.Draw(new Rect(
                        (1f - PROGRESSBAR_WIDTH - PROGRESSBAR_PADDING) * Screen.width,
                        Screen.width * PROGRESSBAR_PADDING,
                        Screen.width * PROGRESSBAR_WIDTH,
                        Screen.width * PROGRESSBAR_HEIGHT
                    ), team.Progress);
            }
        }
    }
}
