using UnityEngine;
using System.Collections.Generic;
using BuildingBlocks.CubeFinger;
using BuildingBlocks.Team;
using BuildingBlocks.Player;

namespace BuildingBlocks.GUI
{
    public class InGameOverlay : MonoBehaviour
    {
        private const float REFRESH_SIZE = .15f;
        private const float REFRESH_PADDING = .8f;

        private const float VIEW_SELECTOR_SIZE = .1f;
        private const float VIEW_SELECTOR_SELECTED_SIZE = .12f;
        private const float VIEW_SELECTOR_TOP = .01f;
        private const float VIEW_SELECTOR_PADDING = .03f;
        private float iconXLocation;

        public const float PROGRESSBAR_WIDTH = .15f;
        public const float PROGRESSBAR_HEIGHT = .03f;
        public const float PROGRESSBAR_PADDING = .01f;

        public Texture2D TrashcanIcon;
        public Texture2D ConstructionIcon;
        public Texture2D RefreshIcon;

        public bool AnimationDone;

        private GUIStyle progressStyle;
        private Texture2D progressTexture;

        private bool trashcanSelected = false;



        void Start()
        {
            progressTexture = new Texture2D(1, 1);
            progressTexture.SetPixel(0, 0, Color.white);
            progressTexture.Apply();

            progressStyle = new GUIStyle();
            progressStyle.normal.background = progressTexture;

            AnimationDone = true;
        }

        void OnGUI()
        {
            iconXLocation = 0.0f;

            drawTrashcanIcon();
            drawBuildIcon();

            // Percentage in top right
            drawProgressBar();

            // Own block in bottom right
            if (AnimationDone)
            {
                drawRefreshIcon();
            }

            //back button quits game
            backButton();
        }



        private void drawTrashcanIcon()
        {
            float totalWidth = Screen.width * (VIEW_SELECTOR_SIZE + 2 * VIEW_SELECTOR_PADDING + VIEW_SELECTOR_SELECTED_SIZE);
            float padding = Screen.width * VIEW_SELECTOR_TOP;
            float size = Screen.width * (trashcanSelected ? VIEW_SELECTOR_SELECTED_SIZE : VIEW_SELECTOR_SIZE);
            if (iconXLocation < 0.001f)
            {
                iconXLocation = Screen.width / 2 - totalWidth / 2;
            }
            else
            {
                iconXLocation += size + Screen.width * VIEW_SELECTOR_PADDING;
            }

            float scalingCorrection = size - VIEW_SELECTOR_SIZE * Screen.width;

            // Draw shadow
            UnityEngine.GUI.color = Color.gray;
            UnityEngine.GUI.DrawTexture(new Rect(iconXLocation - scalingCorrection, padding, size, size), TrashcanIcon);
            // Draw button
            UnityEngine.GUI.color = trashcanSelected ? Color.red : Color.white;
            UnityEngine.GUI.DrawTexture(new Rect(iconXLocation - scalingCorrection, padding, size, size), TrashcanIcon);
            if (UnityEngine.GUI.Button(new Rect(iconXLocation - scalingCorrection, padding, size, size), GUIContent.none, GUIStyle.none))
            {

                switchMode();
            }
        }

        private void drawBuildIcon()
        {

            float totalWidth = Screen.width * (VIEW_SELECTOR_SIZE + 2 * VIEW_SELECTOR_PADDING + VIEW_SELECTOR_SELECTED_SIZE);
            float padding = Screen.width * VIEW_SELECTOR_TOP;
            float size = Screen.width * (!trashcanSelected ? VIEW_SELECTOR_SELECTED_SIZE : VIEW_SELECTOR_SIZE);
            if (iconXLocation < 0.001f)
            {
                iconXLocation = Screen.width / 2 - totalWidth / 2;
            }
            else
            {
                iconXLocation += size + Screen.width * VIEW_SELECTOR_PADDING;
            }

            float scalingCorrection = size - VIEW_SELECTOR_SIZE * Screen.width;

            // Draw shadow
            UnityEngine.GUI.color = Color.gray;
            UnityEngine.GUI.DrawTexture(new Rect(iconXLocation - scalingCorrection, padding, size, size), ConstructionIcon);
            // Draw button
            UnityEngine.GUI.color = !trashcanSelected ? Color.green : Color.white;
            UnityEngine.GUI.DrawTexture(new Rect(iconXLocation - scalingCorrection, padding, size, size), ConstructionIcon);
            if (UnityEngine.GUI.Button(new Rect(iconXLocation - scalingCorrection, padding, size, size), GUIContent.none, GUIStyle.none))
            {
                switchMode();
            }
        }

        private void drawRefreshIcon()
        {
            float size = Screen.width * REFRESH_SIZE;
            float padding_left = Screen.width * REFRESH_PADDING;
            float padding_top = Screen.height * REFRESH_PADDING;

            // Draw button
            UnityEngine.GUI.color = Color.white;
            UnityEngine.GUI.DrawTexture(new Rect(padding_left, padding_top, size, size), RefreshIcon);
            if (UnityEngine.GUI.Button(new Rect(padding_left, padding_top, size, size), GUIContent.none, GUIStyle.none))
            {
                NetworkView playerNetworkView = GameObject.Find("Player").networkView;
                INetworkView _networkView = new NetworkViewWrapper(playerNetworkView);
                _networkView.RPC("ThrowAwayBlock", RPCMode.Server);
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
            float progress = 0.0f;
            ITeam team = Player.Player.LocalPlayer.Team;
            if (team != null)
            {
                progress = team.Progress;

                ProgressBar.Draw(new Rect(
                        (1f - PROGRESSBAR_WIDTH - PROGRESSBAR_PADDING) * Screen.width,
                        Screen.width * PROGRESSBAR_PADDING,
                        Screen.width * PROGRESSBAR_WIDTH,
                        Screen.width * PROGRESSBAR_HEIGHT
                    ), progress);
            }
        }

        private void leaveGame()
        {
            UnityEngine.GUI.color = Color.white;
            if (UnityEngine.GUI.Button(new Rect(0, Screen.height - 25, 100, 20), "Leave Game"))
            {
                Application.Quit();
            }
        }

        private void backButton()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                Application.LoadLevel(1);
            }
        }
    }
}
