using UnityEngine;
using System.Collections.Generic;

public class InGameOverlay : MonoBehaviour
{
    private const float TRASHCAN_SIZE          = .1f;
    private const float TRASHCAN_SELECTED_SIZE = .12f;
    private const float TRASHCAN_PADDING       = .01f;

    private const float VIEW_SELECTOR_SIZE          = .1f;
    private const float VIEW_SELECTOR_SELECTED_SIZE = .12f;
    private const float VIEW_SELECTOR_TOP           = .01f;
    private const float VIEW_SELECTOR_PADDING       = .03f;

    private const float PROGRESSBAR_WIDTH   = .15f;
    private const float PROGRESSBAR_HEIGHT  = .03f;
    private const float PROGRESSBAR_PADDING = .01f;

    private float progress = 0.4f;

    public Texture2D TrashcanIcon;
    public Texture2D ConstructionIcon;
    public Texture2D HouseIcon;
    public Texture2D BlocksIcon;

    private static LinkedList<GuiView> views;

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
        InGameOverlay.AddView(new GuiView("game1scene", ConstructionIcon));
        InGameOverlay.AddView(new GuiView("s", HouseIcon));
        InGameOverlay.AddView(new GuiView("b", BlocksIcon));
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
        if (GUI.Button(new Rect(padding, padding, size, size), TrashcanIcon, GUIStyle.none))
        {
            trashcanSelected = !trashcanSelected;
            TouchBehaviour.DeleteMode = trashcanSelected;
        }
    }

    private void drawViewIcons()
    {
        float totalWidth = Screen.width * ((views.Count - 1) * VIEW_SELECTOR_SIZE + views.Count * VIEW_SELECTOR_PADDING + VIEW_SELECTOR_SELECTED_SIZE);
        float padding = Screen.width * VIEW_SELECTOR_TOP;
        float x = Screen.width / 2 - totalWidth / 2;

        foreach (GuiView view in views)
        {
            float size = Screen.width * (Application.loadedLevelName == view.SceneName ? VIEW_SELECTOR_SELECTED_SIZE : VIEW_SELECTOR_SIZE);

            // Draw shadow
            GUI.color = Color.gray;
            GUI.DrawTexture(new Rect(x, padding, size * 1.005f, size * 1.005f), view.Icon);

            // Draw button
            GUI.color = Color.white;
            if (GUI.Button(new Rect(x, padding, size, size), view.Icon, GUIStyle.none))
            {
                Application.LoadLevel(view.SceneName);
            }
            x += size + Screen.width * VIEW_SELECTOR_PADDING;
        }
    }

    private void drawProgressBar()
    {
        // Draw progress bar background
        GUI.color = Color.gray;
        GUI.Box(new Rect(
                (1f - PROGRESSBAR_WIDTH - PROGRESSBAR_PADDING) * Screen.width, 
                Screen.width * PROGRESSBAR_PADDING, 
                Screen.width * PROGRESSBAR_WIDTH, 
                Screen.width * PROGRESSBAR_HEIGHT
            ), GUIContent.none, progressStyle);

        // Draw progress
        GUI.color = Color.yellow;
        GUI.Box(new Rect(
                (1f - PROGRESSBAR_WIDTH * progress - PROGRESSBAR_PADDING) * Screen.width, 
                Screen.width * PROGRESSBAR_PADDING, 
                Screen.width * PROGRESSBAR_WIDTH * progress, 
                Screen.width * PROGRESSBAR_HEIGHT
            ), GUIContent.none, progressStyle);
    }

    public static void AddView(GuiView view)
    {
        views.AddLast(view);
    }

    public static void AddView(string sceneName, Texture2D icon)
    {
        AddView(new GuiView(sceneName, icon));
    }
}