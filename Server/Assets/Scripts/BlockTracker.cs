using UnityEngine;

public class BlockTracker
{
    public delegate void CompleteHandler();
    public event CompleteHandler OnCompletion;

    private ITeam team;
    private INetwork network;
    private GameObject prefab;

    private Structure<Color?> goalStructure;
    private Structure<bool> correctStructure;

    private int maxCorrectness;
    private int correctness;
    public float Progress
    {
        get
        {
            return (float) correctness / (float) maxCorrectness;
        }
    }

    public BlockTracker(ITeam team, INetwork network, Structure<Color?> goalStructure)
    {
        this.team = team;
        this.network = network;
        this.prefab = Resources.Load("Block") as GameObject;

        this.goalStructure = goalStructure;
        this.correctStructure = new Structure<bool>(new bool[goalStructure.GetLength(0), goalStructure.GetLength(1), goalStructure.GetLength(2)]);

        this.maxCorrectness = goalStructure.GetLength(0) * goalStructure.GetLength(1) * goalStructure.GetLength(2);

        initializeCorrectness();

        instantiateGroundBlock(Color.red);
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
        block.GetComponent<GroundBlockBehaviour>().SetInfo(team.ImageTarget, color);

        checkBlock(location, color);

        // todo: Why the fuck is CurrentStructure implemented using Vector3 instead of Color?
        //team.CurrentStructure.updateCorrectness(location, new Vector3(color.r, color.g, color.b));
    }

    public void PlaceBlock(IPlayer player, Vector3 location, Color color)
    {
        if (player.Team == team)
        {
            instantiateBlock(location, color);
        }
    }

    public void RemoveBlock(Vector3 location)
    {
        checkBlock(location, null);
    }

    public void checkBlock(Vector3 location, Color? color)
    {
        Vector3 normalized = location / prefab.transform.localScale.x;
        normalized.x += goalStructure.GetLength(0) / 2;
        normalized.y += goalStructure.GetLength(1) / 2;
        normalized.z += goalStructure.GetLength(2) / 2;

        bool correct = isCorrect(normalized, color);

        if (correct != getCorrectness(normalized))
        {
            setCorrectness(normalized, correct);
        }
    }

    private void initializeCorrectness()
    {
        for (int x = 0; x < goalStructure.GetLength(0); x++)
        {
            for (int y = 0; y < goalStructure.GetLength(1); y++)
            {
                for (int z = 0; z < goalStructure.GetLength(2); z++)
                {
                    Vector3 location = new Vector3(x, y, z);
                    setCorrectness(location, isCorrect(location, null));
                }
            }
        }
    }

    private bool isCorrect(Vector3 location, Color? color)
    {
        return goalStructure[(int)location.x, (int)location.y, (int)location.z] == color;
    }

    private bool getCorrectness(Vector3 location)
    {
        return correctStructure[(int)location.x, (int)location.y, (int)location.z];
    }

    private void setCorrectness(Vector3 location, bool correct)
    {
        if (correct != correctStructure[(int)location.x, (int)location.y, (int)location.z])
        {
            correctness += correct ? 1 : -1;
            correctStructure[(int)location.x, (int)location.y, (int)location.z] = correct;

            if (correctness == maxCorrectness && OnCompletion != null)
            {
                OnCompletion();
            }
        }
    }
}
