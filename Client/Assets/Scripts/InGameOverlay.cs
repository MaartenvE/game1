using UnityEngine;
using System.Collections.Generic;

public class InGameOverlay : MonoBehaviour
{
    private const int TRASHCAN_SIZE = 48;
    private const int TRASHCAN_SELECTED_SIZE = 64;
    private const int TRASHCAN_PADDING = 3;

    private const int VIEW_SELECTOR_SIZE    = 48;
    private const int VIEW_SELECTOR_TOP     = 3;
    private const int VIEW_SELECTOR_PADDING = 13;

    private const int PROGRESSBAR_WIDTH   = 50;
    private const int PROGRESSBAR_HEIGHT  = 12;
    private const int PROGRESSBAR_PADDING = 5;

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
        InGameOverlay.AddView(new GuiView("game1scene", HouseIcon));
        InGameOverlay.AddView(new GuiView("game1scene", BlocksIcon));
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
        GUI.color = trashcanSelected ? Color.red : Color.white;
        int size = trashcanSelected ? TRASHCAN_SELECTED_SIZE : TRASHCAN_SIZE;
        if (GUI.Button(new Rect(TRASHCAN_PADDING, TRASHCAN_PADDING, size, size), TrashcanIcon, GUIStyle.none))
        {
            trashcanSelected = !trashcanSelected;
            TouchBehaviour.DeleteMode = trashcanSelected;
            Debug.Log("Clicked trashcan icon");
        }
    }

    private void drawViewIcons()
    {
        GUI.color = Color.white;
        int totalWidth = views.Count * VIEW_SELECTOR_SIZE + (views.Count - 1) * VIEW_SELECTOR_PADDING;
        int x = Screen.width / 2 - totalWidth / 2;

        foreach (GuiView view in views)
        {
            if (GUI.Button(new Rect(x, VIEW_SELECTOR_TOP, VIEW_SELECTOR_SIZE, VIEW_SELECTOR_SIZE), view.Icon, GUIStyle.none))
            {
                Debug.Log("Clicked " + view.SceneName);
                Application.LoadLevel(view.SceneName);
            }
            x += VIEW_SELECTOR_SIZE + VIEW_SELECTOR_PADDING;
        }
    }

    private void drawProgressBar()
    {
        GUI.color = Color.gray;
        GUI.Box(new Rect(Screen.width - PROGRESSBAR_WIDTH - PROGRESSBAR_PADDING, PROGRESSBAR_PADDING, PROGRESSBAR_WIDTH, PROGRESSBAR_HEIGHT), GUIContent.none, progressStyle);
        GUI.color = Color.yellow;
        GUI.Box(new Rect(Screen.width - 25, PROGRESSBAR_PADDING, 20, PROGRESSBAR_HEIGHT), GUIContent.none, progressStyle);
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