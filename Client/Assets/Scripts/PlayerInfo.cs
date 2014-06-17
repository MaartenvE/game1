using UnityEngine;
using System.Collections;
using BuildingBlocks.CubeFinger;

public class PlayerInfo : MonoBehaviour
{
    public static bool IsSpectator = false;
    public static int Team;
    public static CubeFinger CubeFinger;

    public static bool HasFullBlock { get; private set; }

    private static GameObject teamObject;

    [RPC]
    void SetPlayerInfo(int team)
    {
        Team = team;

        GameObject goalStructure = GameObject.Find("GoalStructure");

        foreach (TeamInfoLoader teamInfoLoader in GameObject.Find("Teams").GetComponentsInChildren<TeamInfoLoader>())
        {
            TeamInfo teamInfo = teamInfoLoader.TeamInfo;
            if (teamInfo.IsMine)
            {
                teamObject = teamInfoLoader.gameObject;
                goalStructure.transform.parent = GameObject.Find(teamInfo.ImageTarget).transform;
                goalStructure.transform.localPosition = Vector3.zero;
            }

            else
            {
                GameObject goalClone = GameObject.Instantiate(goalStructure) as GameObject;
                goalClone.transform.parent = GameObject.Find(teamInfo.ImageTarget).transform;
                goalClone.transform.localPosition = Vector3.zero;
            }
        }
    }

    [RPC]
    public void SetHalfBlockColor(Vector3 color)
    {
        GameObject rotatingBlock = GameObject.Find("RotatingBlock");
        rotatingBlock.renderer.material.color = ColorModel.ConvertToUnityColor(color);
        GameObject rotatingHalfBlock = GameObject.Find("RotatingHalfBlock");
        rotatingHalfBlock.renderer.material.color = ColorModel.ConvertToUnityColor(color);
    }

    [RPC]
    public void SetBlockHalf()
    {
        HasFullBlock = false;
        GameObject rotatingBlock = GameObject.Find("RotatingBlock");
        rotatingBlock.renderer.enabled = false;
        GameObject rotatingHalfBlock = GameObject.Find("RotatingHalfBlock");
        rotatingHalfBlock.renderer.enabled = true;
    }

    [RPC]
    public void SetBlockFull()
    {
        HasFullBlock = true;
        GameObject rotatingBlock = GameObject.Find("RotatingBlock");
        rotatingBlock.renderer.enabled = true;
        GameObject rotatingHalfBlock = GameObject.Find("RotatingHalfBlock");
        rotatingHalfBlock.renderer.enabled = false;
    }

    [RPC]
    public void ThrowAwayBlock()
    {

    }
}