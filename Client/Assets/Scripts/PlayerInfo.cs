using UnityEngine;
using System.Collections;
using BuildingBlocks.CubeFinger;
using BuildingBlocks.Team;

public class PlayerInfo : MonoBehaviour
{
    public static bool IsSpectator = false;
    public static int Team;
    public static CubeFinger CubeFinger;

    public static bool HasFullBlock { get; private set; }

    private static GameObject teamObject;

    private GameObject fullBlock;
    private GameObject halfBlock;

    void Start()
    {
        fullBlock = GameObject.Find("RotatingBlock");
        halfBlock = GameObject.Find("RotatingHalfBlock");
    }

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
    public void SetBlockType(int full, Vector3 color)
    {
        HasFullBlock = full != 0;
        setupBlock(ColorModel.ConvertToUnityColor(color));
        startAnimation();
    }

    private void setupBlock(Color color)
    {
        halfBlock.renderer.enabled = !HasFullBlock;
        fullBlock.renderer.enabled = HasFullBlock;
        halfBlock.renderer.material.color = color;
        fullBlock.renderer.material.color = color;
    }

    private void startAnimation()
    {
        GameObject block = HasFullBlock ? fullBlock : halfBlock;
        BlockAnimationBehaviour animation = block.AddComponent<BlockAnimationBehaviour>();
        animation.SetUpAnimation(new Vector3(0, 0.5f, 2), GameObject.Find("BlockPosition").transform.localPosition);
    }

    [RPC]
    public void ThrowAwayBlock()
    {

    }
}