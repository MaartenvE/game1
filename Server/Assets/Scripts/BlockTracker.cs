using UnityEngine;

public class BlockTracker
{
    public event StructureCompleteHandler OnStructureComplete;
    public event StructureProgressHandler OnProgressChange;

    private ITeam team;
    private INetwork network;
    private GameObject prefab;

    private TeamStructureTracker structure;

    public float Progress
    {
        get
        {
            return structure.Progress;
        }
    }

    public BlockTracker(ITeam team, INetwork network, Structure<Color?> goalStructure)
    {
        this.team = team;
        this.network = network;
        this.prefab = Resources.Load("Block") as GameObject;

        this.structure = new TeamStructureTracker(goalStructure);

        this.structure.OnProgressChange += (float progress) =>
        {
            if (this.OnProgressChange != null) this.OnProgressChange(progress);
        };

        this.structure.OnCompletion += () =>
        {
            if (this.OnStructureComplete != null) this.OnStructureComplete();
        };

        Vector3 center = this.structure.Normalize(Vector3.zero, prefab.transform.localScale.x);
        instantiateGroundBlock(goalStructure[center] ?? Color.black);
    }

    private void instantiateGroundBlock(Color color)
    {
        BlockBehaviourFactory f = prefab.GetComponent<BlockBehaviourFactory>();
        f.BlockBehaviourType = "GroundBlockBehaviour";
        instantiateBlock(Vector3.zero, color);
        f.BlockBehaviourType = "BlockBehaviour";
    }

    private void instantiateBlock(Vector3 location, Color color)
    {
        GameObject block = network.Instantiate(prefab, location, prefab.transform.rotation, 1) as GameObject;

        bool correct = checkBlock(location, color);

        // todo: pass correctness
        block.GetComponent<GroundBlockBehaviour>().SetInfo(team.ImageTarget, color);
    }

    public void PlaceBlock(IPlayer player, Vector3 location, Color color)
    {
        if (player.Team == team)
        {
            instantiateBlock(location, color);
        }
    }

    // todo: make fully responsible for destroying block, pass correctness
    public void RemoveBlock(Vector3 location)
    {
        checkBlock(location, null);
    }

    private bool checkBlock(Vector3 location, Color? color)
    {
        return structure.CheckBlock(structure.Normalize(location, prefab.transform.localScale.x), color);
    }
}
