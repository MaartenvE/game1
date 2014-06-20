﻿using UnityEngine;
using BuildingBlocks.Team;
using BuildingBlocks;

public class BlockConstructor : IBlockConstructor
{
    private GameObject prefab;
    private ITeam team;

    public float Scale { get; private set; }

    public BlockConstructor(ITeam team)
    {
        this.prefab = Resources.Load("GameCube") as GameObject;
        this.team = team;
        this.Scale = prefab.transform.localScale.x;
    }

    public void PlaceGroundBlock(Color color)
    {
        BlockBehaviourFactory f = prefab.GetComponent<BlockBehaviourFactory>();
        f.BlockBehaviourType = "GroundBlockBehaviour";
        PlaceBlock(Vector3.zero, color);
        f.BlockBehaviourType = "BlockBehaviour";
    }

    public void PlaceBlock(Vector3 position, Color color)
    {
        GameObject block = Network.Instantiate(prefab, position, new Quaternion(), 1) as GameObject;
        block.GetComponent<GroundBlockBehaviour>().SetInfo(team.TeamId, position, color);
    }

    public void RemoveBlock(IGameObject block)
    {
        Network.RemoveRPCs(block.networkView.viewID);
        Network.Destroy(block.networkView.viewID);
    }
}