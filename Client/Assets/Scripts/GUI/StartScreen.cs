using UnityEngine;
using BuildingBlocks.Team;
using BuildingBlocks.Blocks;

namespace BuildingBlocks.GUI
{
    public class StartScreen : MonoBehaviour
    {
        private const float BUTTON_WIDTH = .5f;
        private const float BUTTON_HEIGHT = .1f;

        void Start()
        {
            GameObject.Find("RotatingBlock").renderer.material.color = ColorModel.RandomColor();
        }

        void OnGUI()
        {
            drawTitle();
            drawButtons();
            handleQuit();
        }

        private void drawTitle()
        {
            UnityEngine.GUI.contentColor = new Color(140f, 156f, 179f);
            UnityEngine.GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Building Blocks", GUIStyles.BoxStyle(Screen.height, Screen.width));
        }

        private void drawButtons()
        {
            float width = Screen.width * BUTTON_WIDTH;
            float height = Screen.height * BUTTON_HEIGHT;
            drawPlayButton(width, height);
            drawSpectateButton(width, height);
        }

        private void drawPlayButton(float width, float height)
        {
            UnityEngine.GUI.backgroundColor = new Color(121f, 180f, 150f);
            if (UnityEngine.GUI.Button(new Rect(
                    (Screen.width / 2) - (width / 2),
                    (Screen.height / 2) - height,
                    width, height),
                "Play", GUIStyles.ButtonStyle(Screen.height, Screen.width)))
            {
                TeamSelector.IsSpectator = false;
                Application.LoadLevel(Application.loadedLevel + 1);
            }
        }

        private void drawSpectateButton(float width, float height)
        {
            if (UnityEngine.GUI.Button(new Rect(
                    (Screen.width / 2) - (width / 2),
                    (Screen.height / 2) + height,
                    width, height),
                "Spectate", GUIStyles.ButtonStyle(Screen.height, Screen.width)))
            {
                TeamSelector.IsSpectator = true;
                Application.LoadLevel(Application.loadedLevel + 1);
            }
        }

        private void handleQuit()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
