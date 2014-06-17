using UnityEngine;
using System.Collections.Generic;
using BuildingBlocks.CubeFinger;

public class InGameOverlay : MonoBehaviour
{
    private const float TRASHCAN_SIZE          = .1f;
    private const float TRASHCAN_SELECTED_SIZE = .12f;
    private const float TRASHCAN_PADDING       = .01f;

    private const float REFRESH_SIZE = .15f;
    private const float REFRESH_PADDING = .8f;

    private const float VIEW_SELECTOR_SIZE          = .1f;
    private const float VIEW_SELECTOR_SELECTED_SIZE = .12f;
    private const float VIEW_SELECTOR_TOP           = .01f;
    private const float VIEW_SELECTOR_PADDING       = .03f;

    public const float PROGRESSBAR_WIDTH   = .15f;
    public const float PROGRESSBAR_HEIGHT  = .03f;
    public const float PROGRESSBAR_PADDING = .01f;

    public Texture2D TrashcanIcon;
    public Texture2D ConstructionIcon;
    public Texture2D HouseIcon;
    public Texture2D BlocksIcon;
    public Texture2D RefreshIcon;

    public bool AnimationDone;

    private static LinkedList<GuiView> views;
    private GuiView activeView;

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

        views = new LinkedList<GuiView>();
        InGameOverlay.AddView(new GuiView("currentStructure", ConstructionIcon));
        InGameOverlay.AddView(new GuiView("goalStructure", HouseIcon));
        InGameOverlay.AddView(new GuiView("combinedStructure", BlocksIcon));
        activeView = views.First.Value;
        AnimationDone = true;
    }

    void Update()
    {
        // Draw the right blocks
        if (activeView.SceneName == "combinedStructure")
        {
            ToggleBlocksByTag("currentStructure", true);
            ToggleBlocksByTag("goalStructure", true);
        }
        else
        {
            ShowStructure(activeView.SceneName);
        }  
    }

    void OnGUI()
    {
        // Trashcan icon in top left
        drawTrashcanIcon();

        // View selectors in top middle
        drawViewIcons();

        // Percentage in top right
        drawProgressBar();

        // Own block in bottom right
        if (AnimationDone)
        {
            drawRefreshIcon();
        }

        // Leave Game
        //leaveGame();

		//back button quits game
        backButton();
    }

    private void drawTrashcanIcon()
    {
        float size = Screen.width * (trashcanSelected ? TRASHCAN_SELECTED_SIZE : TRASHCAN_SIZE);
        float padding = Screen.width * TRASHCAN_PADDING;
        
        // Draw shadow
        GUI.color = Color.gray;
        GUI.DrawTexture(new Rect(padding, padding, size * 1.01f, size * 1.01f), TrashcanIcon);

        // Draw button
        GUI.color = trashcanSelected ? Color.red : Color.white;
        GUI.DrawTexture(new Rect(padding, padding, size, size), TrashcanIcon);
        if (GUI.Button(new Rect(padding, padding, size, size), GUIContent.none, GUIStyle.none))
        {
            trashcanSelected = !trashcanSelected;
            CubeFinger cubeFinger = PlayerInfo.CubeFinger;
            if (cubeFinger != null)
            {
                cubeFinger.Mode = trashcanSelected ? CubeFingerMode.Delete : CubeFingerMode.Build;
            }
        }
    }

    private void drawRefreshIcon()
    {
        float size = Screen.width * REFRESH_SIZE;
        float padding_left = Screen.width * REFRESH_PADDING;
        float padding_top = Screen.height * REFRESH_PADDING;

        // Draw button
        GUI.color = Color.white;
        GUI.DrawTexture(new Rect(padding_left, padding_top, size, size), RefreshIcon);
        if (GUI.Button(new Rect(padding_left, padding_top, size, size), GUIContent.none, GUIStyle.none))
        {
            NetworkView playerNetworkView = GameObject.Find("Player").networkView;
            INetworkView _networkView = new NetworkViewWrapper(playerNetworkView);
            _networkView.RPC("ThrowAwayBlock", RPCMode.Server);
        }
    }

    private void drawViewIcons()
    {
        float totalWidth = Screen.width * ((views.Count - 1) * VIEW_SELECTOR_SIZE + views.Count * VIEW_SELECTOR_PADDING + VIEW_SELECTOR_SELECTED_SIZE);
        float padding = Screen.width * VIEW_SELECTOR_TOP;
        float x = Screen.width / 2 - totalWidth / 2;

        foreach (GuiView view in views)
        {
            float size = Screen.width * (activeView.SceneName == view.SceneName ? VIEW_SELECTOR_SELECTED_SIZE : VIEW_SELECTOR_SIZE);

            // Draw shadow
            GUI.color = Color.gray;
            GUI.DrawTexture(new Rect(x, padding, size + 1, size + 1), view.Icon);

            // Draw button
            // For some reason, buttons are drawn smaller, so draw a Texture with underneath a button.
            GUI.color = (activeView.SceneName == view.SceneName) ? Color.green : Color.white;
            GUI.DrawTexture(new Rect(x, padding, size, size), view.Icon);
            if (GUI.Button(new Rect(x, padding, size, size), GUIContent.none, GUIStyle.none))
            {
                activeView = view;
            }

            x += size + Screen.width * VIEW_SELECTOR_PADDING;
        }
    }

    private void ShowStructure(string _tag)
    {
        ToggleBlocksByTag("currentStructure", false);
        ToggleBlocksByTag("goalStructure", false);
        ToggleBlocksByTag(_tag, true);
    }

    private void ToggleBlocksByTag(string _tag, bool _show)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(_tag);
        foreach (GameObject block in gameObjects)
        {
            block.renderer.enabled = _show;
            block.layer = _show ? 0 : 2;
        }
    }

    private void drawProgressBar()
    {
        float progress = 0.0f;
        int team = PlayerInfo.Team;
        TeamInfoLoader[] teamLoaders = GameObject.Find("Teams").GetComponentsInChildren<TeamInfoLoader>();
        foreach (TeamInfoLoader loader in teamLoaders)
        {
            if (loader.TeamInfo.ID == team)
            {
                progress = loader.TeamInfo.Progress;
            }
        }

        ProgressBar.Draw(new Rect(
                (1f - PROGRESSBAR_WIDTH - PROGRESSBAR_PADDING) * Screen.width,
                Screen.width * PROGRESSBAR_PADDING,
                Screen.width * PROGRESSBAR_WIDTH,
                Screen.width * PROGRESSBAR_HEIGHT
            ), progress);
    }

    public static void AddView(GuiView view)
    {
        views.AddLast(view);
    }

    public static void AddView(string sceneName, Texture2D icon)
    {
        AddView(new GuiView(sceneName, icon));
    }

    private void leaveGame()
    {
        GUI.color = Color.white;
        if (GUI.Button(new Rect(0, Screen.height - 25, 100, 20), "Leave Game"))
        {
            Application.Quit();
        }
    }

    private void backButton()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel(1);
        }
    }
}