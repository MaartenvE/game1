using UnityEngine;

public struct GuiView
{
    public readonly string SceneName;
    public readonly Texture2D Icon;

    public GuiView(string sceneName, Texture2D icon)
    {
        SceneName = sceneName;
        Icon = icon;
    }
}
