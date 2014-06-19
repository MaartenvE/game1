/*using UnityEngine;
using System.Collections;

public delegate void StructureCompleteHandler();
public delegate void StructureProgressHandler(float progress);

public class TeamStructureTracker
{
    public event StructureCompleteHandler OnCompletion;
    public event StructureProgressHandler OnProgressChange;

    private Structure<Color?> goal;
    private Structure<Color?> current;

    private int totalblockCount;
    private int correctBlockCount = 0;
    private int wrongBlockCount = 0;

    public float Progress
    {
        get
        {
            return Mathf.Max(0.0f, (correctBlockCount - (wrongBlockCount / 2f)) / totalblockCount);
        }
    }

    public TeamStructureTracker(Structure<Color?> goalStructure)
    {
        this.goal = goalStructure;
        this.current = new Structure<Color?>(goal.GetLength(0), goal.GetLength(1), goal.GetLength(2));

        initializeCorrectness();
    }

    private void initializeCorrectness()
    {
        foreach (Color? color in goal)
        {
            if (color != null)
            {
                this.totalblockCount++;
            }
        }
    }

    public Vector3 Normalize(Vector3 position, float scale)
    {
        return goal.Normalize(position, scale);
    }

    public Vector3 Denormalize(Vector3 position, float scale)
    {
        return goal.Denormalize(position, scale);
    }

    public bool CheckBlock(Vector3 position, Color? color)
    {

        bool wasCorrect = checkColor(position, current[position]);
        bool correct = checkColor(position, color);

        if (correct)
        {
            if (color == null) wrongBlockCount--;
            else correctBlockCount++;
        }

        else
        {
            if (color == null)
            {
                if (wasCorrect) correctBlockCount--;
                else wrongBlockCount--;
            }
            else
            {
                wrongBlockCount++;
            }
        }

        current[position] = color;

        invokeHandlers();

        return correct;
    }

    private bool checkColor(Vector3 position, Color? color)
    {
        return this.goal[position] == color;
    }


    private void invokeHandlers()
    {
        if (OnProgressChange != null)
        {
            OnProgressChange(Progress);
        }

        if (Progress == 1 && OnCompletion != null)
        {
            OnCompletion();
        }
    }
}
*/